using System.Linq;
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
            soundVolSlider.value = Options.SoundVol;

            soundVolSlider.onValueChanged.AddListener(delegate {
                OnSliderValChange();
            });
        }

        #endregion

        private void OnSliderValChange() {
            Options.SetSoundVol(soundVolSlider.value);

            var objs = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Sound");
            foreach(GameObject GO in objs) {
                if(GO.activeSelf) {
                    foreach(Transform child in GO.transform) {
                        child.GetComponent<AudioSource>().volume = Options.SoundVol;
                    }
                }
            }
        }

        public SoundVolSlider() {
            soundVolSlider = null;
        }
    }
}