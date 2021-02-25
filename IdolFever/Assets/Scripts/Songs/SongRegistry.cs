using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever
{
    internal sealed class SongRegistry : MonoBehaviour
    {
        public enum SongList : int
        {
            WELLERMAN,
            MOUNTAIN_KING,
            FUMO_SONG,
            NOT_OPTION
        };

        internal static string GetSongName(SongList index)
        {
            switch (index)
            {
                default:
                    return "";

                case SongList.WELLERMAN:
                    return "Wellerman";

                case SongList.MOUNTAIN_KING:
                    return "Mountain King";

                case SongList.FUMO_SONG:
                    return "Fumo Song";

            }
        }

    }

}
