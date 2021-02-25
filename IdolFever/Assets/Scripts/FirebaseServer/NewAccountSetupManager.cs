using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IdolFever.Server
{
    // new account set up manager
    public class NewAccountSetupManager : MonoBehaviour
    {

        #region Fields

        public ServerDatabase serverDatabase;
        public TextMeshProUGUI gemText;

        #endregion

        #region Properties
        #endregion

        #region Unity Messages

        private void Start()
        {

            // this also helps repair accounts in some manner
            StartCoroutine(serverDatabase.IsCharacterValuePresent((hasCharacters) =>
            {

                if (!hasCharacters)
                {
                    StartCoroutine(serverDatabase.UpdateCharacters(Character.CharacterFactory.eCHARACTER.R_CHARACTER_BOY0.ToString(), 1));
                    StartCoroutine(serverDatabase.UpdateCharacters(Character.CharacterFactory.eCHARACTER.R_CHARACTER_GIRL0.ToString(), 1));
                    StartCoroutine(serverDatabase.UpdateGems(500));
                    gemText.text = "500";
                }

            }));

        }

        #endregion

    }
}