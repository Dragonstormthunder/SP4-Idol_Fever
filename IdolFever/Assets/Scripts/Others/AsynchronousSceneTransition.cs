using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IdolFever {
    internal sealed class AsynchronousSceneTransition: MonoBehaviour {
        #region Fields

        public float progressVal;
        private static bool is1stScreen = true;
        [SerializeField] private Animator animator;
        [SerializeField] private string sceneName;
        [SerializeField] private string startAnimName;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            if(!is1stScreen) {
                animator.SetTrigger("End");
            } else {
                is1stScreen = false;
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
                progressVal = Mathf.Clamp01(operation.progress / 0.9f) * 100.0f;

                yield return null;
            }
        }

        #endregion

        public AsynchronousSceneTransition() {
            progressVal = 0.0f;
            animator = null;
            sceneName = string.Empty;
            startAnimName = string.Empty;
        }
    }
}