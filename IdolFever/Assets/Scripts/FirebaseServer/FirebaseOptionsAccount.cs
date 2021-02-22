using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace IdolFever.Server
{
    public class FirebaseOptionsAccount : MonoBehaviour
    {

        #region Fields

        //Firebase variables
        [Header("Firebase")]
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;
        public FirebaseUser User;
        public DatabaseReference DBreference;

        // UI Buttons
        //public Button logoutButton;
        //public Button deleteAccountButton;

        public GameObject panel;
        public Button noButton;
        public Button yesButton;

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            // grab firebase for requirement needs
            auth = FirebaseAuth.DefaultInstance;
            User = FirebaseAuth.DefaultInstance.CurrentUser;
            DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        }

        public void LogOut()
        {
            // sign out the user
            auth.SignOut();

            // go back to login screen
            SceneManager.LoadScene("LoginScene");
        }


        public void DeleteAccount()
        {
            // if there's no user no need to delete
            if (User != null)
            {

                var DBTask = DBreference.Child("users").Child(User.UserId).RemoveValueAsync();

                User.DeleteAsync().ContinueWith(task => {
                    if (task.IsCanceled)
                    {
                        Debug.LogError("DeleteAsync was canceled.");
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        Debug.LogError("DeleteAsync encountered an error: " + task.Exception);
                        return;
                    }

                    //Debug.Log("User deleted successfully.");

                    //// go back to login screen
                    //SceneManager.LoadScene("LoginScene");

                    //DeleteConfirmed();


                });
            }

            // change back to login scene
            SceneManager.LoadScene("LoginScene");

        }

        public void CancelOperation()
        {
            panel.SetActive(false);
        }

        public void BringUpConfirmationPopUp()
        {
            panel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(panel);
        }

    }

}