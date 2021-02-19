using UnityEngine;
using UnityEngine.UI;

namespace IdolFever {
    internal sealed class MusicVolSlider: MonoBehaviour {
        #region Fields

        [SerializeField] private Slider musicVolSlider;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

	    private void Awake() {
            musicVolSlider.value = Options.MusicVol;

            musicVolSlider.onValueChanged.AddListener(delegate {
                OnSliderValChange();
            });
        }

        #endregion

        private void OnSliderValChange() {
            Options.SetMusicVol(musicVolSlider.value);
        }

        public MusicVolSlider() {
            musicVolSlider = null;
        }
    }
}