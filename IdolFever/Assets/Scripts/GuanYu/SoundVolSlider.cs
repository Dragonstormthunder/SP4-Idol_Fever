using UnityEngine;
using UnityEngine.UI;

namespace IdolFever {
    internal sealed class SoundVolSlider: MonoBehaviour {
        #region Fields

        [SerializeField] private Slider soundVolSlider;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

	    private void Awake() {
            soundVolSlider.onValueChanged.AddListener(delegate {
                OnSliderValChange();
            });
        }

        #endregion

        private void OnSliderValChange() {
            Options.SetSoundVol(soundVolSlider.value);
        }

        public SoundVolSlider() {
            soundVolSlider = null;
        }
    }
}