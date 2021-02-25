using UnityEngine;

namespace IdolFever {
    internal static class Options {
        #region Fields

        private static float musicVol;
        private static float soundVol;
        private static GraphicsQualityOptions.GraphicsQualityOption graphicsOption;

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

        public static GraphicsQualityOptions.GraphicsQualityOption GraphicsOption {
            get {
                if(graphicsOption == GraphicsQualityOptions.GraphicsQualityOption.NotAnOption) {
                    graphicsOption = (GraphicsQualityOptions.GraphicsQualityOption)PlayerPrefs.GetInt("graphicsOption", (int)GraphicsQualityOptions.GraphicsQualityOption.High);
                }

                return graphicsOption;
            }
        }

        #endregion

        static Options() {
            musicVol = -1.0f;
            soundVol = -1.0f;
            graphicsOption = GraphicsQualityOptions.GraphicsQualityOption.NotAnOption;
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
            graphicsOption = GraphicsQualityOptions.GraphicsQualityOption.Low;
            PlayerPrefs.SetInt("graphicsOption", (int)graphicsOption);
        }

        public static void ChangeToHighGraphics() {
            graphicsOption = GraphicsQualityOptions.GraphicsQualityOption.High;
            PlayerPrefs.SetInt("graphicsOption", (int)graphicsOption);
        }
    }
}