using UnityEngine;
using UnityEngine.SceneManagement;

namespace IdolFever {
    internal sealed class SynchronousSceneTransition: MonoBehaviour {
        #region Fields

        [SerializeField] private Animator animator;
        [SerializeField] private string sceneName;
        [SerializeField] private float transitionTime;

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
                _ = StartCoroutine(SynchronousSceneTransitionCoroutine(sceneName));
            }
	    }

        private System.Collections.IEnumerator SynchronousSceneTransitionCoroutine(string sceneName) {
            animator.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(sceneName);
        }

        #endregion

        public SynchronousSceneTransition() {
            animator = null;
            sceneName = string.Empty;
            transitionTime = 0.0f;
        }
    }
}