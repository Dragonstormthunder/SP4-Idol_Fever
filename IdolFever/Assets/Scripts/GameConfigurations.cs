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

        private static Character.CharacterFactory.eCHARACTER characterIndex;
        private static int bonus;
        private static string username;

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

        public static Character.CharacterFactory.eCHARACTER CharacterIndex
        {
            get { return characterIndex; }
            set { characterIndex = value; }
        }

        public static int CharacterBonus
        {
            get { return bonus; }
            set { bonus = value; }
        }

        public static string Username {
            get {
                return username;
            }
            set {
                username = value;
            }
        }

        #endregion

        static GameConfigurations()
        {
            songChosen = SongRegistry.SongList.NOT_OPTION;
            characterIndex = Character.CharacterFactory.eCHARACTER.R_CHARACTER_BOY0;
            bonus = 0;
        }

    }

}