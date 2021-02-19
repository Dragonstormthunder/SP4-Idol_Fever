using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;

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

        // UI Buttons
        public Button logoutButton;
        public Button deleteAccountButton;

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

                    Debug.Log("User deleted successfully.");

                    // go back to login screen
                    SceneManager.LoadScene("LoginScene");

                });
            }

        }
    }

}
