using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever.Server.Characters
{

    public class CharacterManager : MonoBehaviour
    {
        #region Fields

        public ServerDatabase serverDatabase;
        public GameObject thumbnailPrefab;
        public Transform characterLocation;

        // character prefabs

        [SerializeField] private List<GameObject> contentCharacters;

        #endregion

        void Start()
        {

            StartCoroutine(serverDatabase.GrabCharacters((characters) =>
            {

                Debug.Log("Manager: " + characters.Count);

                for (int i = 0; i < characters.Count; ++i)
                {


                    Debug.Log("Character: " + characters[i].Key + ", " + characters[i].Value);

                    GameObject characterObject = Instantiate(thumbnailPrefab, new Vector2(0, 0), Quaternion.identity);

                    contentCharacters.Add(characterObject);

                    // place into the list
                    characterObject.transform.parent = characterLocation;

                }

            }));

        }

    }
}