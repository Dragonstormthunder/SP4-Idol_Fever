using UnityEngine;

namespace IdolFever {
    internal sealed class TitleTextMtlControl: MonoBehaviour {
        #region Fields

        private float hue;
        private Vector4 OG;
        [SerializeField] private float factor;
        [SerializeField] private float spd;
        [SerializeField] private Material titleTextMtl;

        #endregion

        #region Properties
        #endregion

        public TitleTextMtlControl() {
            hue = 0.0f;
            //OG = new Vector4(); //hmmm
            factor = 0.0f;
            spd = 0.0f;
            titleTextMtl = null;
        }

        #region Unity User Callback Event Funcs

        private void Awake() {
            OG = titleTextMtl.GetVector("_IntensityVec");
        }

        private void Update() {
            hue += Time.deltaTime * spd;
            if(hue >= 1.0f) {
                hue = 0.0f;
            }
            Color myColorMultiplier = Color.HSVToRGB(hue, 1.0f, 1.0f);

            if(Options.GraphicsOption == GraphicsQualityOptions.GraphicsQualityOption.High) {
                titleTextMtl.SetVector("_IntensityVec", new Vector4(
                    OG.x + myColorMultiplier.r * factor,
                    OG.y + myColorMultiplier.g * factor,
                    OG.z + myColorMultiplier.b * factor,
                    0.0f
                ));
            } else {
                titleTextMtl.SetVector("_IntensityVec", new Vector4(
                    OG.x,
                    OG.y,
                    OG.z,
                    0.0f
                ));
            }
        }

        private void OnDisable() {
            titleTextMtl.SetVector("_IntensityVec", OG);
        }

        #endregion
    }
}