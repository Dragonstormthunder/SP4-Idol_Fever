using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever
{

    // store game configurations for the gameplay
    internal static class GameConfigurations
    {

        #region Fields

        private static SongRegistry.SongList songChosen;

        #endregion


        #region Properties

        public static SongRegistry.SongList SongChosen
        {
            get
            {
                if (SongRegistry.SongList.NOT_OPTION == songChosen)
                {
                    songChosen = SongRegistry.SongList.MOUNTAIN_KING;
                }
                return songChosen;
            }
            set { songChosen = value; }
        }

        #endregion

        static GameConfigurations()
        {
            songChosen = SongRegistry.SongList.NOT_OPTION;
        }

    }

}