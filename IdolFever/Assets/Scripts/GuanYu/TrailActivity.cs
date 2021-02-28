using UnityEngine;

namespace IdolFever {
    internal sealed class TrailActivity: MonoBehaviour {
        #region Fields

        [SerializeField] private GameObject myTrail;

        #endregion

        #region Properties
        #endregion

        internal TrailActivity() {
            myTrail = null;
        }

        #region Unity User Callback Event Funcs

        private void Update() {
            if(UnityEngine.Rendering.SplashScreen.isFinished) {
                myTrail.SetActive(true);
                enabled = false;
            }
        }

        #endregion
    }
}