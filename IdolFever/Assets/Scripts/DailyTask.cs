using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IdolFever.Server
{
    public class DailyTask : MonoBehaviour
    {
        #region Fields

        public ServerDatabase serverDatabase;
        public GameObject taskPrefab;
        public Transform taskLocation;

        [SerializeField] private List<GameObject> contentTask;

        #endregion

        public enum TaskType
        {
            COMPLETE_ONE_GAME_OF_MULTI,
            COMPLETE_FIVE_GAME,
            COMPLETE_ALL_TASK,
            NUM_TASKTYPE
        }

        public enum RewardType
        {
            GEMS_100,
            GEMS_300,
            GEMS_500,
            NUM_REWARDTYPE
        }

        //public string Taskname;
        //public string ID;
        //public string displayTxt;
        //public string reward;
        //public bool taskCompleted;


        void Start()
        {

            //StartCoroutine(serverDatabase.UpdateAchievements(ListAchievements.COMPLETE_THE_TUTORIAL.ToString(), true));

            // create all the prefabs for the achievements and place it in a list for easy use
            List<GameObject> contentTask = new List<GameObject>();

            string t_name = "";
            string t_description = "";

            for (TaskType i = 0; i < TaskType.NUM_TASKTYPE; ++i)
            {
                GameObject task = Instantiate(taskPrefab, taskLocation.transform, false);

                // name the achievements
                //Transform achievementName = achievement.transform.Find("TaskMessage");
                //Transform achievementDescription = achievement.transform.GetChild(0).transform.Find("progressTxt");

                t_description = TaskFactory.TaskDescriptions(i, ref t_name);

                //achievementName.GetComponent<TextMeshProUGUI>().text = name;
                //achievementDescription.GetComponent<TextMeshProUGUI>().text = description;

                GameObject taskName = task.transform.Find("TaskMessage").gameObject;
                taskName.GetComponent<TextMeshProUGUI>().text = t_name;


                // add it to the list
                contentTask.Add(task);

            }


            //// start a coroutine to get the achievements
            //// use a callback to obtain the achievements and do something with it
            //StartCoroutine(serverDatabase.GrabTask((task) =>
            //{

            //    Debug.Log("Here: " + task.Count);

            //    // we have all the achievements the player has


            //    // if there are no acheivements, this loop will fail
            //    for (int i = 0; i < task.Count; ++i)
            //    {

            //        Debug.Log(task[i]);

            //        // need to find the index where it is
            //        TaskType index = 0;
            //        for (index = 0; index < TaskType.NUM_TASKTYPE; ++i)
            //        {
            //            if (index.ToString() == task[i])
            //            {
            //                // disable the button for the reward
            //                Transform button = contentTask[(int)index].transform.GetChild(0).transform.Find("ButtonClaimed");
            //                button.gameObject.SetActive(false);
            //                break;
            //            }
            //        }
            //    }

            //}));

        }

        // Update is called once per frame
        void Update()
        {          
        }
    }
}