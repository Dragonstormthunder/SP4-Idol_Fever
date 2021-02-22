using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IdolFever {
    internal sealed class AsyncSceneTransitionOut: MonoBehaviour {
        #region Fields

        [SerializeField] private AsyncSceneTransitionIn asyncSceneTransitionIn;
        [SerializeField] private AsyncSceneTransitionOutTypes.AsyncSceneTransitionOutType type;
        [SerializeField] private Animator animator;
        [SerializeField] private AudioListener audioListener;
        [SerializeField] private GameObject myInterest;
        [SerializeField] private Image img;
        [SerializeField] private string sceneName;
        [SerializeField] private string startAnimName;

        #endregion

        #region Properties

        public static bool IsStartSceneTransitionOutAnimReceived {
            get;
            set;
        }

        public string SceneName {
            get {
                return sceneName;
            }
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void ChangeScene() {
            if(!PhotonNetwork.IsConnected) {
                img.fillAmount = 0.0f;

                animator.SetTrigger("Start");

                SceneTracker.prevSceneName = SceneManager.GetActiveScene().name;
            }
            _ = StartCoroutine(ChangeSceneCoroutine(sceneName));
        }

        private System.Collections.IEnumerator ChangeSceneCoroutine(string sceneName) {
            float animLen = -1.0f;
            foreach(AnimationClip clip in animator.runtimeAnimatorController.animationClips) {
                if(clip.name == startAnimName) {
                    animLen = clip.length;
                }
            }

            if(animLen >= 0.0f) {
                yield return new WaitForSeconds(animLen);
            } else {
                UnityEngine.Assertions.Assert.IsTrue(false);
            }

            switch(type) {
                case AsyncSceneTransitionOutTypes.AsyncSceneTransitionOutType.AddSingle: {
                    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

                    while(!operation.isDone) {
                        img.fillAmount = Mathf.Clamp01(operation.progress / 0.9f);
                        yield return null;
                    }

                    break;
                }
                case AsyncSceneTransitionOutTypes.AsyncSceneTransitionOutType.AddAdditive: {
                    SceneManager.sceneLoaded += OnSceneLoaded;
                    SceneManager.sceneUnloaded += OnSceneUnloaded;

                    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

                    while(!operation.isDone) {
                        img.fillAmount = Mathf.Clamp01(operation.progress / 0.9f);
                        yield return null;
                    }

                    break;
                }
                case AsyncSceneTransitionOutTypes.AsyncSceneTransitionOutType.RemoveAdditive: {
                    AsyncOperation operation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name, UnloadSceneOptions.None);

                    while(!operation.isDone) {
                        img.fillAmount = Mathf.Clamp01(operation.progress / 0.9f);
                        yield return null;
                    }

                    break;
                }
                default:
                   UnityEngine.Assertions.Assert.IsTrue(false);
                   break;
            }
        }

        public AsyncSceneTransitionOut() {
            asyncSceneTransitionIn = null;
            type = AsyncSceneTransitionOutTypes.AsyncSceneTransitionOutType.AddSingle;
            animator = null;
            audioListener = null;
            myInterest = null;
            img = null;
            sceneName = string.Empty;
            startAnimName = string.Empty;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            myInterest.SetActive(false);
            audioListener.enabled = false;

            SceneManager.SetActiveScene(scene);

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneUnloaded(Scene scene) {
            myInterest.SetActive(true);
            audioListener.enabled = true;

            asyncSceneTransitionIn.TransitionIn();

            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }
}