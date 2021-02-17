using UnityEngine;
using UnityEngine.UI;

namespace IdolFever {
    internal sealed class AsyncSceneTransitionIn: MonoBehaviour {
        #region Fields

        private static bool is1stScreen = true;
        [SerializeField] private Animator animator;
        [SerializeField] private Image img;

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

            img.fillAmount = 100.0f;
        }

        #endregion

        public AsyncSceneTransitionIn() {
            animator = null;
            img = null;
        }
    }
}