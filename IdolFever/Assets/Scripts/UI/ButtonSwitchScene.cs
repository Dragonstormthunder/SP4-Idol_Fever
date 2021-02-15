using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IdolFever.Storage;

namespace IdolFever.UI
{
    public class ButtonSwitchScene : MonoBehaviour
    {

        // the scene to change to, the default one
        public string defaultChangeSceneName;

        public bool changeSceneDueOnMode = false;
        public string onlineChangeSceneName;
        public string storyChangeSceneName;

        // default function
        public void ClickChangeScene()
        {

            StaticDataStorage.LastSceneName = SceneManager.GetActiveScene().name;
            Debug.Log("Last Scene: " + StaticDataStorage.LastSceneName);

            if (changeSceneDueOnMode)
            {
                switch (StaticDataStorage.GameMode)
                {
                    default:
                        break;

                    case StaticDataStorage.GAME_MODE.MODE_STORY:
                        SceneManager.LoadScene(storyChangeSceneName);
                        break;

                    case StaticDataStorage.GAME_MODE.MODE_ONLINE:
                        SceneManager.LoadScene(onlineChangeSceneName);
                        break;
                }
            }
            else
            {
                SceneManager.LoadScene(defaultChangeSceneName);
            }

        }

    }
}