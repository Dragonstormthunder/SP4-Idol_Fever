//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;


////using Firebase;
////using Firebase.Auth;
////using Firebase.Database;
////using System.Linq;
////using System.Threading.Tasks;
////using UnityEngine.EventSystems;


//namespace IdolFever.Server
//{
//    public class DailyTask : MonoBehaviour
//    {
//        #region Fields

//        ////Firebase variables
//        //[Header("Firebase")]
//        //public DependencyStatus dependencyStatus;
//        //public FirebaseAuth auth;
//        //public FirebaseUser User;
//        //public DatabaseReference DBreference;

//        public ServerDatabase serverDatabase;
//        public GameObject taskPrefab;
//        public Transform taskLocation;
//        public TextMeshProUGUI progressText;
        


//        //[SerializeField] private List<GameObject> contentTask;

//        #endregion

//        public enum TaskType
//        {
//            COMPLETE_ONE_GAME_OF_MULTI,
//            COMPLETE_FIVE_GAME,
//            COMPLETE_ALL_TASK,
//            NUM_TASKTYPE
//        }

//        public enum RewardType
//        {
//            GEMS_100,
//            GEMS_300,
//            GEMS_500,
//            NUM_REWARDTYPE
//        }

//        void Start()
//        {
//            // yourimage = GameObject.Find("yourimage");


//            // create all the prefabs for the achievements and place it in a list for easy use
//            List<GameObject> contentTask = new List<GameObject>();

//            string t_name = "";
//            int t_progress = 0;
//            string t_description = "";

//            for (TaskType i = 0; i < TaskType.NUM_TASKTYPE; ++i)
//            {
//                GameObject task = Instantiate(taskPrefab, taskLocation.transform, false);

//                // name the achievements          
//                t_description = TaskFactory.TaskDescriptions(i, ref t_name, t_progress);

//                GameObject taskName = task.transform.Find("TaskMessage").gameObject;
//                taskName.GetComponent<TextMeshProUGUI>().text = t_name;


//                // add it to the list
//                contentTask.Add(task);

//            }


//            // start a coroutine to get the task
//            // use a callback to obtain the task and 
//            //check if task is done
//            //do something with it
//            StartCoroutine(serverDatabase.GrabTask((task) =>
//            {

//                Debug.Log("Here: " + task.Count);

//                // we have all the task the player has


//                // if there are no tASKS, this loop will fail
//                for (int i = 0; i < task.Count; ++i)
//                {
//                    Debug.Log(task[i]);

//                    // need to find the index where it is
//                    TaskType index = 0;

//                    //if (task[i] == "COMPLETE_ONE_GAME_OF_MULTI")
//                    //{
//                    //    Debug.Log("Get 100 Gems");
//                    //}
//                    //else if (task[i] == "COMPLETE_FIVE_GAME")
//                    //{
//                    //    Debug.Log("Get 200 Gems");
//                    //}
//                    //else if (task[i] == "COMPLETE_ALL_TASK")
//                    //{
//                    //    Debug.Log("Get 300 Gems");
//                    //}


//                    Debug.Log("INDEX:" + index);
//                    for (index = 0; index < TaskType.NUM_TASKTYPE; ++index)
//                    {
//                        if (index.ToString() == task[i])
//                        {
//                            // disable the button for the reward
//                            Transform button = contentTask[(int)index].transform.Find("ClaimButton");
//                            button.gameObject.SetActive(false);
//                            //enable ANOTHER button for reward
//                            Transform button2 = contentTask[(int)index].transform.Find("ClaimAlreadyButton");
//                            button2.gameObject.SetActive(true);

//                            //Debug.Log("INDEX IF LOOP:" + index);
//                            break;
//                        }
//                    }
//                }
//            }));
//        }

//        void Update()
//        {

//        }

//    }
//}