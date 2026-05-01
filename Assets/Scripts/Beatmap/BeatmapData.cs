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
        public ulong timestamp, length;
        public NoteKey key;
        public bool down;

        public BeatmapEvent(ulong time, ulong len, NoteKey k, bool d = true)
        {
            timestamp = time;
            length = len;
            key = k;
            down = d;
        }

        public static int CompareTime(BeatmapEvent x, BeatmapEvent y)
        {
            return (int)(x.timestamp - y.timestamp);
        }

        public float XPos()
        {
            switch (key)
            {
                case NoteKey.KEY1:
                    return -720;
                case NoteKey.KEY2:
                    return -240;
                case NoteKey.KEY3:
                    return 240;
                case NoteKey.KEY4:
                    return 720;
            }
            return 0;

        }
    }

    public class BeatmapData
    {
        public List<BeatmapEvent> beats;

        public BeatmapData()
        {
            beats = new List<BeatmapEvent>();
        }
    }
}