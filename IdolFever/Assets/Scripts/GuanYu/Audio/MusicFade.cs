using UnityEngine;
using UnityEngine.SceneManagement;

namespace IdolFever {
    internal sealed class MusicFade: MonoBehaviour {
        #region Fields

        private float lerpFactor;
        [SerializeField] private bool isFadeIn;
        [SerializeField] private float maxFadeTime;
        [SerializeField] private MusicCentralControl musicCentralControl;
        [SerializeField] private string[] sceneNames;

        #endregion

        #region Properties
        #endregion

        public MusicFade() {
            lerpFactor = 0.0f;
            isFadeIn = true;
            maxFadeTime = 0.0f;
            musicCentralControl = null;
            sceneNames = System.Array.Empty<string>();
        }

        #region Unity User Callback Event Funcs

        private void Update() {
            foreach(string sceneName in sceneNames) {
                if(sceneName == SceneManager.GetActiveScene().name) {
                    lerpFactor += Time.deltaTime / maxFadeTime;

                    if(lerpFactor > 1.0f) {
                        lerpFactor = 1.0f;
                    }

                    foreach(AudioSource audioSrc in musicCentralControl.AudioSrcs) {
                        if(isFadeIn) {
                            audioSrc.volume = Mathf.Lerp(0.0f, Options.MusicVol, lerpFactor);
                        } else {
                            audioSrc.volume = Mathf.Lerp(Options.MusicVol, 0.0f, lerpFactor);
                        }
                    }

                    return;
                }
            }

            lerpFactor = 0.0f;
        }

        #endregion
    }
}