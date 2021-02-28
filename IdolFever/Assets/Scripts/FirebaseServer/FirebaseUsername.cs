using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IdolFever.Server
{
    public class FirebaseUsername : MonoBehaviour
    {

        #region Fields

        public ServerDatabase serverDatabase;
        public TextMeshProUGUI usernameText;

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            // update the number of username
            Debug.Log("Start username coroutine");
            StartCoroutine(serverDatabase.GetUsername((username) =>
            {
                Debug.Log("Getting username");
                usernameText.text = username;
            }));
        }

    }

}
