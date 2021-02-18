using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever.UI
{
    public static class StaticDataStorage
    {
        public enum GAME_MODE
        {
            MODE_MENU,
            MODE_ONLINE,
            MODE_STORY
        }

        public enum CARD_DRAWN
        {
            CARD_1,
            CARD_2,
            CARD_3,
            CARD_4,
            CARD_5,
            CARD_6,
            CARD_7,
            CARD_8,
            CARD_9,
            CARD_10,
            TOTAL_CARD,
        }

        public static string LastSceneName
        {
            get { return lastSceneName; }
            set { lastSceneName = value; }
        }

        public static GAME_MODE GameMode
        {
            get { return gameMode; }
            set { gameMode = value; }
        }

        private static string lastSceneName;    // last scene name
        private static GAME_MODE gameMode;      // current game mode

        public static bool R_Girl = false;
        public static bool R_Boy = false;
        public static bool SR_Girl = false;
        public static bool SR_Boy = false;
        public static bool SSR_Girl = false;
        public static bool SSR_Boy = false;

        public static bool TenDraw = false;
        public static CARD_DRAWN cardNo = (CARD_DRAWN)0;
    }

}