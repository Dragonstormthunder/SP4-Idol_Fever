using TMPro;
using UnityEngine;

namespace IdolFever {
    internal sealed class DcTextAnim: MonoBehaviour {
        #region Fields

        private float elapsedTime;
        [SerializeField] private TextMeshProUGUI tmpComponent;
        [HideInInspector] public float spd;
        [HideInInspector] public float startY;
        [HideInInspector] public float endY;
        [HideInInspector] public float startSize;
        [HideInInspector] public float endSize;
        [HideInInspector] public float startAlpha;
        [HideInInspector] public float endAlpha;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Update() {
            elapsedTime += Time.deltaTime;

            float lerpFactor = EaseInOutCirc(Mathf.Cos(elapsedTime * spd) * 0.5f + 0.5f);

            tmpComponent.alpha = (1.0f - lerpFactor) * startAlpha + lerpFactor * endAlpha;
            if(Mathf.Approximately(tmpComponent.alpha, 0.0f)) {
                Destroy(gameObject);
                return;
            }

            RectTransform rectTransform = (RectTransform)transform;
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, (1.0f - lerpFactor) * startY + lerpFactor * endY);

            tmpComponent.fontSize = (1.0f - lerpFactor) * startSize + lerpFactor * endSize;
        }

        #endregion

        public DcTextAnim() {
            elapsedTime = 0.0f;
            tmpComponent = null;
            spd = 0.0f;
            startY = 0.0f;
            endY = 0.0f;
            startSize = 0.0f;
            endSize = 0.0f;
            startAlpha = 0.0f;
            endAlpha = 0.0f;
        }

        private float EaseInOutCirc(float x) {
            return x < 0.5f
                ? (1.0f - Mathf.Sqrt(1.0f - Mathf.Pow(2.0f * x, 2.0f))) * 0.5f
                : (Mathf.Sqrt(1.0f - Mathf.Pow(-2.0f * x + 2.0f, 2.0f)) + 1.0f) * 0.5f;
        }
    }
}