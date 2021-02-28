using UnityEngine;

namespace IdolFever {
    internal sealed class Trail: MonoBehaviour {
        #region Fields

        [SerializeField] private float distFromCam;
        [SerializeField] private Camera camComponent;

        #endregion

        #region Properties
        #endregion

        public Trail() {
            camComponent = null;
        }

        #region Unity User Callback Event Funcs

        private void Update() {
            transform.position = camComponent.ScreenPointToRay(Input.mousePosition).GetPoint(10);
        }

        #endregion
    }
}