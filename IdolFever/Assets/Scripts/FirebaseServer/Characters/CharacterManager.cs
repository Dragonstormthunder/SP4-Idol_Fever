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

        [SerializeField] private List<GameObject> contentCharacters;

        [EnumNamedArray(typeof(CharacterFactory.eCHARACTER))]
        public GameObject[] thumbnailPrefabs = new GameObject[(int)CharacterFactory.eCHARACTER.NUM_CHARACTER];

        #endregion

        void Start()
        {

            StartCoroutine(serverDatabase.GrabCharacters((characters) =>
            {

                Debug.Log("Manager: " + characters.Count);

                for (int i = 0; i < characters.Count; ++i)
                {

                    Debug.Log("Character: " + characters[i].Key + ", " + characters[i].Value);

                    GameObject characterObject = Instantiate(thumbnailPrefab, characterLocation.transform, false);
                    characterObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);

                    contentCharacters.Add(characterObject);

                    //// place into the content
                    //characterObject.transform.parent = characterLocation;

                    // add the image prefab to it
                    // find the parent object first
                    GameObject CircleMask = characterObject.transform.Find("CircleMask").gameObject;

                    Button thumbnailButton = characterObject.GetComponent<Button>();
                    thumbnailButton.targetGraphic = CircleMask.GetComponent<Image>();

                    CharacterViewButtonSwitch characterViewButtonSwitch = characterObject.GetComponent<CharacterViewButtonSwitch>();

                    characterViewButtonSwitch.characterViewManager = characterViewManager;
                    

                    // inefficient loop code
                    for (CharacterFactory.eCHARACTER index = 0; index < CharacterFactory.eCHARACTER.NUM_CHARACTER; ++index)
                    {

                        if (index.ToString() == characters[i].Key)
                        {
                            if (i == 0)
                            {
                                characterViewManager.SetWhichImageActive(index);
                            }
                            GameObject thumbnail = Instantiate(thumbnailPrefabs[(int)index], CircleMask.transform, false);
                            characterViewButtonSwitch.index = index;
                            break;
                        }
                    }

                }

            }));

        }

    }
}