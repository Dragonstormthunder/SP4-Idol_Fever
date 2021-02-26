//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//namespace IdolFever.Server
//{
//    public class AmtImage : MonoBehaviour
//    {
//        #region Fields

//        //public ServerDatabase serverDatabase;
//        public GameObject ProgressPrefab;
//        public Transform ProgressLocation;
      

//        [SerializeField] private List<GameObject> taskProgress;
//        #endregion

//        //public enum AmtOfBar
//        //{
//        //    FIVE,
//        //    ONE,
//        //    ONE,
//        //    NUM_TASKTYPE
//        //}

//        //public enum RewardType
//        //{
//        //    GEMS_100,
//        //    GEMS_300,
//        //    GEMS_500,
//        //    NUM_REWARDTYPE
//        //}

//        //public string Taskname;
//        //public string ID;
//        //public string displayTxt;
//        //public string reward;
//        //public bool taskCompleted;


//        void Start()
//        {

//            //StartCoroutine(serverDatabase.UpdateTasks(TaskType.COMPLETE_ONE_GAME_OF_MULTI.ToString(), true));

//            // create all the prefabs for the achievements and place it in a list for easy use
//            List<GameObject> contentTask = new List<GameObject>();

//            for (DailyTask.TaskType i = 0; i < DailyTask.TaskType.NUM_TASKTYPE; ++i)
//            {
//                GameObject progress = Instantiate(ProgressPrefab, ProgressLocation.transform, false);            

//                // add it to the list
//                contentTask.Add(progress);

//            }          
//        }


//        // Update is called once per frame
//        void Update()
//        {
            
//        }
//    }
//}