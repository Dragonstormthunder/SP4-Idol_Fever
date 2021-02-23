using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IdolFever.Server.Achievements
{

    public class AchievementManager : MonoBehaviour
    {

        #region Fields

        public ServerDatabase serverDatabase;
        public GameObject achievementPrefab;
        public Transform achievementLocation;

        [SerializeField] private List<GameObject> contentAchievements;

        #endregion

        #region AchievementList

        // save the achievements as an enum
        // but we're going to use enum.ToString() for the saving to the Firebase

        public enum ListAchievements
        {
            COMPLETE_THE_TUTORIAL,
            BAND_UPGRADE,
            UNIT_COLLECTOR,
            FIRST_TIME_HOORAY,
            ALWAYS_GOT_TIME_FOR_A_PERFECT,
            YOU_WILL_CHARM_THEM_NEXT_TIME,
            HARD_MODE_ENGAGED,
            NUM_ACHIEVEMENTS
        }

        #endregion

        // Start is called before the first frame update
        void Start()
        {

            //StartCoroutine(serverDatabase.UpdateAchievements(ListAchievements.COMPLETE_THE_TUTORIAL.ToString(), true));

            // create all the prefabs for the achievements and place it in a list for easy use
            List<GameObject> contentAchievements = new List<GameObject>();

            string name = "";
            string description = "";

            for (ListAchievements i = 0; i < ListAchievements.NUM_ACHIEVEMENTS; ++i)
            {
                GameObject achievement = Instantiate(achievementPrefab, achievementLocation.transform, false);

                // name the achievements
                Transform achievementName = achievement.transform.GetChild(0).transform.Find("AchievementName");
                Transform achievementDescription = achievement.transform.GetChild(0).transform.Find("AchievementDescription");

                description = AchievementFactory.AchievementDescriptions(i, ref name);

                achievementName.GetComponent<TextMeshProUGUI>().text = name;
                achievementDescription.GetComponent<TextMeshProUGUI>().text = description;

                // add it to the list
                contentAchievements.Add(achievement);

            }


            // start a coroutine to get the achievements
            // use a callback to obtain the achievements and do something with it
            StartCoroutine(serverDatabase.GrabAchievements((achievements) =>
            {

                Debug.Log("Here: " + achievements.Count);

                // we have all the achievements the player has


                // if there are no acheivements, this loop will fail
                for (int i = 0; i < achievements.Count; ++i)
                {

                    Debug.Log(achievements[i]);

                    // need to find the index where it is
                    ListAchievements index = 0;
                    for (index = 0; index < ListAchievements.NUM_ACHIEVEMENTS; ++i)
                    {
                        if (index.ToString() == achievements[i])
                        {
                            // disable the button for the reward
                            Transform button = contentAchievements[(int)index].transform.GetChild(0).transform.Find("RewardButton");
                            button.gameObject.SetActive(false);
                            break;
                        }
                    }
                }

            }));

        }

    }
}