using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using IdolFever.Beatmap;
namespace IdolFever.Game
{

    public class BeatmapPlayer : MonoBehaviour
    {
        // Start is called before the first frame update

        public Transform noteHolder;
        public Transform particleHolder;
        public GameObject notePrefab;
        public GameObject hitPrefab;
        public ComboCounter comboCounter;
        public ScoreMeter scoreMeter;
        private BeatmapData beatmap;
        private List<Note> notes;
        [SerializeField] private List<AudioClip> songs;
        [SerializeField] private AsyncSceneTransitionOut sceneOut;
        public AudioSource audio;
        private int beatIndex;
        private ulong usec;
        void Start()
        {
            if (GameConfigurations.SongChosen == SongRegistry.SongList.FUMO_SONG)
            {
                audio.clip = songs[0];
                beatmap = BeatmapReader.Open("OriginalSong1.mid");
            }
            if (GameConfigurations.SongChosen == SongRegistry.SongList.MOUNTAIN_KING)
            {
                audio.clip = songs[1];
                beatmap = BeatmapReader.Open("MountainKing.mid");
            }
            if (GameConfigurations.SongChosen == SongRegistry.SongList.WELLERMAN)
            {
                audio.clip = songs[2];
                beatmap = BeatmapReader.Open("Wellerman.mid");
            }

            audio.Play();
            int n = beatmap.beats.Count;
            scoreMeter.maxscore = n * n * 10 + n * 610;
            beatIndex = 0;
            notes = new List<Note>();
        }

        // Update is called once per frame
        void Update()
        {
            if(PauseScreen.isPaused) {
                return;
            }

            float t = audio.time;
            usec = (ulong)(t * 1000000);
            long spawn = (long)(usec) + 2000000;
            while (beatmap.beats.Count > beatIndex && (long) beatmap.beats[beatIndex].timestamp < spawn)
            {
                GameObject noteGO = Instantiate(notePrefab, Vector3.zero, Quaternion.identity);
                noteGO.transform.SetParent(noteHolder, false);
                noteGO.GetComponent<Note>().noteEvent = beatmap.beats[beatIndex];
                if (beatmap.beats[beatIndex].length > 0) noteGO.GetComponent<Image>().color = new Color(0.8f, 0.3f, 0.8f);
                if (!beatmap.beats[beatIndex].down) noteGO.GetComponent<Image>().color = new Color(0.9f, 0.3f, 0.3f);
                notes.Add(noteGO.GetComponent<Note>());
                beatIndex++;
            }
            for(int i = 0; i < notes.Count;)
            {
                Note n = notes[i];
                if((long) n.noteEvent.timestamp < (long) usec - 400000)
                {
                    notes.RemoveAt(i);
                    Destroy(n.transform.gameObject);
                    comboCounter.combo = 0;
                    continue;
                }
                n.transform.localPosition = new Vector3(n.noteEvent.XPos(), ((long)n.noteEvent.timestamp - (long)usec) / 1000 - 300, 0);
                ++i;
            }
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonOne", "")))) NoteHit(0);
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonTwo", "")))) NoteHit(1);
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonThree", "")))) NoteHit(2);
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonFour", "")))) NoteHit(3);
            if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonOne", "")))) NoteRelease(0);
            if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonTwo", "")))) NoteRelease(1);
            if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonThree", "")))) NoteRelease(2);
            if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonFour", "")))) NoteRelease(3);

            if(audio.time > audio.clip.length - 1)
            {
                sceneOut.ChangeScene();
            }
        }

        public void NoteHit(int k)
        {
            NoteKey key = NoteKey.KEY1;
            switch (k)
            {
                case 0: key = NoteKey.KEY1; break;
                case 1: key = NoteKey.KEY2; break;
                case 2: key = NoteKey.KEY3; break;
                case 3: key = NoteKey.KEY4; break;
            }
            for (int i = 0; i < notes.Count; ++i)
            {
                Note n = notes[i];
                if (!n.noteEvent.down) continue;
                if (n.noteEvent.key == key && (long)n.noteEvent.timestamp < (long)usec + 37500 && (long)n.noteEvent.timestamp > (long)usec - 37500)
                {
                    notes.RemoveAt(i);
                    GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                    hitGo.transform.localPosition = new Vector3(hitGo.transform.localPosition.x, -300, 0);
                    hitGo.GetComponent<Text>().text = "PERFECT";
                    comboCounter.combo++;
                    scoreMeter.AddScore(600 + comboCounter.combo * 10);
                    Destroy(n.transform.gameObject);
                    return;
                }
                if (n.noteEvent.key == key && (long)n.noteEvent.timestamp < (long)usec + 75000 && (long)n.noteEvent.timestamp > (long)usec - 75000)
                {
                    notes.RemoveAt(i);
                    GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                    hitGo.transform.localPosition = new Vector3(hitGo.transform.localPosition.x, -300, 0);
                    hitGo.GetComponent<Text>().text = "GOOD";
                    comboCounter.combo++;
                    scoreMeter.AddScore(400 + comboCounter.combo * 10);
                    Destroy(n.transform.gameObject);
                    return;
                }
                if (n.noteEvent.key == key && (long)n.noteEvent.timestamp < (long)usec + 125000 && (long)n.noteEvent.timestamp > (long)usec - 125000)
                {
                    notes.RemoveAt(i);
                    GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                    hitGo.transform.localPosition = new Vector3(hitGo.transform.localPosition.x, -300, 0);
                    hitGo.GetComponent<Text>().text = "EH";
                    scoreMeter.AddScore(200);
                    Destroy(n.transform.gameObject);
                    return;
                }
                if (n.noteEvent.key == key && (long)n.noteEvent.timestamp < (long)usec + 200000 && (long)n.noteEvent.timestamp > (long)usec - 200000)
                {
                    notes.RemoveAt(i);
                    GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                    hitGo.transform.localPosition = new Vector3(hitGo.transform.localPosition.x, -300, 0);
                    hitGo.GetComponent<Text>().text = "MISS";
                    comboCounter.combo = 0;
                    Destroy(n.transform.gameObject);
                    return;
                }
            }
        }
        public void NoteRelease(int k)
        {
            NoteKey key = NoteKey.KEY1;
            switch(k)
            {
                case 0: key = NoteKey.KEY1; break;
                case 1: key = NoteKey.KEY2; break;
                case 2: key = NoteKey.KEY3; break;
                case 3: key = NoteKey.KEY4; break;
            }
            for (int i = 0; i < notes.Count; ++i)
            {
                Note n = notes[i];
                if (n.noteEvent.down) continue;
                if (n.noteEvent.key == key && (long)n.noteEvent.timestamp < (long)usec + 37500 && (long)n.noteEvent.timestamp > (long)usec - 37500)
                {
                    notes.RemoveAt(i);
                    GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                    hitGo.transform.localPosition = new Vector3(hitGo.transform.localPosition.x, -300, 0);
                    hitGo.GetComponent<Text>().text = "PERFECT";
                    comboCounter.combo++;
                    scoreMeter.AddScore(600 + comboCounter.combo * 10);
                    Destroy(n.transform.gameObject);
                    return;
                }
                if (n.noteEvent.key == key && (long)n.noteEvent.timestamp < (long)usec + 75000 && (long)n.noteEvent.timestamp > (long)usec - 75000)
                {
                    notes.RemoveAt(i);
                    GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                    hitGo.transform.localPosition = new Vector3(hitGo.transform.localPosition.x, -300, 0);
                    hitGo.GetComponent<Text>().text = "GOOD";
                    comboCounter.combo++;
                    scoreMeter.AddScore(400 + comboCounter.combo * 10);
                    Destroy(n.transform.gameObject);
                    return;
                }
                if (n.noteEvent.key == key && (long)n.noteEvent.timestamp < (long)usec + 125000 && (long)n.noteEvent.timestamp > (long)usec - 125000)
                {
                    notes.RemoveAt(i);
                    GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                    hitGo.transform.localPosition = new Vector3(hitGo.transform.localPosition.x, -300, 0);
                    hitGo.GetComponent<Text>().text = "EH";
                    scoreMeter.AddScore(200);
                    Destroy(n.transform.gameObject);
                    return;
                }
                if (n.noteEvent.key == key && (long)n.noteEvent.timestamp < (long)usec + 200000 && (long)n.noteEvent.timestamp > (long)usec - 200000)
                {
                    notes.RemoveAt(i);
                    GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                    hitGo.transform.localPosition = new Vector3(hitGo.transform.localPosition.x, -300, 0);
                    hitGo.GetComponent<Text>().text = "MISS";
                    comboCounter.combo = 0;
                    Destroy(n.transform.gameObject);
                    return;
                }
            }
        }

    }
}
