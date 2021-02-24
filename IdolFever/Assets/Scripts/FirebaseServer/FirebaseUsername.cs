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
            // update the number of gems
            StartCoroutine(serverDatabase.GetUsername((username) =>
            {
                usernameText.text = username;
            }));
        }

    }

}
