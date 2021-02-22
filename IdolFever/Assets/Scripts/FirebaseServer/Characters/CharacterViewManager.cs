using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IdolFever.Character;

namespace IdolFever.Server.Characters
{
    public class CharacterViewManager : MonoBehaviour
    {

        #region Fields

        [EnumNamedArray(typeof(CharacterFactory.eCHARACTER))]
        public GameObject[] splashViewImages;

        [SerializeField] private CharacterFactory.eCHARACTER index;

        #endregion

        #region Unity Messages

        private void Start()
        {
            // subscribe to the event
            CharacterStorageEvents.INSTANCE.onCharacterSwitched += OnSwitchImageActive;
        }

        private void OnDestroy()
        {
            // unsubscribe to the event
            CharacterStorageEvents.INSTANCE.onCharacterSwitched -= OnSwitchImageActive;
        }

        #endregion

        public void OnSwitchImageActive(CharacterFactory.eCHARACTER _newIndex)
        {
            //Debug.Log("Awful: " + _newIndex.ToString());

            // set the old image to inactive
            if (splashViewImages[(int)index] != null)
                splashViewImages[(int)index].SetActive(false);

            // set the index to the new one
            index = _newIndex;

            if (splashViewImages[(int)index] != null)
                splashViewImages[(int)index]?.SetActive(true);

        }

    }

}