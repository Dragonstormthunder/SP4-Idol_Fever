using UnityEngine;

namespace InterfaceA2 {
    internal sealed class TitleTextAnim: MonoBehaviour {
        #region Fields

        private float elapsedTime;
        private float offsetX;
        private float offsetY;
        private RectTransform rectTransformComponent;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        public TitleTextAnim() {
            elapsedTime = 0.0f;
            offsetX = 0.0f;
            offsetY = 0.0f;
            rectTransformComponent = null;
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            rectTransformComponent = (RectTransform)gameObject.transform;
        }

        private void Update() {
            elapsedTime += Time.deltaTime;
        }

        private void FixedUpdate() {
            offsetX = Mathf.Sin(4.0f * elapsedTime) * 0.4f;
            offsetY = Mathf.Cos(4.0f * elapsedTime) * 0.4f;

            rectTransformComponent.localPosition = new Vector3(
                rectTransformComponent.localPosition.x + offsetX,
                rectTransformComponent.localPosition.y + offsetY,
                0.0f
            );
        }

        #endregion
    }
}