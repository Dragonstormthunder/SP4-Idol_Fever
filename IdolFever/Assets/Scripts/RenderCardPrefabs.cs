using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IdolFever.UI
{
    public class RenderCardPrefabs : MonoBehaviour
    {
        public GameObject gameObjectPanelBg;

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
            }
            if (StaticDataStorage.R_Girl == true)
            {
                //render the card
                createR_GirlPrefab();
                StaticDataStorage.R_Girl = false;
                //r girl drawn count
                ++StaticDataStorage.R_GirlDrawCount;
                Debug.Log("StaticDataStorage.R_GirlDrawCount:" + StaticDataStorage.R_GirlDrawCount.ToString());
            }
            if (StaticDataStorage.R_Boy == true)
            {
                //render the card
                createR_BoyPrefab();
                StaticDataStorage.R_Boy = false;
                //r girl drawn count
                ++StaticDataStorage.R_BoyDrawCount;
                Debug.Log("StaticDataStorage.R_BoyDrawCount:" + StaticDataStorage.R_BoyDrawCount.ToString());
            }
            if (StaticDataStorage.SR_Girl == true)
            {
                //render the card
                createSR_GirlPrefab();
                StaticDataStorage.SR_Girl = false;
                //r girl drawn count
                ++StaticDataStorage.SR_GirlDrawCount;
                Debug.Log("StaticDataStorage.SR_GirlDrawCount:" + StaticDataStorage.SR_GirlDrawCount.ToString());
            }
            if (StaticDataStorage.SR_Boy == true)
            {
                //render the card
                createSR_BoyPrefab();
                StaticDataStorage.SR_Boy = false;
                //r girl drawn count
                ++StaticDataStorage.SR_BoyDrawCount;
                Debug.Log("StaticDataStorage.SR_BoyDrawCount:" + StaticDataStorage.SR_BoyDrawCount.ToString());
            }
            if (StaticDataStorage.SSR_Girl == true)
            {
                //render the card
                createSSR_GirlPrefab();
                StaticDataStorage.SSR_Girl = false;
                //r girl drawn count
                ++StaticDataStorage.SSR_GirlDrawCount;
                Debug.Log("StaticDataStorage.SSR_GirlDrawCount:" + StaticDataStorage.SSR_GirlDrawCount.ToString());
            }
            if (StaticDataStorage.SSR_Boy == true)
            {
                //render the card
                createSSR_BoyPrefab();
                StaticDataStorage.SSR_Boy = false;
                //r girl drawn count
                ++StaticDataStorage.SSR_BoyDrawCount;
                Debug.Log("StaticDataStorage.SSR_BoyDrawCount:" + StaticDataStorage.SSR_BoyDrawCount.ToString());
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                //remove bg (unhide the wheel)
                OffPanel(gameObjectPanelBg);
                //Destroy everything in hierachy that i have created
                DestroyR_GirlPrefab();
            }
        }

        private void createR_GirlPrefab()
        {
            R_Girl_Clone = Instantiate(R_imageGirlPrefab, new Vector3(450, 209, 0), Quaternion.identity) as GameObject;

            Debug.Log("Spawned R GirlInstantiate");
        }

        private void createR_BoyPrefab()
        {
            R_Boy_Clone = Instantiate(R_imageBoyPrefab, new Vector3(450, 209, 0), Quaternion.identity) as GameObject;

            Debug.Log("Spawned R_Boy_Instantiate");
        }

        private void createSR_GirlPrefab()
        {
            SR_Girl_Clone = Instantiate(SR_imageGirlPrefab, new Vector3(450, 209, 0), Quaternion.identity) as GameObject;

            Debug.Log("Spawned SR GirlInstantiate");
        }

        private void createSR_BoyPrefab()
        {
            SR_Boy_Clone = Instantiate(SR_imageBoyPrefab, new Vector3(450, 209, 0), Quaternion.identity) as GameObject;

            Debug.Log("Spawned SR_Boy_Instantiate");
        }

        private void createSSR_GirlPrefab()
        {
            SSR_Girl_Clone = Instantiate(SSR_imageGirlPrefab, new Vector3(450, 209, 0), Quaternion.identity) as GameObject;

            Debug.Log("Spawned SSR GirlInstantiate");
        }

        private void createSSR_BoyPrefab()
        {
            SSR_Boy_Clone = Instantiate(SSR_imageBoyPrefab, new Vector3(450, 209, 0), Quaternion.identity) as GameObject;

            Debug.Log("Spawned SSR_Boy_Instantiate");
        }

        private void DestroyR_GirlPrefab()
        {
            Destroy(R_Girl_Clone);
            Destroy(R_Boy_Clone); 
            Destroy(SR_Girl_Clone);
            Destroy(SR_Boy_Clone);
            Destroy(SSR_Girl_Clone);
            Destroy(SSR_Boy_Clone);
        }

        private void OnPanel(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        private void OffPanel(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
