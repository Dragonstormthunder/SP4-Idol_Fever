using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace IdolFever.Beatmap
{
    public enum NoteKey
    {
        KEY1, KEY2, KEY3, KEY4
    }

    public class BeatmapEvent
    {
        ulong timestamp, length;
        NoteKey key;

        public BeatmapEvent(ulong time, ulong len, NoteKey k)
        {
            timestamp = time;
            length = len;
            key = k;
        }
    }

    public class Beatmap
    {
        public List<BeatmapEvent> beats;
    }
}