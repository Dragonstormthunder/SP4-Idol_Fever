using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IdolFever.Character;

namespace IdolFever.Server.Characters
{

    public class CharacterManager : MonoBehaviour
    {
        #region Fields

        public ServerDatabase serverDatabase;
        public GameObject thumbnailPrefab;
        public Transform characterLocation;
        public CharacterViewManager characterViewManager;

        // character prefabs

        //[SerializeField] private List<GameObject> contentCharacters;

        [EnumNamedArray(typeof(CharacterFactory.eCHARACTER))]
        public GameObject[] thumbnailPrefabs = new GameObject[(int)CharacterFactory.eCHARACTER.NUM_CHARACTER];

        #endregion

        void Start()
        {

            StartCoroutine(serverDatabase.GrabCharacters((characters) =>
            {

                for (int i = 0; i < characters.Count; ++i)
                {

                    Debug.Log("Character: " + characters[i].Key + ", " + characters[i].Value);

                    // instantiate it in the content panel
                    GameObject characterObject = Instantiate(thumbnailPrefab, characterLocation.transform, false);
                    characterObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);

                    // add the image prefab to it
                    // find the parent object first
                    GameObject CircleMask = characterObject.transform.Find("CircleMask").gameObject;

                    Button thumbnailButton = characterObject.GetComponent<Button>();

                    // inefficient loop code
                    for (CharacterFactory.eCHARACTER index = 0; index < CharacterFactory.eCHARACTER.NUM_CHARACTER; ++index)
                    {
                        if (index.ToString() == characters[i].Key)
                        {
                            GameObject thumbnail = Instantiate(thumbnailPrefabs[(int)index], CircleMask.transform, false);

                            // set it to the correct index for the event messaging system
                            thumbnailButton.GetComponent<CharacterViewButtonSwitch>().CharacterIndex = index;

                            StartCoroutine(serverDatabase.NumberOfCharacters(characters[i].Key, (numberOfCharas) =>
                            {

                                //Debug.Log("Character: " + index.ToString() + " has " + numberOfCharas);

                                CharacterDataDisplay characterDataDisplay = thumbnailButton.GetComponent<CharacterDataDisplay>();
                                characterDataDisplay.Bonus = numberOfCharas;
                                characterDataDisplay.SetDescription(index);

                            }));

                            //characterViewButtonSwitch.index = index;
                            break;  // don't be more inefficient that this already is
                                    // break it since it has already been found
                        }
                    }

                }

            }));

        }

    }
}