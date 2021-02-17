using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IdolFever.Server
{

    public class AchievementManager : MonoBehaviour
    {
        #region Fields

        public ServerDatabase serverDatabase;
        public GameObject achievementPrefab;
        public Transform achievementLocation;

        #endregion


        // Start is called before the first frame update
        void Start()
        {

            StartCoroutine(serverDatabase.GrabAchievements((achievements) =>
            {

                Debug.Log(achievements);

                for (int i = 0; i < achievements.Count; ++i)
                {
                    GameObject achievement = Instantiate(achievementPrefab, new Vector2(0, 0), Quaternion.identity);

                    achievement.transform.parent = achievementLocation;

                    achievement.GetComponent<RectTransform>().localPosition = new Vector2(-650 + i * 500, -62);

                    Transform achievementName = achievement.transform.GetChild(0).transform.Find("AchievementName");
                    achievementName.GetComponent<TextMeshProUGUI>().text = achievements[i];

                }


            }));

        }

    }
}