using UnityEngine;

namespace IdolFever {
    internal sealed class ButtonSquishyAnimControl: MonoBehaviour {
        #region Fields

        [SerializeField] private Animation buttonSquishyAnim;

        #endregion

        #region Properties
        #endregion

        public ButtonSquishyAnimControl() {
            buttonSquishyAnim = null;
        }

        #region Unity User Callback Event Funcs
        #endregion

        public void OnButtonClick() {
            buttonSquishyAnim.Play();
        }
    }
}