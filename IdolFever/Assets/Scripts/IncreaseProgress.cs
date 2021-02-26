using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using IdolFever.UI;


using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Linq;

namespace IdolFever.Server
{
    public class IncreaseProgress : MonoBehaviour
    {
        #region FIREBASE

        //Firebase variables
        [Header("Firebase")]
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;
        public FirebaseUser User;
        public DatabaseReference DBreference;

        #endregion
        public DailyManager dm;

        private void Init()
        {
            auth = FirebaseAuth.DefaultInstance;
            User = FirebaseAuth.DefaultInstance.CurrentUser;
            DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        }

        private void Start()
        {
            StaticDataStorage.nowTime = getDate() + "";

            StartCoroutine(dm.GetTaskUTC((progress) =>
            {
                Debug.Log("sucess: get taskUTC");
            }));

        }


        public void IncreaseRoundPlayed()
        {
            if (Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextRound))
            {
                Debug.Log("solo++");
                StaticDataStorage.roundPlayed++;
                StartCoroutine(dm.UpdateProgress(StaticDataStorage.roundPlayed, StaticDataStorage.roundMulti));
            }


        }

        public void IncreaseMultiRounds()
        {
            if (Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextMulti))
            {

                StaticDataStorage.roundMulti++;

                StartCoroutine(dm.UpdateProgress(StaticDataStorage.roundPlayed, StaticDataStorage.roundMulti));
            }


        }


        int getDate()
        {
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            Int32 cur_time = (Int32)(System.DateTime.UtcNow - epochStart).TotalSeconds;
            Debug.LogWarning("nowTime: " + cur_time);

            return cur_time;
        }
    }
}
