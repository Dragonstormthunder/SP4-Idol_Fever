using UnityEngine;
using UnityEngine.UI;

namespace IdolFever {
    internal sealed class TapTextAnim: MonoBehaviour {
        #region Fields

        private float elapsedTime;
        [SerializeField] private Image img;
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
            img = null;
            startSize = 0.0f;
            endSize = 0.0f;
            startAlpha = 0.0f;
            endAlpha = 0.0f;
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            UnityEngine.Assertions.Assert.IsNotNull(img);
        }

        private void Update() {
            elapsedTime += Time.deltaTime;
        }

        private void FixedUpdate() {
            float lerpFactor = EaseInOutQuad(Mathf.Cos(elapsedTime * 4.0f) * 0.5f + 0.5f);

            Color myColor = img.color;
            img.color = new Color(myColor.r, myColor.g, myColor.b, (1.0f - lerpFactor) * startAlpha + lerpFactor * endAlpha);

            float scaleFactor = (1.0f - lerpFactor) * startSize + lerpFactor * endSize;
            img.gameObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }

        #endregion

        private float EaseInOutQuad(float x) {
                return x < 0.5f ? 2.0f * x * x : 1.0f - Mathf.Pow(-2.0f * x + 2.0f, 2.0f) * 0.5f;
            }
        }
}