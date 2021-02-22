using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever
{
    internal sealed class SongDataFactory
    {
        // generate the appropriate song data

        public static void GenerateSongData(SongRegistry.SongList index, ref string name, ref int rating)
        {
            switch (index)
            {
                default:
                    // return with default values
                    name = "";
                    rating = 1;
                    break;
                case SongRegistry.SongList.MOUNTAIN_KING:
                    name = "Mountain King";
                    rating = 5;
                    break;
                case SongRegistry.SongList.FUMO_SONG:
                    name = "Fumo Song";
                    rating = 1;
                    break;

                case SongRegistry.SongList.WELLERMAN:
                    name = "Wellerman";
                    rating = 3;
                    break;

            }
        }


    }
}