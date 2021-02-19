using UnityEngine;

namespace IdolFever {
    internal sealed class PauseScreen: MonoBehaviour {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public void ActivateSelf() {
            gameObject.SetActive(true);
        }

        public void DeactivateSelf() {
            gameObject.SetActive(false);
        }
    }
}