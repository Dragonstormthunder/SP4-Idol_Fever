using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using IdolFever.UI;

namespace IdolFever.Server
{
    public class InitializeGemCount : MonoBehaviour
    {

        #region Fields

        public ServerDatabase serverDatabase;
        public TextMeshProUGUI numberOfGemsText;

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Get Gems Start Coroutine");
            // update the number of gems
            StartCoroutine(serverDatabase.GetGems((gems) =>
            {
                Debug.Log("Get Gems Inside Coroutine");
                numberOfGemsText.text = gems.ToString();
                StaticDataStorage.gems = gems;
                Debug.Log("Get Gems Finish");
            }));
        }

    }

}
