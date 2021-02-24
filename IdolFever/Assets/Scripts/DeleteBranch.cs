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

public class DeleteBranch : MonoBehaviour
{
    #region Fields

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;



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
        DBreference = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(User.UserId);

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeleteTaskDone()
    {
        if (User != null)
        {
            var DBTask = DBreference.Child("TASK").Child("DATABASE_TASK_DONE").RemoveValueAsync();

            //var DBTask = DBreference.Child("DATABASE_TASK").Child(User.UserId).RemoveValueAsync();


            User.DeleteAsync().ContinueWith(task =>
            {
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

                //// go back to login screen
                //SceneManager.LoadScene("LoginScene");

                //DeleteConfirmed();


            });
        }

        //// change back to login scene
        //SceneManager.LoadScene("LoginScene");
    }
}
