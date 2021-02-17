using System.Collections;
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

        // a dozen of keys
        public const string DATABASE_USERS = "users";
        public const string DATABASE_GEM = "GEM";
        public const string DATABASE_ACHIEVEMENT = "ACHIEVEMENT";
        public const string DATABASE_ACHIEVEMENT_CLAIMED = "CLAIMED";
        public const string DATABASE_CHARACTER = "CHARACTER";
        public const string DATABASE_CHARACTER_BONUS = "BONUS";

        //Firebase variables
        [Header("Firebase")]
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;
        public FirebaseUser User;
        public DatabaseReference DBreference;

        [Header("UI")]
        public TextMeshProUGUI numberOfGems;

        void Awake()
        {
            Init();
        }

        private void Init()
        {
            auth = FirebaseAuth.DefaultInstance;
            User = FirebaseAuth.DefaultInstance.CurrentUser;
            DBreference = FirebaseDatabase.DefaultInstance.RootReference;

            StartCoroutine(HasAchievementBeenClaimed("Wolf", (hasIt) =>
            {
                numberOfGems.text = hasIt.ToString();
            }));

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

        public IEnumerator UpdateCharacters(string characterName, int bonus)
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
    }

}

