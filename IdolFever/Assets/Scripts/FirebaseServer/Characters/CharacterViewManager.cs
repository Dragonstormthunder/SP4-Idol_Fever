using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IdolFever.Character;
using TMPro;

namespace IdolFever.Server.Characters
{
    public class CharacterViewManager : MonoBehaviour
    {

        #region Fields

        [EnumNamedArray(typeof(CharacterFactory.eCHARACTER))]
        public GameObject[] splashViewImages;

        [SerializeField] private CharacterFactory.eCHARACTER index;
        [SerializeField] CharacterDecentralizeData characterDecentralizeData;
        [SerializeField] private TextMeshProUGUI skillName;
        [SerializeField] private TextMeshProUGUI skillDescription;
        [SerializeField] private TextMeshProUGUI numberOfCharacters;

        #endregion

        #region Unity Messages

        private void Start()
        {
            // subscribe to the event
            CharacterStorageEvents.INSTANCE.onCharacterSwitched += OnSwitchImageActive;
            CharacterStorageEvents.INSTANCE.onCharacterSwitched += OnSwitchCharacterAttribute;

            skillName.text = "";
            skillDescription.text = "";
            numberOfCharacters.text = "";
        }

        private void OnDestroy()
        {
            // unsubscribe to the event
            CharacterStorageEvents.INSTANCE.onCharacterSwitched -= OnSwitchImageActive;
            CharacterStorageEvents.INSTANCE.onCharacterSwitched -= OnSwitchCharacterAttribute;

        }

        #endregion

        private void OnSwitchCharacterAttribute(KeyValuePair<CharacterFactory.eCHARACTER, int> _newIndex)
        {
            skillName.text = characterDecentralizeData.AccessCharacterSkillName(_newIndex.Key);
            skillDescription.text = characterDecentralizeData.AccessCharacterSkillDescription(_newIndex.Key, _newIndex.Value);
            numberOfCharacters.text = "Number Have: " + _newIndex.Value;
        }

        private void OnSwitchImageActive(KeyValuePair<CharacterFactory.eCHARACTER, int> _newIndex)
        {
            //Debug.Log("Awful: " + _newIndex.ToString());

            // set the old image to inactive
            if (splashViewImages[(int)index] != null)
                splashViewImages[(int)index].SetActive(false);

            // set the index to the new one
            index = _newIndex.Key;

            if (splashViewImages[(int)index] != null)
                splashViewImages[(int)index]?.SetActive(true);
        }

    }

}