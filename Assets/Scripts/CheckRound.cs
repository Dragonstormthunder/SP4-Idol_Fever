//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace IdolFever.Server
//{
//    public class CheckRound : MonoBehaviour
//    {
//        public ServerDatabase serverDatabase;
//        private int count = 0;
//        // Start is called before the first frame update
//        void Start()
//        {

//        }

//        // Update is called once per frame
//        void Update()
//        {
//            StartCoroutine(serverDatabase.UpdateRoundOfFive(count));

//            StartCoroutine(serverDatabase.GetRoundOfFive((progressOfFive) =>
//            {
//                Debug.Log("five: " + progressOfFive);
//                if (progressOfFive == 5)
//                {
//                    StartCoroutine(serverDatabase.UpdateTask(DailyTask.TaskType.COMPLETE_FIVE_GAME.ToString(), true));
//                    Debug.Log("fiveddd: " + progressOfFive);
//                }
//            }));
//        }

//        public void check()
//        {
//            count += 1;
//            Debug.Log("count: " + count);
//        }

//    }
//}