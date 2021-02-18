using UnityEngine;

namespace IdolFever {
    internal static class Options {
        #region Fields

        private static float musicVol;
        private static float soundVol;
        private static GraphicsQualityOptions.GraphicsQualityOption currGraphicsOption;

        #endregion

        #region Properties

        public static GraphicsQualityOptions.GraphicsQualityOption CurrGraphicsOption {
            get {
                return currGraphicsOption;
            }
            private set {
            }
        }

        #endregion

        static Options() {
            musicVol = 0.0f;
            soundVol = 0.0f;
            currGraphicsOption = GraphicsQualityOptions.GraphicsQualityOption.NotAnOption;
        }

        public static void SetMusicVol(float _musicVol) {
            musicVol = _musicVol;
        }

        public static void SetSoundVol(float _soundVol) {
            soundVol = _soundVol;
        }

        public static void ChangeToLowGraphics() {
            currGraphicsOption = GraphicsQualityOptions.GraphicsQualityOption.Low;
        }

        public static void ChangeToHighGraphics() {
            currGraphicsOption = GraphicsQualityOptions.GraphicsQualityOption.High;
        }
    }
}