using UnityEngine;

namespace IdolFever {
    internal sealed class TitleTextMtlControl: MonoBehaviour {
        #region Fields

        private float elapsedTime;
        private float hue;
        private Vector4 OG;
        [SerializeField] private float factor;
        [SerializeField] private float initialVal;
        [SerializeField] private Material titleTextMtl;

        #endregion

        #region Properties
        #endregion

        public TitleTextMtlControl() {
            elapsedTime = 0.0f;
            hue = 0.0f;
            //OG = new Vector4(); //hmmm
            factor = 0.0f;
            initialVal = 0.0f;
            titleTextMtl = null;
        }

        #region Unity User Callback Event Funcs

        private void Awake() {
            OG = titleTextMtl.GetVector("_IntensityVec");
        }

        private void Update() {
            elapsedTime += Time.deltaTime;

            hue = Mathf.Sin(elapsedTime) * 0.5f + 0.5f;
            Color myColorMultiplier = Color.HSVToRGB(hue, 1.0f, 1.0f);

            titleTextMtl.SetVector("_IntensityVec", new Vector4(
                initialVal + myColorMultiplier.r * factor,
                initialVal + myColorMultiplier.g * factor,
                initialVal + myColorMultiplier.b * factor,
                0.0f
            ));
        }

        private void OnDisable() {
            titleTextMtl.SetVector("_IntensityVec", OG);
        }

        #endregion
    }
}