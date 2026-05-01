using UnityEngine;
using UnityEngine.SceneManagement;

namespace IdolFever {
    internal sealed class MusicFade: MonoBehaviour {
        #region Fields

        [SerializeField] private bool isFadeIn;
        [SerializeField] private float fadeTime; //From start to end
        [SerializeField] private MusicCentralControl musicCentralControl;
        [SerializeField] private string[] sceneNames;

        #endregion

        #region Properties
        #endregion

        public MusicFade() {
            isFadeIn = true;
            fadeTime = 0.0f;
            musicCentralControl = null;
            sceneNames = System.Array.Empty<string>();
        }

        #region Unity User Callback Event Funcs

        private void Update() {
            foreach(string sceneName in sceneNames) {
                if(sceneName == SceneManager.GetActiveScene().name) {
                    foreach(AudioSource audioSrc in musicCentralControl.AudioSrcs) {
                        if(isFadeIn) {
                            float lerpFactor = audioSrc.volume / Options.MusicVol;
                            lerpFactor += Time.deltaTime / fadeTime;

                            audioSrc.volume = Mathf.Lerp(0.0f, Options.MusicVol, lerpFactor);
                        } else {
                            float lerpFactor = (Options.MusicVol - audioSrc.volume) / Options.MusicVol;
                            lerpFactor += Time.deltaTime / fadeTime;

                            audioSrc.volume = Mathf.Lerp(Options.MusicVol, 0.0f, lerpFactor);
                        }
                    }
                }
            }
        }

        #endregion
    }
}