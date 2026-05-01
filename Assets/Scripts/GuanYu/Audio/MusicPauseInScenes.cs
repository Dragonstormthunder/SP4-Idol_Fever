using UnityEngine;
using UnityEngine.SceneManagement;

namespace IdolFever {
    internal sealed class MusicPauseInScenes: MonoBehaviour {
        #region Fields

        [SerializeField] private MusicPauseControl musicPauseControl;
        [SerializeField] private string[] sceneNames;

        #endregion

        #region Properties
        #endregion

        public MusicPauseInScenes() {
            musicPauseControl = null;
            sceneNames = System.Array.Empty<string>();
        }

        #region Unity User Callback Event Funcs

        private void FixedUpdate() {
            foreach(string sceneName in sceneNames) {
                if(sceneName == SceneManager.GetActiveScene().name) {
                    musicPauseControl.PauseAllMusic();
                    break;
                }
            }
        }

        #endregion
    }
}