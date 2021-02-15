using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IdolFever.Achievement
{
    public class BaseAchievementStat : MonoBehaviour
    {

        public bool HasAchievement
        {
            get { return haveThisAchievement; }
            set { haveThisAchievement = value; }
        }

        public bool HasBeenClaimed
        {
            get { return hasBeenClaimed; }
            set { hasBeenClaimed = value; }
        }

        public string AchievementName
        {
            get { return achievementName; }
            set
            {
                achievementName = value;
                // remember to change the text
                textAchievementName.text = achievementName;
            }
        }

        public string AchievementDescription
        {
            get { return achievementDescription; }
            set
            {
                achievementDescription = value;
                // remember to change the text
                textAchievementDescription.text = achievementDescription;
            }
        }

        private void Start()
        {
            textAchievementName = transform.Find("Name")?.GetComponent<TextMeshProUGUI>();
            textAchievementDescription = transform.Find("Description")?.GetComponent<TextMeshProUGUI>();

            textAchievementName.text = achievementName;
            textAchievementDescription.text = achievementDescription;
        }

        [SerializeField] private TextMeshProUGUI textAchievementName;
        [SerializeField] private TextMeshProUGUI textAchievementDescription;
        [SerializeField] private string achievementName;
        [SerializeField] private string achievementDescription;
        [SerializeField] private bool haveThisAchievement;
        [SerializeField] private bool hasBeenClaimed;

    }
}