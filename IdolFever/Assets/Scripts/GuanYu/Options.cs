using UnityEngine;

namespace IdolFever {
    internal static class Options {
        #region Fields

        private static float musicVol;
        private static float soundVol;
        private static GraphicsQualityOptions.GraphicsQualityOption currGraphicsOption;

        #endregion

        #region Properties

        public static float MusicVol {
            get {
                if(musicVol < 0.0f) {
                    musicVol = PlayerPrefs.GetFloat("musicVol", 1.0f);
                }

                return musicVol;
            }
        }

        public static float SoundVol {
            get {
                if(soundVol < 0.0f) {
                    soundVol = PlayerPrefs.GetFloat("soundVol", 1.0f);
                }

                return soundVol;
            }
        }

        public static GraphicsQualityOptions.GraphicsQualityOption CurrGraphicsOption {
            get {
                return currGraphicsOption;
            }
        }

        #endregion

        static Options() {
            musicVol = -1.0f;
            soundVol = -1.0f;
            currGraphicsOption = GraphicsQualityOptions.GraphicsQualityOption.NotAnOption;
        }

        public static void SetMusicVol(float _musicVol) {
            musicVol = _musicVol;
            PlayerPrefs.SetFloat("musicVol", musicVol);
        }

        public static void SetSoundVol(float _soundVol) {
            soundVol = _soundVol;
            PlayerPrefs.SetFloat("soundVol", soundVol);
        }

        public static void ChangeToLowGraphics() {
            currGraphicsOption = GraphicsQualityOptions.GraphicsQualityOption.Low;
        }

        public static void ChangeToHighGraphics() {
            currGraphicsOption = GraphicsQualityOptions.GraphicsQualityOption.High;
        }
    }
}