using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IdolFever {
    internal sealed class AsynchronousSceneTransition: MonoBehaviour {
        #region Fields

        [SerializeField] private Animator animator;
        [SerializeField] private string sceneName;
        [SerializeField] private string startAnimName;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            if(SceneManager.GetActiveScene().buildIndex != 0) {
                animator.SetTrigger("End");
            }
        }

	    private void Update() {
            if(Input.GetKeyDown(KeyCode.Space)) {
                _ = StartCoroutine(AsynchronousSceneTransitionCoroutine(sceneName));
            }
	    }

        private System.Collections.IEnumerator AsynchronousSceneTransitionCoroutine(string sceneName) {
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
                float percentageProgress = Mathf.Clamp01(operation.progress / 0.9f) * 100.0f; //??

                yield return null;
            }
        }

        #endregion

        public AsynchronousSceneTransition() {
            animator = null;
            sceneName = string.Empty;
            startAnimName = string.Empty;
        }
    }
}