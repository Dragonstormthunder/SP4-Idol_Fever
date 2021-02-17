using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IdolFever {
    internal sealed class AsyncSceneTransitionOut: MonoBehaviour {
        #region Fields

        [SerializeField] private Animator animator;
        [SerializeField] private Image img;
        [SerializeField] private string sceneName;
        [SerializeField] private string startAnimName;

        #endregion

        #region Properties

        public string SceneName {
            get {
                return sceneName;
            }
            private set { //Not used
                sceneName = value;
            }
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void ChangeScene() {
            _ = StartCoroutine(MyStartCoroutine(sceneName));
        }

        private System.Collections.IEnumerator MyStartCoroutine(string sceneName) {
            img.fillAmount = 0.0f;

            animator.SetTrigger("Start");

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

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

            while(!operation.isDone) {
                img.fillAmount = Mathf.Clamp01(operation.progress / 0.9f);
                yield return null;
            }
        }

        public AsyncSceneTransitionOut() {
            animator = null;
            img = null;
            sceneName = string.Empty;
            startAnimName = string.Empty;
        }
    }
}