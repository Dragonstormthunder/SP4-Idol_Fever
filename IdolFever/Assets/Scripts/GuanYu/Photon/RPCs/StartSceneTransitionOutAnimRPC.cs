using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IdolFever {
    internal sealed class StartSceneTransitionOutAnimRPC: MonoBehaviour {
        #region Fields

        [SerializeField] private Animator animator;
        [SerializeField] private string startAnimName;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public StartSceneTransitionOutAnimRPC() {
            animator = null;
            startAnimName = string.Empty;
        }

        [PunRPC] public void StartSceneTransitionOutAnim(string GOName) {
            PanelsControl.IsStartSceneTransitionOutAnimReceived = true;

            GameObject sceneTransitionGO = GameObject.Find(GOName);
            Animator animator = sceneTransitionGO.GetComponent<Animator>();

            GameObject progressBarGO = animator.transform.Find("ProgressBar").gameObject;
            GameObject progressGO = progressBarGO.transform.Find("Progress").gameObject;
            Image img = progressGO.GetComponent<Image>();

            img.fillAmount = 0.0f;

            animator.SetTrigger("Start");

            SceneTracker.prevSceneName = SceneManager.GetActiveScene().name;

            _ = StartCoroutine(nameof(AnimCoroutine));
        }

        private System.Collections.IEnumerator AnimCoroutine() {
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
        }
    }
}