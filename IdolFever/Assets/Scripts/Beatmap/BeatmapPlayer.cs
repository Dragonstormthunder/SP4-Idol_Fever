using System.Collections;
using System.Collections.Generic;
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
        private AudioSource audio;
        private int beatIndex;
        private ulong usec;
        void Start()
        {
            beatmap = BeatmapReader.Open("MountainKing.mid");
            audio = GetComponent<AudioSource>();
            audio.Play();
            beatIndex = 0;
            notes = new List<Note>();
        }

        // Update is called once per frame
        void Update()
        {
            float t = audio.time;
            usec = (ulong)(t * 1000000);
            long spawn = (long)(usec) + 2000000;
            while (beatmap.beats.Count > beatIndex && (long) beatmap.beats[beatIndex].timestamp < spawn)
            {
                GameObject noteGO = Instantiate(notePrefab, Vector3.zero, Quaternion.identity);
                noteGO.transform.SetParent(noteHolder, false);
                noteGO.GetComponent<Note>().noteEvent = beatmap.beats[beatIndex];
                notes.Add(noteGO.GetComponent<Note>());
                beatIndex++;
            }
            for(int i = 0; i < notes.Count;)
            {
                Note n = notes[i];
                if((long) n.noteEvent.timestamp < (long) usec - 1000000)
                {
                    notes.RemoveAt(i);
                    Destroy(n.transform.gameObject);
                    continue;
                }
                n.transform.localPosition = new Vector3(n.noteEvent.XPos(), ((long)n.noteEvent.timestamp - (long)usec) / 1000 - 300, 0);
                ++i;
            }
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonOne", "")))) NoteHit(0);
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonTwo", "")))) NoteHit(1);
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonThree", "")))) NoteHit(2);
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonFour", "")))) NoteHit(3);
        }

        public void NoteHit(int k)
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
                if (n.noteEvent.key == key && (long)n.noteEvent.timestamp < (long)usec + 37500 && (long)n.noteEvent.timestamp > (long)usec - 37500)
                {
                    notes.RemoveAt(i);
                    GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                    hitGo.GetComponent<Text>().text = "PERFECT";
                    comboCounter.combo++;
                    scoreMeter.AddScore(600 + comboCounter.combo * 20);
                    Destroy(n.transform.gameObject);
                    return;
                }
                if (n.noteEvent.key == key && (long)n.noteEvent.timestamp < (long)usec + 75000 && (long)n.noteEvent.timestamp > (long)usec - 75000)
                {
                    notes.RemoveAt(i);
                    GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                    hitGo.GetComponent<Text>().text = "GOOD";
                    comboCounter.combo++;
                    scoreMeter.AddScore(400 + comboCounter.combo * 20);
                    Destroy(n.transform.gameObject);
                    return;
                }
                if (n.noteEvent.key == key && (long)n.noteEvent.timestamp < (long)usec + 125000 && (long)n.noteEvent.timestamp > (long)usec - 125000)
                {
                    notes.RemoveAt(i);
                    GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                    hitGo.GetComponent<Text>().text = "EH";
                    scoreMeter.AddScore(200);
                    Destroy(n.transform.gameObject);
                    return;
                }
                if (n.noteEvent.key == key && (long)n.noteEvent.timestamp < (long)usec + 200000 && (long)n.noteEvent.timestamp > (long)usec - 200000)
                {
                    notes.RemoveAt(i);
                    GameObject hitGo = Instantiate(hitPrefab, n.transform.position, Quaternion.identity, particleHolder);
                    hitGo.GetComponent<Text>().text = "MISS";
                    comboCounter.combo = 0;
                    Destroy(n.transform.gameObject);
                    return;
                }
            }
        }

    }
}
