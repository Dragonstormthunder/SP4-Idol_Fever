using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;

namespace IdolFever.Server
{
    public class FirebaseLogOut : MonoBehaviour
    {
        #region Fields
        static FirebaseLogOut INSTANCE;
        #endregion

        #region Properties
        #endregion

        #region Unity Messages

        private void Awake()
        {
            // make sure only one is created
            if (INSTANCE == null)
            {
                DontDestroyOnLoad(this);
                INSTANCE = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnApplicationQuit()
        {
            // sign out the user
            FirebaseAuth.DefaultInstance.SignOut();
            Debug.Log("Signed out");
        }

        #endregion

    }
}

