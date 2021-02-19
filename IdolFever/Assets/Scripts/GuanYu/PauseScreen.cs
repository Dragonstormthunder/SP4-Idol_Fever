using UnityEngine;

namespace IdolFever {
    internal sealed class PauseScreen: MonoBehaviour {
        #region Fields

        public static bool isPaused;
        [SerializeField] private GameObject pauseButton;
        [SerializeField] private MusicPauseControl musicPauseControl;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        static PauseScreen() {
            isPaused = false;
        }

        public PauseScreen() {
            pauseButton = null;
            musicPauseControl = null;
        }

        public void ActivateSelf() {
            gameObject.SetActive(true);
            pauseButton.SetActive(false);
            musicPauseControl.PauseAllMusic();
            isPaused = true;
        }

        public void DeactivateSelf() {
            gameObject.SetActive(false);
            pauseButton.SetActive(true);
            musicPauseControl.PlayAllMusic();
            isPaused = false;
        }
    }
}