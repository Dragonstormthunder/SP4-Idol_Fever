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
        public CharacterDecentralizeData characterDecentralizeData;

        // character prefabs

        //[SerializeField] private List<GameObject> contentCharacters;

        //[EnumNamedArray(typeof(CharacterFactory.eCHARACTER))]
        //public GameObject[] thumbnailPrefabs = new GameObject[(int)CharacterFactory.eCHARACTER.NUM_CHARACTER];

        #endregion

        void Start()
        {

            //for (CharacterFactory.eCHARACTER i = 0; i < CharacterFactory.eCHARACTER.NUM_CHARACTER; ++i)
            //{
            //    if (i == CharacterFactory.eCHARACTER.R_CHARACTER_BEGIN || i == CharacterFactory.eCHARACTER.R_CHARACTER_END ||
            //        i == CharacterFactory.eCHARACTER.SR_CHARACTER_BEGIN || i == CharacterFactory.eCHARACTER.SSR_CHARACTER_END ||
            //        i == CharacterFactory.eCHARACTER.SSR_CHARACTER_BEGIN || i == CharacterFactory.eCHARACTER.SSR_CHARACTER_END)
            //            continue;

            //    StartCoroutine(serverDatabase.UpdateCharacters(i.ToString(), 1));
            //}

            StartCoroutine(Initailize());

        }


        private IEnumerator Initailize()
        {
            // wait one frame, just in case the database hasn't started yet
            yield return 0;

            StartCoroutine(serverDatabase.GrabCharacters((characters) =>
            {

                for (int i = 0; i < characters.Count; ++i)
                {

                    Debug.Log("CharacterManager: Character: " + characters[i].Key + ", " + characters[i].Value);

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
                            GameObject thumbnail = Instantiate(characterDecentralizeData.AccessThumbnailPrefab(index), CircleMask.transform, false);

                            // set it to the correct index for the event messaging system

                            CharacterViewButtonSwitch characterViewButtonSwitch = thumbnailButton.GetComponent<CharacterViewButtonSwitch>();
                            characterViewButtonSwitch.CharacterIndex = index;


                            characterViewButtonSwitch.CharacterBonus = characters[i].Value;

                            //StartCoroutine(serverDatabase.NumberOfCharacters(characters[i].Key, (numberOfCharas) =>
                            //{

                            //    Debug.Log("Character: " + index.ToString() + " has " + numberOfCharas);

                            //    characterViewButtonSwitch.CharacterBonus = numberOfCharas;

                            //    //CharacterDataDisplay characterDataDisplay = thumbnailButton.GetComponent<CharacterDataDisplay>();
                            //    //characterDataDisplay.Bonus = numberOfCharas;
                            //    //characterDataDisplay.SetDescription(index);

                            //}));
                            Debug.Log("Character: " + index.ToString() + " has ended");
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