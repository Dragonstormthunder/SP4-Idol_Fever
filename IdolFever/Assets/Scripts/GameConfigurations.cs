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
        private static float lastHighScore;

        private static bool wasThereOpponent;
        private static string opponentUsername;
        private static float opponentHighScore;

        private static bool uploadToFirebase;

        #endregion


        #region Properties

        public static SongRegistry.SongList SongChosen
        {
            get
            {
                if (songChosen == SongRegistry.SongList.NOT_OPTION)
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

        public static string Username
        {
            get { return username; }
            set { username = value; }
        }

        public static float LastHighScore
        {
            get { return lastHighScore; }
            set { lastHighScore = value; }
        }

        public static bool WasThereOpponent
        {
            get { return wasThereOpponent; }
            set { wasThereOpponent = value; }
        }

        public static string OpponentUsername
        {
            get { return opponentUsername; }
            set { opponentUsername = value; }
        }

        public static float OpponentHighScore
        {
            get { return opponentHighScore; }
            set { opponentHighScore = value; }
        }

        public static bool UploadToFirebase
        {
            get { return uploadToFirebase; }
            set { uploadToFirebase = value; }
        }

        #endregion

        static GameConfigurations()
        {
            songChosen = SongRegistry.SongList.NOT_OPTION;
            characterIndex = Character.CharacterFactory.eCHARACTER.R_CHARACTER_BOY0;
            bonus = 0;

            username = "";
            lastHighScore = 0;

            wasThereOpponent = false;
            opponentUsername = "";
            opponentHighScore = 0;

            uploadToFirebase = false;

        }

    }

}