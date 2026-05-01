using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever.Server.Achievements
{
    // this is just a factory for setting achievements
    public static class AchievementFactory
    {

        public static string AchievementDescriptions(AchievementManager.ListAchievements achievement, ref string name)
        {
            switch (achievement)
            {
                default:
                    name = "";
                    return "";

                case AchievementManager.ListAchievements.COMPLETE_THE_TUTORIAL:
                    name = "Complete the Tutorial";
                    return "Complete The Tutorial";

                case AchievementManager.ListAchievements.BAND_UPGRADE:
                    name = "Band Upgrade";
                    return "Collect X unique characters";

                case AchievementManager.ListAchievements.UNIT_COLLECTOR:
                    name = "Unit Collector";
                    return "Collect X number of a unique character";

                case AchievementManager.ListAchievements.FIRST_TIME_HOORAY:
                    name = "First Time Hooray";
                    return "Get a full perfect on a song";

                case AchievementManager.ListAchievements.ALWAYS_GOT_TIME_FOR_A_PERFECT:
                    name = "Always Got Time For A Perfect";
                    return "Get full perfect for X unique songs";

                case AchievementManager.ListAchievements.YOU_WILL_CHARM_THEM_NEXT_TIME:
                    name = "You'll Charm Them Next Time";
                    return "Miss 80% of notes in a song";

                case AchievementManager.ListAchievements.HARD_MODE_ENGAGED:
                    name = "Hard Mode Engaged";
                    return "Complete X number of songs on hard mode";

            }

        }
    }

}

