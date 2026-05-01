using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever
{
    // detect platform runtime
    public class DetectPlatformRuntime : MonoBehaviour
    {

        // for debugging, only on window editor
        public enum ENABLE_WINDOW_EDITOR
        {
            NO_DEBUG,
            DEBUG_WINDOWS,
            DEBUG_ANDROID
        }

        public GameObject windowsCanvas;
        public GameObject androidCanvas;

        public ENABLE_WINDOW_EDITOR debug;

        public void Start()
        {
            switch (Application.platform)
            {
                default:
                    throw new Exception("Invalid Platform");
                case RuntimePlatform.Android:
                    windowsCanvas.SetActive(false);
                    androidCanvas.SetActive(true);
                    break;
                case RuntimePlatform.WindowsPlayer:
                    windowsCanvas.SetActive(true);
                    androidCanvas.SetActive(false);
                    break;
                // for debugging -
                case RuntimePlatform.WindowsEditor:
                    switch (debug)
                    {
                        default:
                            break;  // proceed to the next part
                        case ENABLE_WINDOW_EDITOR.NO_DEBUG:
                            break;  // proceed to the next part
                        case ENABLE_WINDOW_EDITOR.DEBUG_ANDROID:

                            windowsCanvas.SetActive(false);
                            androidCanvas.SetActive(true);

                            return; // kill here
                        case ENABLE_WINDOW_EDITOR.DEBUG_WINDOWS:

                            windowsCanvas.SetActive(true);
                            androidCanvas.SetActive(false);

                            return; // kill here
                    }
                    break;
                    // end debug -
            }
        }

        // for debugging
        // remember to comment out for the actual submission
        public void Update()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                switch (debug)
                {
                    default:
                        break;  // proceed to the next part
                    case ENABLE_WINDOW_EDITOR.NO_DEBUG:
                        break;  // proceed to the next part
                    case ENABLE_WINDOW_EDITOR.DEBUG_ANDROID:

                        windowsCanvas.SetActive(false);
                        androidCanvas.SetActive(true);

                        break;
                    case ENABLE_WINDOW_EDITOR.DEBUG_WINDOWS:

                        windowsCanvas.SetActive(true);
                        androidCanvas.SetActive(false);

                        break;
                }
            }
        }
    }
}