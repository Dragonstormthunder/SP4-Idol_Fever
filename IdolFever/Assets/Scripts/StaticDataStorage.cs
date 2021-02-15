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

        //public enum COLOR_MODE
        //{
        //    MODE_ORANGE,
        //    MODE_RED,
        //    MODE_DARK_BLUE,
        //    MODE_LIGHT_BLUE,
        //    MODE_LIGHT_GREEN,
        //    NUM_COLOR
        //}

        //public enum POWER_UP_TYPE
        //{
        //    POWER_UP_NITROBOOST,
        //    POWER_UP_SHIELD,
        //    NUM_POWER_UP // use this for no power-up
        //}

        //public static string[] HTMLColors = { "#FF9900", "#FF000A", "#0B00FF", "#00FFEF", "#00FF36" };

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

        //public static int Currency
        //{
        //    get { return m_currency; }
        //    set { m_currency = value; }
        //}

        //public static int WheelSpeed
        //{
        //    get { return wheelSpeed; }
        //    set { wheelSpeed = value; }
        //}

        //public static int WheelAcceleration
        //{
        //    get { return wheelAcceleration; }
        //    set { wheelAcceleration = value; }
        //}

        //public static int WheelWeight
        //{
        //    get { return wheelWeight; }
        //    set { wheelWeight = value; }
        //}

        //public static int CarBodySpeed
        //{
        //    get { return carBodySpeed; }
        //    set { carBodySpeed = value; }
        //}

        //public static int CarBodyAcceleration
        //{
        //    get { return carBodyAcceleration; }
        //    set { carBodyAcceleration = value; }
        //}

        //public static int CarBodyWeight
        //{
        //    get { return carBodyWeight; }
        //    set { carBodyWeight = value; }
        //}

        //public static int GliderSpeed
        //{
        //    get { return gliderSpeed; }
        //    set { gliderSpeed = value; }
        //}

        //public static int GliderAcceleration
        //{
        //    get { return gliderAcceleration; }
        //    set { gliderAcceleration = value; }
        //}

        //public static int GliderWeight
        //{
        //    get { return gliderWeight; }
        //    set { gliderWeight = value; }
        //}

        //public static bool[] Wheels
        //{
        //    get { return wheels; }
        //    set { wheels = value; }
        //}

        //public static bool[] Bodys
        //{
        //    get { return bodys; }
        //    set { bodys = value; }
        //}

        //public static bool[] Gliders
        //{
        //    get { return gliders; }
        //    set { gliders = value; }
        //}

        //public static bool[] ChaptersIP
        //{
        //    get { return chaptersIP; }
        //    set { chaptersIP = value; }
        //}

        //public static bool[] ChaptersC
        //{
        //    get { return chaptersC; }
        //    set { chaptersC = value; }
        //}

        // total speed
        //public static int Speed
        //{
        //    get { return wheelSpeed + carBodySpeed + gliderSpeed; }
        //}

        // total acceleration
        //public static int Acceleration
        //{
        //    get { return wheelAcceleration + carBodyAcceleration + gliderAcceleration; }
        //}

        // total weight
        //public static int Weight
        //{
        //    get { return wheelWeight + carBodyWeight + gliderWeight; }
        //}

        //public static COLOR_MODE WheelColor
        //{
        //    get { return wheelColor; }
        //    set { wheelColor = value; }
        //}

        //public static COLOR_MODE CarBodyWheel
        //{
        //    get { return carBodyColor; }
        //    set { carBodyColor = value; }
        //}

        //public static COLOR_MODE GliderColor
        //{
        //    get { return gliderColor; }
        //    set { gliderColor = value; }
        //}

        //public static COLOR_MODE CarBodyColor
        //{
        //    get { return carBodyColor; }
        //    set { carBodyColor = value; }
        //}

        //public static POWER_UP_TYPE PowerUp
        //{
        //    get { return powerUp; }
        //    set { powerUp = value; }
        //}

        private static string lastSceneName;    // last scene name
        private static GAME_MODE gameMode;      // current game mode
        //private static int m_currency;

        //private static COLOR_MODE wheelColor;
        //private static int wheelSpeed;
        //private static int wheelAcceleration;
        //private static int wheelWeight;

        //private static COLOR_MODE carBodyColor;
        //private static int carBodySpeed;
        //private static int carBodyAcceleration;
        //private static int carBodyWeight;

        //private static COLOR_MODE gliderColor;
        //private static int gliderSpeed;
        //private static int gliderAcceleration;
        //private static int gliderWeight;

        //private static POWER_UP_TYPE powerUp = POWER_UP_TYPE.NUM_POWER_UP;

        //private static bool[] wheels = new bool[12];
        //private static bool[] bodys = new bool[12];
        //private static bool[] gliders = new bool[12];
        //private static bool[] chaptersIP = new bool[5]; // in progress
        //private static bool[] chaptersC = new bool[5];  // completed
    }

}