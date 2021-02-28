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
            MoveTrailToCursor(Input.mousePosition);
        }

        #endregion

        private void MoveTrailToCursor(Vector3 screenPosition) {
            transform.position = camComponent.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distFromCam));
        }
    }
}