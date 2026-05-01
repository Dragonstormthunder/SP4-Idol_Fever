using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IdolFever.UI;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Linq;
using TMPro;

namespace IdolFever.Server
{

    public class DailyManager : MonoBehaviour
    {

        #region DATABASE_KEYS

        public const string DATABASE_TASK = "TASK";
        public const string DATABASE_TASK_DONE = "TASK_DONE";
        public const string DATABASE_TOTAL_ROUND_PLAYED = "TOTAL_ROUND_PLAYED";
        //////////////////////////////////////////////////////////
        public const string DATABASE_TOTAL_ROUND_MULTI = "TOTAL_ROUND_MULTI";
        public const string DATABASE_TOTAL_ALL_DONE = "TOTAL_ALL_DONE";

        public const string DATABASE_MULTI_UTC = "MULTI_UTC";
        public const string DATABASE_FIVEROUND_UTC = "FIVEROUND_UTC";
        public const string DATABASE_ALLDONE_UTC = "ALLDONE_UTC";

        public ManOfUI ui;

        public const string DATABASE_TOTAL_RGIRLS_DRAWN = "TOTAL_RGIRLS_DRAWN";
        public const string DATABASE_TOTAL_RBOYS_DRAWN = "TOTAL_RBOYS_DRAWN";
        public const string DATABASE_TOTAL_SRGIRLS_DRAWN = "TOTAL_SRGIRLS_DRAWN";
        public const string DATABASE_TOTAL_SRBOYS_DRAWN = "TOTAL_SRBOYS_DRAWN";
        public const string DATABASE_TOTAL_SSRGIRLS_DRAWN = "TOTAL_SSRGIRLS_DRAWN";
        public const string DATABASE_TOTAL_SSRBOYS_DRAWN = "TOTAL_SSRBOYS_DRAWN";

        #endregion

        #region FIREBASE

        //Firebase variables
        [Header("Firebase")]
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;
        public FirebaseUser User;
        public DatabaseReference DBreference;

        #endregion

        void Awake()
        {
            Init();
        }

        private void Init()
        {

            auth = FirebaseAuth.DefaultInstance;
            User = FirebaseAuth.DefaultInstance.CurrentUser;
            DBreference = FirebaseDatabase.DefaultInstance.RootReference;
            Debug.Log("daily called");

            //StartCoroutine(UpdateProgress(5, 2));

            StartCoroutine(GetTaskUTC((progress) =>
            {
                Debug.Log("sucess utc get");

            }));


            StartCoroutine(GetProgress((progress) =>
            {
                Debug.Log("sucess: " + StaticDataStorage.roundPlayed);

                if (ui != null)
                    ui.UpdateFills();

            }));
        }


        //public IEnumerator UpdateCharacter(int R_Girl, int SR_Girl, int SSR_Girl, int R_Boy, int SR_Boy, int SSR_Boy)
        //{
        //    // update the value of the round 
        //    // will create here if it doesn't exist
        //    var DBTaskRG = DBreference.Child("users").Child(User.UserId).Child(DATABASE_TOTAL_RGIRLS_DRAWN).SetValueAsync(R_Girl);
        //    var DBTaskRB = DBreference.Child("users").Child(User.UserId).Child(DATABASE_TOTAL_RBOYS_DRAWN).SetValueAsync(R_Boy);

        //    var DBTaskSRG = DBreference.Child("users").Child(User.UserId).Child(DATABASE_TOTAL_SRGIRLS_DRAWN).SetValueAsync(SR_Girl);
        //    var DBTaskSRB = DBreference.Child("users").Child(User.UserId).Child(DATABASE_TOTAL_SRBOYS_DRAWN).SetValueAsync(SR_Boy);

        //    var DBTaskSSRG = DBreference.Child("users").Child(User.UserId).Child(DATABASE_TOTAL_SSRGIRLS_DRAWN).SetValueAsync(SSR_Girl);
        //    var DBTaskSSRB = DBreference.Child("users").Child(User.UserId).Child(DATABASE_TOTAL_SSRBOYS_DRAWN).SetValueAsync(SSR_Boy);


        //    yield return new WaitUntil(predicate: () => DBTaskRG.IsCompleted);
        //    yield return new WaitUntil(predicate: () => DBTaskRB.IsCompleted);
        //    yield return new WaitUntil(predicate: () => DBTaskSRG.IsCompleted);
        //    yield return new WaitUntil(predicate: () => DBTaskSRB.IsCompleted);
        //    yield return new WaitUntil(predicate: () => DBTaskSSRG.IsCompleted);
        //    yield return new WaitUntil(predicate: () => DBTaskSSRB.IsCompleted);


        //    // error
        //    if (DBTaskRG.Exception != null)
        //    {
        //        Debug.LogWarning(message: $"Failed to register task with {DBTaskRG.Exception}");
        //    }
        //    else if (DBTaskRB.Exception != null)
        //    {
        //        Debug.LogWarning(message: $"Failed to register task with {DBTaskRB.Exception}");
        //    }
        //    else if (DBTaskSRG.Exception != null)
        //    {
        //        Debug.LogWarning(message: $"Failed to register task with {DBTaskSRG.Exception}");
        //    }
        //    else if (DBTaskSRB.Exception != null)
        //    {
        //        Debug.LogWarning(message: $"Failed to register task with {DBTaskSRB.Exception}");
        //    }
        //    else if (DBTaskSSRG.Exception != null)
        //    {
        //        Debug.LogWarning(message: $"Failed to register task with {DBTaskRG.Exception}");
        //    }
        //    else if (DBTaskSSRB.Exception != null)
        //    {
        //        Debug.LogWarning(message: $"Failed to register task with {DBTaskSSRB.Exception}");
        //    }
        //}

        //public IEnumerator GetCharacter(System.Action<int> callbackOnFinish)
        //{
        //    Debug.Log("Progress called");
        //    var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        //    yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        //    if (DBTask.Exception != null)
        //    {
        //        Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        //    }
        //    else if (DBTask.Result.Value == null)
        //    {
        //        callbackOnFinish(0);
        //    }
        //    else
        //    {
        //        //Debug.Log("Snapshot Progress");
        //        DataSnapshot snapshot = DBTask.Result;

        //        List<DataSnapshot> dataSnapshots = snapshot.Children.ToList();

        //        if (snapshot.HasChild(DATABASE_TOTAL_RGIRLS_DRAWN))
        //        {
        //            //Debug.Log("Able to access data");
        //            int value = int.Parse(snapshot.Child(DATABASE_TOTAL_RGIRLS_DRAWN).Value.ToString());

        //            Debug.LogWarning("value: " + value);
        //            StaticDataStorage.R_GirlDrawCount = value;




        //            callbackOnFinish(value);
        //        }
        //        else
        //        {
        //            //Debug.Log("Unable to access data ");
        //            callbackOnFinish(0);
        //        }

        //        if (snapshot.HasChild(DATABASE_TOTAL_RBOYS_DRAWN))
        //        {
        //            Debug.Log("Able to access data multi");
        //            int value = int.Parse(snapshot.Child(DATABASE_TOTAL_RBOYS_DRAWN).Value.ToString());

        //            Debug.LogWarning("value multi: " + value);
        //            StaticDataStorage.R_BoyDrawCount = value;




        //            callbackOnFinish(value);
        //        }
        //        else
        //        {
        //            //Debug.Log("Unable to access data multi");
        //            callbackOnFinish(0);
        //        }

        //        if (snapshot.HasChild(DATABASE_TOTAL_SRGIRLS_DRAWN))
        //        {
        //            //Debug.Log("Able to access data");
        //            int value = int.Parse(snapshot.Child(DATABASE_TOTAL_SRGIRLS_DRAWN).Value.ToString());

        //            Debug.LogWarning("value: " + value);
        //            StaticDataStorage.SR_GirlDrawCount = value;




        //            callbackOnFinish(value);
        //        }
        //        else
        //        {
        //            //Debug.Log("Unable to access data ");
        //            callbackOnFinish(0);
        //        }

        //        if (snapshot.HasChild(DATABASE_TOTAL_SRBOYS_DRAWN))
        //        {
        //            Debug.Log("Able to access data multi");
        //            int value = int.Parse(snapshot.Child(DATABASE_TOTAL_SRBOYS_DRAWN).Value.ToString());

        //            Debug.LogWarning("value multi: " + value);
        //            StaticDataStorage.SR_BoyDrawCount = value;




        //            callbackOnFinish(value);
        //        }
        //        else
        //        {
        //            //Debug.Log("Unable to access data multi");
        //            callbackOnFinish(0);
        //        }

        //        if (snapshot.HasChild(DATABASE_TOTAL_SSRGIRLS_DRAWN))
        //        {
        //            //Debug.Log("Able to access data");
        //            int value = int.Parse(snapshot.Child(DATABASE_TOTAL_SSRGIRLS_DRAWN).Value.ToString());

        //            Debug.LogWarning("value: " + value);
        //            StaticDataStorage.SSR_GirlDrawCount = value;




        //            callbackOnFinish(value);
        //        }
        //        else
        //        {
        //            //Debug.Log("Unable to access data ");
        //            callbackOnFinish(0);
        //        }

        //        if (snapshot.HasChild(DATABASE_TOTAL_SSRBOYS_DRAWN))
        //        {
        //            Debug.Log("Able to access data multi");
        //            int value = int.Parse(snapshot.Child(DATABASE_TOTAL_SSRBOYS_DRAWN).Value.ToString());

        //            Debug.LogWarning("value multi: " + value);
        //            StaticDataStorage.SSR_BoyDrawCount = value;




        //            callbackOnFinish(value);
        //        }
        //        else
        //        {
        //            //Debug.Log("Unable to access data multi");
        //            callbackOnFinish(0);
        //        }

        //    }

        //}


        public IEnumerator UpdateProgress(int progressOfFive, int progressMulti)
        {
            // update the value of the round 
            // will create here if it doesn't exist
            var DBTask = DBreference.Child("users").Child(User.UserId).Child(DATABASE_TOTAL_ROUND_PLAYED).SetValueAsync(progressOfFive);
            var DBTaskMulti = DBreference.Child("users").Child(User.UserId).Child(DATABASE_TOTAL_ROUND_MULTI).SetValueAsync(progressMulti);


            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
            yield return new WaitUntil(predicate: () => DBTaskMulti.IsCompleted);

            // error
            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else if (DBTaskMulti.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTaskMulti.Exception}");
            }
        }

        public IEnumerator GetProgress(System.Action<int> callbackOnFinish)
        {
            Debug.Log("Progress called");
            var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else if (DBTask.Result.Value == null)
            {
                callbackOnFinish(0);
            }
            else
            {
                //Debug.Log("Snapshot Progress");
                DataSnapshot snapshot = DBTask.Result;

                List<DataSnapshot> dataSnapshots = snapshot.Children.ToList();

                if (snapshot.HasChild(DATABASE_TOTAL_ROUND_PLAYED))
                {
                    //Debug.Log("Able to access data");
                    int value = int.Parse(snapshot.Child(DATABASE_TOTAL_ROUND_PLAYED).Value.ToString());

                    Debug.LogWarning("value: " + value);
                    StaticDataStorage.roundPlayed = value;




                    callbackOnFinish(value);
                }
                else
                {
                    //Debug.Log("Unable to access data ");
                    callbackOnFinish(0);
                }

                if (snapshot.HasChild(DATABASE_TOTAL_ROUND_MULTI))
                {
                    Debug.Log("Able to access data multi");
                    int value = int.Parse(snapshot.Child(DATABASE_TOTAL_ROUND_MULTI).Value.ToString());

                    Debug.LogWarning("value multi: " + value);
                    StaticDataStorage.roundMulti = value;




                    callbackOnFinish(value);
                }
                else
                {
                    //Debug.Log("Unable to access data multi");
                    callbackOnFinish(0);
                }



            }

        }




        public IEnumerator UpdateTaskUTC(string utc, int task)
        {
            // update the value of the round 
            // will create here if it doesn't exist
            if (task == 1)
            {
                Debug.Log(task + " send");
                var DBTask = DBreference.Child("users").Child(User.UserId).Child(DATABASE_MULTI_UTC).SetValueAsync(utc);
                yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
                // error
                if (DBTask.Exception != null)
                {
                    Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
                }

            }
            else if (task == 2)
            {
                var DBTask = DBreference.Child("users").Child(User.UserId).Child(DATABASE_FIVEROUND_UTC).SetValueAsync(utc);
                yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
            }
            else
            {
                var DBTask = DBreference.Child("users").Child(User.UserId).Child(DATABASE_ALLDONE_UTC).SetValueAsync(utc);
                yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
            }
        }



        public IEnumerator GetTaskUTC(System.Action<int> callbackOnFinish)
        {
            Debug.Log("getRound called");
            var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else if (DBTask.Result.Value == null)
            {
                callbackOnFinish(0);
            }
            else
            {
                //Debug.Log("Snapshot TaskUTC");
                DataSnapshot snapshot = DBTask.Result;

                List<DataSnapshot> dataSnapshots = snapshot.Children.ToList();

                if (snapshot.HasChild(DATABASE_MULTI_UTC))
                {
                    //Debug.Log("Able to access data");
                    string value = snapshot.Child(DATABASE_MULTI_UTC).Value.ToString();

                    StaticDataStorage.nextMulti = value;




                    callbackOnFinish(1);
                }
                else
                {
                    //Debug.Log("Unable to access data ");
                    callbackOnFinish(0);
                }

                if (snapshot.HasChild(DATABASE_FIVEROUND_UTC))
                {
                    //Debug.Log("Able to access data");
                    string value = snapshot.Child(DATABASE_FIVEROUND_UTC).Value.ToString();

                    StaticDataStorage.nextRound = value;


                    callbackOnFinish(1);
                }
                else
                {
                    //Debug.Log("Unable to access data ");
                    callbackOnFinish(0);
                }


                if (snapshot.HasChild(DATABASE_ALLDONE_UTC))
                {
                    //Debug.Log("Able to access data");
                    string value = snapshot.Child(DATABASE_ALLDONE_UTC).Value.ToString();

                    StaticDataStorage.nextAll = value;


                    callbackOnFinish(1);
                }
                else
                {
                    //Debug.Log("Unable to access data ");
                    callbackOnFinish(0);
                }


            }

        }
    }
}


