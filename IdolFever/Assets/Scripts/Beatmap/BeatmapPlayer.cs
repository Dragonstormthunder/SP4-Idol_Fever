using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IdolFever.Beatmap;
namespace IdolFever.Game
{

    public class BeatmapPlayer : MonoBehaviour
    {
        // Start is called before the first frame update

        public Transform noteHolder;
        public GameObject notePrefab;
        private BeatmapData beatmap;
        private List<Note> notes;
        private AudioSource audio;
        private int beatIndex;
        void Start()
        {
            beatmap = BeatmapReader.Open("Assets/Resources/BeatmapTest.mid");
            audio = GetComponent<AudioSource>();
            audio.Play();
            beatIndex = 0;
            notes = new List<Note>();
        }

        // Update is called once per frame
        void Update()
        {
            float t = audio.time;
            ulong usec = (ulong)(t * 1000000);
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
                n.transform.localPosition = new Vector3(n.noteEvent.XPos(), ((long)n.noteEvent.timestamp - (long)usec) / 1000 + 100, 0);
                ++i;
            }
        }
    }
}
