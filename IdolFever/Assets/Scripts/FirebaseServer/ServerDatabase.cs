﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Linq;
using TMPro;

namespace IdolFever.Server
{

    public class ServerDatabase : MonoBehaviour
    {

        #region DATABASE_KEYS

        // a dozen of keys
        public const string DATABASE_USERS = "users";
        public const string DATABASE_GEM = "GEM";
        public const string DATABASE_ACHIEVEMENT = "ACHIEVEMENT";
        public const string DATABASE_ACHIEVEMENT_CLAIMED = "CLAIMED";
        public const string DATABASE_CHARACTER = "CHARACTER";
        public const string DATABASE_CHARACTER_NUMBER = "NUMBER";
        public const string DATABASE_ENERGY = "ENERGY";
        public const string DATABASE_LAST_LOGIN = "LAST_LOGIN";

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
        }

        public IEnumerator UpdateGems(int gems)
        {
            // update the value of the gem
            // will create here if it doesn't exist
            var DBTask = DBreference.Child(DATABASE_USERS).Child(User.UserId).Child(DATABASE_GEM).SetValueAsync(gems);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            // error
            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
        }

        public IEnumerator GetGems(System.Action<int> callbackOnFinish)
        {
            var DBTask = DBreference.Child(DATABASE_USERS).Child(User.UserId).GetValueAsync();

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
                Debug.Log("Snapshot Gems");
                DataSnapshot snapshot = DBTask.Result;

                List<DataSnapshot> dataSnapshots = snapshot.Children.ToList();

                if (snapshot.HasChild(DATABASE_GEM))
                {
                    Debug.Log("Able to access data");
                    int value = int.Parse(snapshot.Child(DATABASE_GEM).Value.ToString());
                    callbackOnFinish(value);
                }
                else
                {
                    Debug.Log("Unable to access data");
                    callbackOnFinish(0);
                }
            }

        }
        public IEnumerator UpdateEnergy(int energy)
        {
            // update the value of the gem
            // will create here if it doesn't exist
            var DBTask = DBreference.Child(DATABASE_USERS).Child(User.UserId).Child(DATABASE_ENERGY).SetValueAsync(energy);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            // error
            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
        }

        public IEnumerator GetEnergy(System.Action<int> callbackOnFinish)
        {
            var DBTask = DBreference.Child(DATABASE_USERS).Child(User.UserId).GetValueAsync();

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
                Debug.Log("Snapshot Energy");
                DataSnapshot snapshot = DBTask.Result;

                if (snapshot.HasChild(DATABASE_ENERGY))
                {
                    Debug.Log("Able to access data");
                    int value = int.Parse(snapshot.Child(DATABASE_GEM).Value.ToString());
                    callbackOnFinish(value);
                }
                else
                {
                    Debug.Log("Unable to access data");
                    callbackOnFinish(0);
                }
            }

        }

        public IEnumerator UpdateAchievements(string achievementName, bool hasClaimed)
        {
            //update the achievement
            //will create here if it doesn't exist
            var DBTask = DBreference.Child(DATABASE_USERS).Child(User.UserId).Child(DATABASE_ACHIEVEMENT).Child(achievementName).Child(DATABASE_ACHIEVEMENT_CLAIMED).SetValueAsync(hasClaimed);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
        }

        public IEnumerator GrabAchievements(System.Action<List<string>> callbackOnFinish)
        {
            List<string> achievements = new List<string>();

            var DBTask = DBreference.Child(DATABASE_USERS).Child(User.UserId).Child(DATABASE_ACHIEVEMENT).GetValueAsync();

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
                callbackOnFinish(achievements);
            }
            else if (DBTask.Result.Value == null)
            {
                Debug.Log("Achievement no result");
                callbackOnFinish(achievements);
            }
            else
            {
                Debug.Log("Snapshot Achievement");
                DataSnapshot snapshot = DBTask.Result;

                // grab all the children of the achievement
                List<DataSnapshot> snapshots = snapshot.Children.ToList();

                // insert all the children into a list to return
                for (int i = 0; i < snapshot.ChildrenCount; ++i)
                {
                    //string value = snapshots[i].Value.ToString();
                    //string value = snapshots[i].Key;
                    achievements.Add(snapshots[i].Key);
                }


                callbackOnFinish(achievements);

            }

        }

        public IEnumerator HasAchievement(string achievementName, System.Action<bool> callbackOnFinish)
        {
            var DBTask = DBreference.Child(DATABASE_USERS).Child(User.UserId).Child(DATABASE_ACHIEVEMENT).GetValueAsync();

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else if (DBTask.Result.Value == null)
            {
                Debug.Log("Achievement no result");
                callbackOnFinish(false);
            }
            else
            {
                Debug.Log("Snapshot Achievement");
                DataSnapshot snapshot = DBTask.Result;
                callbackOnFinish(snapshot.HasChild(achievementName));
            }

        }

        public IEnumerator HasAchievementBeenClaimed(string achievementName, System.Action<bool> callbackOnFinish)
        {
            var DBTask = DBreference.Child(DATABASE_USERS).Child(User.UserId).Child(DATABASE_ACHIEVEMENT).Child(achievementName).GetValueAsync();

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else if (DBTask.Result.Value == null)
            {
                Debug.Log("Achievement Claim no result");
                callbackOnFinish(false);
            }
            else
            {
                Debug.Log("Snapshot Achievement");
                DataSnapshot snapshot = DBTask.Result;

                callbackOnFinish(bool.Parse(snapshot.Child(DATABASE_ACHIEVEMENT_CLAIMED).Value.ToString()));
            }
        }

        public IEnumerator UpdateCharacters(string characterName, int number)
        {
            // update the characters
            // will create here if it doesn't exist
            var DBTask = DBreference.Child(DATABASE_USERS).Child(User.UserId).Child(DATABASE_CHARACTER).Child(characterName).Child(DATABASE_CHARACTER_NUMBER).SetValueAsync(number);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            // error
            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }

        }

        public IEnumerator NumberOfCharacters(string characterName, System.Action<int> callbackOnFinish)
        {

            var DBTask = DBreference.Child(DATABASE_USERS).Child(User.UserId).Child(DATABASE_CHARACTER).GetValueAsync();

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else if (DBTask.Result.Value == null)
            {
                Debug.Log("Characters no result");
                callbackOnFinish(0); // no character exists anyway so the character will be zero
            }
            else
            {
                // check if character name exists
                DBTask = DBreference.Child(DATABASE_USERS).Child(User.UserId).Child(DATABASE_CHARACTER).Child(characterName).GetValueAsync();

                yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

                if (DBTask.Exception != null)
                {
                    Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
                }
                else if (DBTask.Result.Value == null)
                {
                    Debug.Log("Character Name no result");
                    callbackOnFinish(0); // none of the character anyway
                }
                else
                {
                    Debug.Log("Snapshot Number of Characters");
                    DataSnapshot snapshot = DBTask.Result;

                    callbackOnFinish(int.Parse(snapshot.Child(DATABASE_CHARACTER_NUMBER).Value.ToString()));
                }
            }
        }

        public IEnumerator GrabCharacters(System.Action<List<KeyValuePair<string, int>>> callbackOnFinish)
        {
            List<KeyValuePair<string, int>> characters = new List<KeyValuePair<string, int>>();


            var DBTask = DBreference.Child(DATABASE_USERS).Child(User.UserId).Child(DATABASE_CHARACTER).GetValueAsync();

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
                callbackOnFinish(characters);
            }
            else if (DBTask.Result.Value == null)
            {
                Debug.Log("Character no result");
                callbackOnFinish(characters);
            }
            else
            {
                Debug.Log("Snapshot Grab Characters");
                DataSnapshot snapshot = DBTask.Result;

                // grab all the children of the achievement
                List<DataSnapshot> snapshots = snapshot.Children.ToList();


                // insert all the children into a list to return
                for (int i = 0; i < snapshot.ChildrenCount; ++i)
                {
                    Debug.Log("Snapshot Key: " + snapshots[i].Key);

                    string name = snapshots[i].Key;

                    Debug.Log("Snapshot Child: " + snapshots[i].Child(DATABASE_CHARACTER_NUMBER).Value.ToString());

                    int value = int.Parse(snapshots[i].Child(DATABASE_CHARACTER_NUMBER).Value.ToString());

                    KeyValuePair<string, int> retrievedCharacter = new KeyValuePair<string, int>(name, value);

                    characters.Add(retrievedCharacter);

                }

                callbackOnFinish(characters);

            }

        }

    }

}

