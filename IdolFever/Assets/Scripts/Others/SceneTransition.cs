using UnityEngine;
using UnityEngine.SceneManagement;

namespace IdolFever {
    internal sealed class SceneTransition: MonoBehaviour {
        #region Fields

        [SerializeField] private Animator animator;
        [SerializeField] private string sceneName;
        [SerializeField] private float transitionTime;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

	    private void Update() {
            if(Input.GetKeyDown(KeyCode.Space)) {
                _ = StartCoroutine(SceneTransitionCoroutine(sceneName));
            }
	    }

        private System.Collections.IEnumerator SceneTransitionCoroutine(string sceneName) {
            animator.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(sceneName);
        }

        #endregion

        public SceneTransition() {
            animator = null;
            sceneName = string.Empty;
            transitionTime = 0.0f;
        }
    }
}