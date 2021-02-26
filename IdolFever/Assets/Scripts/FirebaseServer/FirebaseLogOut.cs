using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

namespace IdolFever.Server
{
    public class FirebaseLogOut : MonoBehaviour
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Unity Messages

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit()
        {
            // sign out the user
            FirebaseAuth.DefaultInstance.SignOut();
        }

        #endregion

    }
}

