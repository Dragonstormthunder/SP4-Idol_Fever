using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using IdolFever.Beatmap;
using Photon.Pun;

namespace IdolFever.Game
{

    public class BeatmapPlayer : MonoBehaviour
    {
        // Start is called before the first frame update

        private bool isExitingScene = false;
        public Transform noteHolder;
        public Transform particleHolder;
        public GameObject notePrefab;
        public GameObject hitPrefab;
        public GameObject holdPrefab;
        public ComboCounter comboCounter;
        public ScoreMeter scoreMeter;
        public Sprite start, end;
        private BeatmapData beatmap;
        private List<Note> notes;
        [SerializeField] private List<AudioClip> songs;
        [SerializeField] private List<string> songFileNames;
        [SerializeField] private List<Sprite> sprites, hitSprites;
        [SerializeField] private AsyncSceneTransitionOut sceneOut;
        [SerializeField] private List<GameObject> characters;
        [SerializeField] private List<GameObject> stages;
        public AudioSource audio;
        private int beatIndex;
        private ulong usec;

        private Transform myChar, otherChar;



        internal static int StageIndex
        {
            get;
            set;
        }

        static BeatmapPlayer()
        {
            StageIndex = -1;
        }


        private void Awake()
        {
            if (!PhotonNetwork.IsConnected)
            {
                StageIndex = UnityEngine.Random.Range(0, 1);
            }
        }

        void Start()
        {
            // game has started, so we're going to upload the highscore data to firebase after this
            GameConfigurations.UploadToFirebase = true;
            // check for opponent's presence
            if (PhotonNetwork.IsConnected)
            {
                GameConfigurations.WasThereOpponent = true;
                if (PhotonNetwork.PlayerListOthers.Length != 0)
                    GameConfigurations.OpponentUsername = PhotonNetwork.PlayerListOthers[0].NickName;
            }
            else
            {
                GameConfigurations.WasThereOpponent = false;
            }

            if ((int)GameConfigurations.SongChosen < (int)SongRegistry.SongList.NOT_OPTION)
            {
                int index = (int)GameConfigurations.SongChosen;
                audio.clip = songs[index];
                beatmap = BeatmapReader.Open(songFileNames[index]);
            }
            else
            {
                beatmap = BeatmapReader.Open(songFileNames[0]);
            }

            myChar = Instantiate(characters[1], new Vector3(-5.4f, -3.7f, -1.8f), Quaternion.AngleAxis(180, new Vector3(0, 1, 0))).transform;
            myChar.GetComponent<Animator>().Rebind();
            myChar.GetComponent<Animator>().SetFloat("Speed", 103.0f / 120.0f);
            myChar.name = "GirlCharacter";

            otherChar = Instantiate(characters[0], new Vector3(5.4f, -3.7f, -1.8f), Quaternion.AngleAxis(180, new Vector3(0, 1, 0))).transform;
            otherChar.GetComponent<Animator>().Rebind();
            otherChar.GetComponent<Animator>().SetFloat("Speed", 103.0f / 120.0f);
            otherChar.name = "BoyCharacter";

            GameObject stage = Instantiate(stages[StageIndex], new Vector3(0, -5, 0), Quaternion.AngleAxis(180, new Vector3(0, 1, 0)));
            stage.name = "Stage";

            audio.Play();
            int n = beatmap.beats.Count;
            scoreMeter.maxscore = n * 600;
            beatIndex = 0;
            notes = new List<Note>();
        }

        // Update is called once per frame
        void Update()
        {
            if (PauseScreen.isPaused && !PhotonNetwork.IsConnected)
            {
                return;
            }

            float t = audio.time;
            usec = (ulong)(t * 1000000);
            long spawn = (long)(usec) + 2000000;
            while (beatmap.beats.Count > beatIndex && (long)beatmap.beats[beatIndex].timestamp < spawn)
            {
                if (beatmap.beats[beatIndex].down)
                {
                    GameObject noteGO = Instantiate(notePrefab, Vector3.zero, Quaternion.identity);
                    if (beatmap.beats[beatIndex].length > 0)
                    {
                        GameObject holdGO = Instantiate(holdPrefab, Vector3.zero, Quaternion.identity);
                        holdGO.transform.SetParent(noteHolder, false);
                        noteGO.GetComponent<Note>().holdNote = holdGO.transform;
                    }
                    noteGO.transform.SetParent(noteHolder, false);
                    noteGO.GetComponent<Note>().noteEvent = beatmap.beats[beatIndex];
                    switch (beatmap.beats[beatIndex].key)
                    {
                        case NoteKey.KEY1:
                            noteGO.GetComponent<Image>().sprite = sprites[0];
                            break;
                        case NoteKey.KEY2:
                            noteGO.GetComponent<Image>().sprite = sprites[1];
                            break;
                        case NoteKey.KEY3:
                            noteGO.GetComponent<Image>().sprite = sprites[2];
                            break;
                        case NoteKey.KEY4:
                            noteGO.GetComponent<Image>().sprite = sprites[3];
                            break;
                        default:
                            break;
                    }
                    notes.Add(noteGO.GetComponent<Note>());
                }
                beatIndex++;
            }
            bool key1 = false, key2 = false, key3 = false, key4 = false;
            for (int i = 0; i < notes.Count;)
            {
                Note n = notes[i];

                if ((long)n.noteEvent.timestamp > (long)usec && (long)n.noteEvent.timestamp < (long)usec + 100000)
                {
                    if (n.noteEvent.key == NoteKey.KEY1) key1 = true;
                    if (n.noteEvent.key == NoteKey.KEY2) key2 = true;
                    if (n.noteEvent.key == NoteKey.KEY3) key3 = true;
                    if (n.noteEvent.key == NoteKey.KEY4) key4 = true;
                }
                if ((long)n.noteEvent.timestamp + (long)n.noteEvent.length < (long)usec - 400000)
                {
                    notes.RemoveAt(i);
                    if (n.holdNote) Destroy(n.holdNote.gameObject);
                    Destroy(n.transform.gameObject);
                    comboCounter.combo = 0;
                    continue;
                }
                n.transform.localPosition = new Vector3(n.noteEvent.XPos(), (n.holdHit ? -300 : ((long)n.noteEvent.timestamp - (long)usec) / 1000 - 300), 0);
                if (n.holdNote != null)
                {
                    n.holdNote.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ((long)n.noteEvent.timestamp + (long)n.noteEvent.length - (long)usec) / 1000 - (n.holdHit ? 0 : ((long)n.noteEvent.timestamp - (long)usec) / 1000));
                    n.holdNote.GetComponent<RectTransform>().localPosition = new Vector3(n.noteEvent.XPos() - 5, n.holdHit ? -310 : ((long)n.noteEvent.timestamp - (long)usec) / 1000 - 310, 0);
                }
                ++i;
            }

            otherChar.GetComponent<Animator>().SetBool("Left", key1);
            otherChar.GetComponent<Animator>().SetBool("Up", key2);
            otherChar.GetComponent<Animator>().SetBool("Down", key3);
            otherChar.GetComponent<Animator>().SetBool("Right", key4);

            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonOne", ""))))
            {
                NoteHit(0);
            }
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonTwo", ""))))
            {
                NoteHit(1);
            }
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonThree", ""))))
            {
                NoteHit(2);
            }
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonFour", ""))))
            {
                NoteHit(3);
            }
            if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonOne", ""))))
            {
                NoteRelease(0);
            }
            if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonTwo", ""))))
            {
                NoteRelease(1);
            }
            if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonThree", ""))))
            {
                NoteRelease(2);
            }
            if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonFour", ""))))
            {
                NoteRelease(3);
            }

            if (audio.time > audio.clip.length - 1)
            {
                if (!isExitingScene)
                {
                    _ = StartCoroutine(nameof(DcAndChangeScene));
                    isExitingScene = true;
                }
            }
        }

        private IEnumerator DcAndChangeScene()
        {
            PhotonNetwork.Disconnect();

            while (PhotonNetwork.IsConnected)
            {
                yield return null;
            }

            sceneOut.ChangeScene();
        }

        public void NoteHit(int k)
        {
            //Debug.Log("Note Hit Begin: id:" + k);

            NoteKey key = NoteKey.KEY1;
            switch (k)
            {
                case 0:
                    key = NoteKey.KEY1;
                    myChar.GetComponent<Animator>().SetBool("Left", true);
                    break;
                case 1:
                    key = NoteKey.KEY2;
                    myChar.GetComponent<Animator>().SetBool("Up", true);
                    break;
                case 2:
                    key = NoteKey.KEY3;
                    myChar.GetComponent<Animator>().SetBool("Down", true);
                    break;
                case 3:
                    key = NoteKey.KEY4;
                    myChar.GetComponent<Animator>().SetBool("Right", true);
                    break;
            }
            for (int i = 0; i < notes.Count; ++i)
            {
                Note n = notes[i];
                if (!n.noteEvent.down)
                {
                    //Debug.Log("Note Hit Continue" + k);
                    continue;
                }
                if (n.noteEvent.key == key && (long)n.noteEvent.timestamp < (long)usec + 200000 && (long)n.noteEvent.timestamp > (long)usec - 200000)
                {
                    //Debug.Log("Note Hit B4 Vibration" + k);
                    //VibrationControl.StartVibration();
                    //Debug.Log("Note Hit AFT Vibration" + k);

                    if ((long)n.noteEvent.timestamp < (long)usec + 37500 && (long)n.noteEvent.timestamp > (long)usec - 37500)
                    {
                        GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                        hitGo.transform.localPosition = new Vector3(hitGo.transform.localPosition.x, -300, 0);
                        hitGo.GetComponent<Image>().sprite = hitSprites[0];
                        comboCounter.combo++;
                        scoreMeter.AddScore(600);
                        GameConfigurations.LastHighScore = scoreMeter.GetScoreMeterValue();
                    }
                    else if ((long)n.noteEvent.timestamp < (long)usec + 125000 && (long)n.noteEvent.timestamp > (long)usec - 125000)
                    {
                        GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                        hitGo.transform.localPosition = new Vector3(hitGo.transform.localPosition.x, -300, 0);
                        hitGo.GetComponent<Image>().sprite = hitSprites[1];
                        comboCounter.combo++;
                        scoreMeter.AddScore(400);
                        GameConfigurations.LastHighScore = scoreMeter.GetScoreMeterValue();
                    }
                    else
                    {
                        GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                        hitGo.transform.localPosition = new Vector3(hitGo.transform.localPosition.x, -300, 0);
                        hitGo.GetComponent<Image>().sprite = hitSprites[2];
                        comboCounter.combo = 0;
                    }
                    if (n.noteEvent.length == 0)
                    {
                        notes.RemoveAt(i);
                        Destroy(n.transform.gameObject);
                    }
                    else
                    {
                        n.holdHit = true;
                    }
                    return;
                }
            }
        }
        public void NoteRelease(int k)
        {
            NoteKey key = NoteKey.KEY1;
            switch (k)
            {
                case 0:
                    key = NoteKey.KEY1;
                    myChar.GetComponent<Animator>().SetBool("Left", false);
                    break;
                case 1:
                    key = NoteKey.KEY2;
                    myChar.GetComponent<Animator>().SetBool("Up", false);
                    break;
                case 2:
                    key = NoteKey.KEY3;
                    myChar.GetComponent<Animator>().SetBool("Down", false);
                    break;
                case 3:
                    key = NoteKey.KEY4;
                    myChar.GetComponent<Animator>().SetBool("Right", false);
                    break;
            }
            for (int i = 0; i < notes.Count; ++i)
            {
                Note n = notes[i];
                if (n.holdHit && n.noteEvent.key == key)
                {
                    if ((long)n.noteEvent.timestamp + (long)n.noteEvent.length < (long)usec + 37500 && (long)n.noteEvent.timestamp + (long)n.noteEvent.length > (long)usec - 37500)
                    {
                        GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                        hitGo.transform.localPosition = new Vector3(hitGo.transform.localPosition.x, -300, 0);
                        hitGo.GetComponent<Image>().sprite = hitSprites[0];
                        comboCounter.combo++;
                        scoreMeter.AddScore(600);
                        GameConfigurations.LastHighScore = scoreMeter.GetScoreMeterValue();
                    }
                    else if ((long)n.noteEvent.timestamp + (long)n.noteEvent.length < (long)usec + 125000 && (long)n.noteEvent.timestamp + (long)n.noteEvent.length > (long)usec - 125000)
                    {
                        GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                        hitGo.transform.localPosition = new Vector3(hitGo.transform.localPosition.x, -300, 0);
                        hitGo.GetComponent<Image>().sprite = hitSprites[1];
                        comboCounter.combo++;
                        scoreMeter.AddScore(400);
                        GameConfigurations.LastHighScore = scoreMeter.GetScoreMeterValue();
                    }
                    else
                    {
                        GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                        hitGo.transform.localPosition = new Vector3(hitGo.transform.localPosition.x, -300, 0);
                        hitGo.GetComponent<Image>().sprite = hitSprites[2];
                        comboCounter.combo = 0;
                    }
                    Destroy(n.GetComponent<Note>().holdNote.gameObject);
                    notes.RemoveAt(i);
                    Destroy(n.transform.gameObject);
                    return;
                }
            }
        }

    }
}
