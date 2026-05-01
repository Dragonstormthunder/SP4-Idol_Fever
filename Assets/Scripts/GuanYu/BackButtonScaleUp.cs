using UnityEngine;

namespace IdolFever {
    internal sealed class BackButtonScaleUp: MonoBehaviour {
        #region Fields

        private bool isPtrUp;
        private float lerpFactor;
        [SerializeField] private float maxTime;
        [SerializeField] private float startScaleFactor;
        [SerializeField] private float endScaleFactor;
        [SerializeField] private Transform myTransform;

        #endregion

        #region Properties
        #endregion

        public BackButtonScaleUp() {
            isPtrUp = false;
            lerpFactor = 0.0f;
            maxTime = 0.0f;
            startScaleFactor = 0.0f;
            endScaleFactor = 0.0f;
            myTransform = null;
        }

        #region Unity User Callback Event Funcs

        private void Update() {
            if(isPtrUp) {
                lerpFactor += Time.deltaTime / maxTime;

                if(lerpFactor > 1.0f) {
                    lerpFactor = 1.0f;
                }

                float myScale = Mathf.Lerp(startScaleFactor, endScaleFactor, lerpFactor);
                myTransform.localScale = new Vector3(myScale, myScale, 1.0f);
            }
        }

        #endregion

        public void OnPointerDown() {
            isPtrUp = false;
        }

        public void OnPointerUp() {
            isPtrUp = true;
        }
    }
}