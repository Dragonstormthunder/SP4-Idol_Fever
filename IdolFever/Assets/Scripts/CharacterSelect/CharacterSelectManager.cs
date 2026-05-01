using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IdolFever.Server;
using TMPro;

using IdolFever;

namespace IdolFever.Character
{

    public class CharacterSelectManager : MonoBehaviour
    {

        #region Fields

        public ServerDatabase serverDatabase;
        public CharacterDecentralizeData characterDecentralizeData;
        public GameObject selectCharacterPrefab;
        public Transform characterPanel;
        public ScrollRect scrollRect;

        [SerializeField] private AsyncSceneTransitionOut asyncSceneTransitionOutScript = null;

        #endregion


        #region Properties
        #endregion


        #region Unity Messages

        void Start()
        {

            StartCoroutine(serverDatabase.GrabCharacters((characters) =>
            {

                for (int i = 0; i < characters.Count; ++i)
                {

                    Debug.Log("Character: " + characters[i].Key + ", " + characters[i].Value + "Begin");

                    // instantiate it in the content panel
                    GameObject selectCharacterObject = Instantiate(selectCharacterPrefab, characterPanel.transform, false);

                    // for the thumbnail
                    GameObject mask = selectCharacterObject.transform.Find("CharacterThumbnailIcon").gameObject.transform.GetChild(1).gameObject;

                    GameObject selectButtonGO = selectCharacterObject.transform.Find("SelectButton").gameObject;
                    Button button = selectButtonGO.GetComponent<Button>();
                    SelectCharacterButton selectCharButtonScript = button.GetComponent<SelectCharacterButton>();
                    selectCharButtonScript.AsyncSceneTransitionOutScript = asyncSceneTransitionOutScript;


                    // inefficient loop code
                    for (CharacterFactory.eCHARACTER index = 0; index < CharacterFactory.eCHARACTER.NUM_CHARACTER; ++index)
                    {
                        // trying to find its index
                        if (index.ToString() == characters[i].Key)
                        {

                            // add the image prefab to it
                            GameObject thumbnail = Instantiate(characterDecentralizeData.AccessThumbnailPrefab(index), mask.transform);

                            TextMeshProUGUI skillName = selectCharacterObject.transform.Find("CharacterSkillName").GetComponent<TextMeshProUGUI>();
                            TextMeshProUGUI skillDescription = selectCharacterObject.transform.Find("CharacterSkillDescription").GetComponent<TextMeshProUGUI>();

                            skillName.text = characterDecentralizeData.AccessCharacterSkillName(index);

                            skillDescription.text = characterDecentralizeData.AccessCharacterSkillDescription(index, characters[i].Value);

                            // for the button
                            GameObject selectButton = selectCharacterObject.transform.Find("SelectButton").gameObject;
                            SelectCharacterButton selectCharacterButton = selectButton.GetComponent<SelectCharacterButton>();
                            selectCharacterButton.CharacterIndex = index;
                            selectCharacterButton.CharacterBonus = characters[i].Value;

                            //StartCoroutine(serverDatabase.NumberOfCharacters(characters[i].Key, (number) =>
                            //{
                            //    skillDescription.text = characterDecentralizeData.AccessCharacterSkillDescription(index, number);
                            //    // for the button
                            //    GameObject selectButton = selectCharacterObject.transform.Find("SelectButton").gameObject;
                            //    SelectCharacterButton selectCharacterButton = selectButton.GetComponent<SelectCharacterButton>();
                            //    selectCharacterButton.CharacterIndex = index;
                            //    selectCharacterButton.CharacterBonus = number;

                            //}));


                            break;  // don't be more inefficient that this already is
                                    // break it since it has already been found
                        }
                    }

                    Debug.Log("Character: " + characters[i].Key + ", " + characters[i].Value + "End B4 Co-routine");

                    StartCoroutine(YieldOneFrame());

                    Debug.Log("Character: " + characters[i].Key + ", " + characters[i].Value + "End AFT Co-routine");

                }

            }));


        }

        #endregion

        private IEnumerator YieldOneFrame()
        {
            yield return 0;

            scrollRect.verticalNormalizedPosition = 1;

        }

    }
}