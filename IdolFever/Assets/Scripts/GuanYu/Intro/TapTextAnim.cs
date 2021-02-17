using TMPro;
using UnityEngine;

namespace InterfaceA2 {
    internal sealed class TapTextAnim: MonoBehaviour {
        #region Fields

        private float elapsedTime;
        [SerializeField] private TextMeshProUGUI tmpComponent;
        [SerializeField] private float startSize;
        [SerializeField] private float endSize;
        [SerializeField] private float startAlpha;
        [SerializeField] private float endAlpha;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        public TapTextAnim() {
            elapsedTime = 0.0f;
            tmpComponent = null;
            startSize = 0.0f;
            endSize = 0.0f;
            startAlpha = 0.0f;
            endAlpha = 0.0f;
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            UnityEngine.Assertions.Assert.IsNotNull(tmpComponent);
        }

        private void Update() {
            elapsedTime += Time.deltaTime;
        }

        private void FixedUpdate() {
            float lerpFactor = EaseInSine(Mathf.Cos(elapsedTime * 4.0f) * 0.5f + 0.5f);
            tmpComponent.alpha = (1.0f - lerpFactor) * startAlpha + lerpFactor * endAlpha;
            tmpComponent.fontSize = (1.0f - lerpFactor) * startSize + lerpFactor * endSize;
        }

        #endregion

        private float EaseInSine(float x) {
            return 1.0f - Mathf.Cos((x * Mathf.PI) * 0.5f);
        }
    }
}