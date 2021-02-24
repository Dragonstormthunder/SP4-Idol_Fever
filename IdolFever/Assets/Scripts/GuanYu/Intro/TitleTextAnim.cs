using UnityEngine;

namespace IdolFever {
    internal sealed class TitleTextAnim: MonoBehaviour {
        #region Fields

        private float elapsedTime;
        private float offsetX;
        private float offsetY;
        private RectTransform rectTransformComponent;
        [SerializeField] private float spdSin;
        [SerializeField] private float magnitudeSin;
        [SerializeField] private float spdCos;
        [SerializeField] private float magnitudeCos;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        public TitleTextAnim() {
            elapsedTime = 0.0f;
            offsetX = 0.0f;
            offsetY = 0.0f;
            rectTransformComponent = null;
            spdSin = 0.0f;
            magnitudeSin = 0.0f;
            spdCos = 0.0f;
            magnitudeCos = 0.0f;
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
            offsetX = Mathf.Sin(spdSin * elapsedTime) * magnitudeSin;
            offsetY = Mathf.Cos(spdCos * elapsedTime) * magnitudeCos;

            rectTransformComponent.localPosition = new Vector3(
                rectTransformComponent.localPosition.x + offsetX,
                rectTransformComponent.localPosition.y + offsetY,
                0.0f
            );
        }

        #endregion
    }
}