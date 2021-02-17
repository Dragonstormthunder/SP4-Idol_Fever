using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IdolFever.UI
{
    public class GachaDraw : MonoBehaviour
    {
        // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
        public GameObject R_imageGirlPrefab;
        private bool Spawned_RGirl;

        public GameObject R_imageBoyPrefab;
        private bool Spawned_RBoy;

        public GameObject SR_imageGirlPrefab;
        private bool Spawned_SRGirl;

        public GameObject SR_imageBoyPrefab;
        private bool Spawned_SRBoy;

        public GameObject SSR_imageGirlPrefab;
        private bool Spawned_SSRGirl;

        public GameObject SSR_imageBoyPrefab;
        private bool Spawned_SSRBoy;

        // This script will simply instantiate the Prefab when the game starts.
        void Start()
        {
            Spawned_RGirl = false;
            Spawned_RBoy = false;
            Spawned_SRGirl = false;
            Spawned_SRBoy = false;
            Spawned_SSRGirl = false;
            Spawned_SSRBoy = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (StaticDataStorage.R_Girl == true && Spawned_RGirl == false)
            {
                Spawned_RGirl = true;
                Instantiate(R_imageGirlPrefab, new Vector3(450, 209, 0), Quaternion.identity);
                Debug.Log("Spawned R Girl");
            }
            else if (StaticDataStorage.R_Boy == true && Spawned_RBoy == false)
            {
                Spawned_RBoy = true;
                Instantiate(R_imageBoyPrefab, new Vector3(450, 209, 0), Quaternion.identity);
                Debug.Log("Spawned R Boy");
            }
            else if (StaticDataStorage.SR_Girl == true && Spawned_SRGirl == false)
            {
                Spawned_SRGirl = true;
                Instantiate(SR_imageGirlPrefab, new Vector3(450, 209, 0), Quaternion.identity);
                Debug.Log("Spawned SR Girl");
            }
            else if (StaticDataStorage.SR_Boy == true && Spawned_SRBoy == false)
            {
                Spawned_SRBoy = true;
                Instantiate(SR_imageBoyPrefab, new Vector3(450, 209, 0), Quaternion.identity);
                Debug.Log("Spawned SR Boy");
            }
            else if (StaticDataStorage.SSR_Girl == true && Spawned_SSRGirl == false)
            {
                Spawned_SSRGirl = true;
                Instantiate(SR_imageGirlPrefab, new Vector3(450, 209, 0), Quaternion.identity);
                Debug.Log("Spawned SSR Girl");
            }
            else if (StaticDataStorage.SSR_Boy == true && Spawned_SSRBoy == false)
            {
                Spawned_SSRBoy = true;
                Instantiate(SSR_imageBoyPrefab, new Vector3(450, 209, 0), Quaternion.identity);
                Debug.Log("Spawned SSR Boy");
            }
        }

        public void Card_false()
        {
            if (StaticDataStorage.R_Girl == true && Spawned_RGirl == true)
            {
                Spawned_RGirl = false;
                StaticDataStorage.R_Girl = false;
                Debug.Log("DeSpawned R Girl");
            }
            if (StaticDataStorage.R_Boy == true && Spawned_RBoy == true)
            {
                Spawned_RBoy = false;
                StaticDataStorage.R_Boy = false;
                Debug.Log("DeSpawned R Boy");
            }
            if (StaticDataStorage.SR_Girl == true && Spawned_SRGirl == true)
            {
                Spawned_SRGirl = false;
                StaticDataStorage.SR_Girl = false;
                Debug.Log("DeSpawned SR Girl");
            }
            if (StaticDataStorage.SR_Boy == true && Spawned_SRBoy == true)
            {
                Spawned_SRBoy = false;
                StaticDataStorage.SR_Boy = false;
                Debug.Log("DeSpawned SR Boy");
            }
            if (StaticDataStorage.SSR_Girl == true && Spawned_SSRGirl == true)
            {
                Spawned_SSRGirl = false;
                StaticDataStorage.SSR_Girl = false;
                Debug.Log("DeSpawned SSR Girl");
            }
            if (StaticDataStorage.SSR_Boy == true && Spawned_SSRBoy == true)
            {
                Spawned_RBoy = false;
                StaticDataStorage.SSR_Boy = false;
                Debug.Log("DeSpawned SSR Boy");
            }
        }
    }
}