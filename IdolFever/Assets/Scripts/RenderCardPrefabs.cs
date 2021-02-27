using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IdolFever.Server;
using IdolFever.Character;
using TMPro;

namespace IdolFever.UI
{
    public class RenderCardPrefabs : MonoBehaviour
    {
        public ServerDatabase database;

        public TextMeshProUGUI cardDrawnTxt;

        public GameObject Confetti;

        public GameObject gameObjectPanelBg;

        public GameObject CardBackPrefab;
        private GameObject CardBack_Clone;

        public GameObject R_imageGirlPrefab;
        private GameObject R_Girl_Clone;

        public GameObject R_imageBoyPrefab;
        private GameObject R_Boy_Clone;

        public GameObject SR_imageGirlPrefab;
        private GameObject SR_Girl_Clone;

        public GameObject SR_imageBoyPrefab;
        private GameObject SR_Boy_Clone;

        public GameObject SSR_imageGirlPrefab;
        private GameObject SSR_Girl_Clone;

        public GameObject SSR_imageBoyPrefab;
        private GameObject SSR_Boy_Clone;

        public GameObject btnClose;

        private bool keysActive = false;
        private bool pressedDestroy = false;
        
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (StaticDataStorage.R_Girl == true || StaticDataStorage.R_Boy == true || StaticDataStorage.SR_Girl == true || StaticDataStorage.SR_Boy == true || StaticDataStorage.SSR_Girl == true || StaticDataStorage.SSR_Boy == true)
            {
                //have bg when card are rendered (hide the wheel)
                OnPanel(gameObjectPanelBg);
                btnClose.GetComponent<Button>().interactable = false;
                cardDrawnTxt.text = "";
            }
            if (StaticDataStorage.CardBack == true)
            {
                createCardBackPrefab();
                StaticDataStorage.CardBack = false;
                cardDrawnTxt.text = "";
            }
        
            if (StaticDataStorage.isFlipped == true)
            {
                DestroyCardBackPrefab();
                StaticDataStorage.isFlipped = false;
                pressedDestroy = false;

                if (StaticDataStorage.R_Girl == true && StaticDataStorage.CardBack == false)
                {
                    //render the card
                    createR_GirlPrefab();
                    StaticDataStorage.R_Girl = false;
                    //r girl drawn count
                    ++StaticDataStorage.R_GirlDrawCount;
                    keysActive = true;
                    btnClose.GetComponent<Button>().interactable = true;
                    OnPanel(Confetti); 

                    Debug.Log("StaticDataStorage.R_GirlDrawCount:" + StaticDataStorage.R_GirlDrawCount.ToString());
                    cardDrawnTxt.text = "Congrats! R GIRL Drawn !";

                }
                if (StaticDataStorage.R_Boy == true && StaticDataStorage.CardBack == false)
                {
                    //render the card
                    createR_BoyPrefab();
                    StaticDataStorage.R_Boy = false;
                    //r girl drawn count
                    ++StaticDataStorage.R_BoyDrawCount;
                    keysActive = true;
                    btnClose.GetComponent<Button>().interactable = true;
                    OnPanel(Confetti);

                    Debug.Log("StaticDataStorage.R_BoyDrawCount:" + StaticDataStorage.R_BoyDrawCount.ToString());
                    cardDrawnTxt.text = "Congrats! R BOY Drawn !";


                }
                if (StaticDataStorage.SR_Girl == true && StaticDataStorage.CardBack == false)
                {
                    //render the card
                    createSR_GirlPrefab();
                    StaticDataStorage.SR_Girl = false;
                    //r girl drawn count
                    ++StaticDataStorage.SR_GirlDrawCount;
                    keysActive = true;
                    btnClose.GetComponent<Button>().interactable = true;
                    OnPanel(Confetti);

                    Debug.Log("StaticDataStorage.SR_GirlDrawCount:" + StaticDataStorage.SR_GirlDrawCount.ToString());
                    cardDrawnTxt.text = "Congrats! SR GIRL Drawn !";
                }
                if (StaticDataStorage.SR_Boy == true && StaticDataStorage.CardBack == false)
                {
                    //render the card
                    createSR_BoyPrefab();
                    StaticDataStorage.SR_Boy = false;
                    //r girl drawn count
                    ++StaticDataStorage.SR_BoyDrawCount;
                    keysActive = true;
                    btnClose.GetComponent<Button>().interactable = true;
                    OnPanel(Confetti);

                    //StartCoroutine(database.UpdateCharacters//(CharacterFactory.eCHARACTER.SR_CHARACTER_BOY0.ToString(), StaticDataStorage.SR_BoyDrawCount));

                    Debug.Log("StaticDataStorage.SR_BoyDrawCount:" + StaticDataStorage.SR_BoyDrawCount.ToString());
                    cardDrawnTxt.text = "Congrats! SR BOY Drawn !";
                }
                if (StaticDataStorage.SSR_Girl == true && StaticDataStorage.CardBack == false)
                {
                    //render the card
                    createSSR_GirlPrefab();
                    StaticDataStorage.SSR_Girl = false;
                    //r girl drawn count
                    ++StaticDataStorage.SSR_GirlDrawCount;
                    keysActive = true;
                    btnClose.GetComponent<Button>().interactable = true;
                    OnPanel(Confetti);

                    Debug.Log("StaticDataStorage.SSR_GirlDrawCount:" + StaticDataStorage.SSR_GirlDrawCount.ToString());
                    cardDrawnTxt.text = "Congrats! SSR GIRL Drawn !";
                }
                if (StaticDataStorage.SSR_Boy == true)
                {
                    //render the card
                    createSSR_BoyPrefab();
                    StaticDataStorage.SSR_Boy = false;
                    //r girl drawn count
                    ++StaticDataStorage.SSR_BoyDrawCount;
                    keysActive = true;
                    btnClose.GetComponent<Button>().interactable = true;
                    OnPanel(Confetti);

                    Debug.Log("StaticDataStorage.SSR_BoyDrawCount:" + StaticDataStorage.SSR_BoyDrawCount.ToString());
                    cardDrawnTxt.text = "Congrats! SSR BOY Drawn !";
                }

                Debug.Log("cardBackIsActive.cardBackIsActive:");
            }

            if (keysActive == true && Input.GetKeyDown(KeyCode.Return) || keysActive == true && pressedDestroy == true) 
            {
                //remove bg (unhide the wheel)
                OffPanel(gameObjectPanelBg);
                //Destroy everything in hierachy that i have created
                DestroyPrefab();

                keysActive = false;
                btnClose.GetComponent<Button>().interactable = false;
                OffPanel(Confetti);
                cardDrawnTxt.text = "";
            }
        }

        private void createCardBackPrefab()
        {
            CardBack_Clone = Instantiate(CardBackPrefab, new Vector3(452, 207, 0), Quaternion.identity) as GameObject;

            //Debug.Log("Spawned CardBackInstantiate");
        }

        private void createR_GirlPrefab()
        {
            R_Girl_Clone = Instantiate(R_imageGirlPrefab, new Vector3(452, 207, 0), Quaternion.identity) as GameObject;

            //Debug.Log("Spawned R GirlInstantiate");
        }

        private void createR_BoyPrefab()
        {
            R_Boy_Clone = Instantiate(R_imageBoyPrefab, new Vector3(452, 207, 0), Quaternion.identity) as GameObject;

            //Debug.Log("Spawned R_Boy_Instantiate");
        }

        private void createSR_GirlPrefab()
        {
            SR_Girl_Clone = Instantiate(SR_imageGirlPrefab, new Vector3(452, 207, 0), Quaternion.identity) as GameObject;

            //Debug.Log("Spawned SR GirlInstantiate");
        }

        private void createSR_BoyPrefab()
        {
            SR_Boy_Clone = Instantiate(SR_imageBoyPrefab, new Vector3(452, 207, 0), Quaternion.identity) as GameObject;

            //Debug.Log("Spawned SR_Boy_Instantiate");
        }

        private void createSSR_GirlPrefab()
        {
            SSR_Girl_Clone = Instantiate(SSR_imageGirlPrefab, new Vector3(452, 207, 0), Quaternion.identity) as GameObject;

            //Debug.Log("Spawned SSR GirlInstantiate");
        }

        private void createSSR_BoyPrefab()
        {
            SSR_Boy_Clone = Instantiate(SSR_imageBoyPrefab, new Vector3(452, 207, 0), Quaternion.identity) as GameObject;

            //Debug.Log("Spawned SSR_Boy_Instantiate");
        }

        private void DestroyPrefab()
        {
            Destroy(R_Girl_Clone);
            Destroy(R_Boy_Clone);
            Destroy(SR_Girl_Clone);
            Destroy(SR_Boy_Clone);
            Destroy(SSR_Girl_Clone);
            Destroy(SSR_Boy_Clone);
            Destroy(CardBack_Clone);
        }

        private void DestroyCardBackPrefab()
        {
            Destroy(CardBack_Clone);
        }
        private void OnPanel(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        private void OffPanel(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        public void PressedDestroyTrue()
        {
            pressedDestroy = true;
        }
    }
}
