using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            StartCoroutine(serverDatabase.GetGems((gems) =>
            {
                numberOfGemsText.text = gems.ToString();
            }));
        }

    }

}
