using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IdolFever.Internet
{

    // check for internet connection
    internal sealed class CheckInternectConnection : MonoBehaviour
    {

        // reference https://stackoverflow.com/questions/24351155/unity-check-internet-connection-availability

        #region Fields
        public GameObject InternetPanelPopUp;

        private CheckInternectConnection INSTANCE;

        private const bool allowCarrierDataNetwork = false;
        private const string pingAddress = "8.8.8.8"; // Google Public DNS server
        private const float waitingTime = 5.0f;

        private Ping ping;
        private float pingStartTime;
        #endregion

        private void Awake()
        {
            // make sure only one is created
            if (INSTANCE == null)
            {
                DontDestroyOnLoad(this);
                INSTANCE = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Start()
        {
            bool internetPossiblyAvailable;
            switch (Application.internetReachability)
            {
                case NetworkReachability.ReachableViaLocalAreaNetwork:
                    internetPossiblyAvailable = true;
                    break;
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                    internetPossiblyAvailable = allowCarrierDataNetwork;
                    break;
                default:
                    internetPossiblyAvailable = false;
                    break;
            }
            if (!internetPossiblyAvailable)
            {
                InternetIsNotAvailable();
                return;
            }
            ping = new Ping(pingAddress);
            pingStartTime = Time.time;

            // subscribe to event
            SceneManager.sceneLoaded += OnSceneLoaded;

        }

        private void OnDestroy()
        {
            // unsubscribe from event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void Update()
        {
            // keep checking
            if (ping != null)
            {
                bool stopCheck = true;
                if (ping.isDone)
                {
                    if (ping.time >= 0)
                        InternetAvailable();
                    else
                        InternetIsNotAvailable();
                }
                else if (Time.time - pingStartTime < waitingTime)
                    stopCheck = false;
                else
                {
                    InternetIsNotAvailable();

                    // make a new ping to look for more internet
                    ping = new Ping(pingAddress);
                    pingStartTime = Time.time;
                }
                if (stopCheck)
                    ping = null; // reset check
            }
        }

        private void InternetIsNotAvailable()
        {
            Debug.Log("No Internet :(");
            InternetPanelPopUp.SetActive(true);
            // if the internet is dead then it's dead, time to quit the game
        }

        private void InternetAvailable()
        {
            Debug.Log("Internet is available! ;)");
            InternetPanelPopUp.SetActive(false);
        }

        public void QuitButton()
        {
            Application.Quit();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // check for internet ping again on a different scene
            ping = new Ping(pingAddress);
            pingStartTime = Time.time;
        }

    }

}
