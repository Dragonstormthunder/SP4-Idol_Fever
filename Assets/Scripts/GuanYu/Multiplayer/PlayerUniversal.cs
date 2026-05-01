using UnityEngine;

namespace IdolFever {
    internal static class PlayerUniversal {
        #region Fields

        private static Color[] colors;

        #endregion

        #region Properties

        public static Color[] Colors {
            get {
                return colors;
            }
            set {
                colors = value;
            }
        }

        #endregion

        static PlayerUniversal() {
            colors = System.Array.Empty<Color>();
        }

        public static void InitColors() {
            float commonVal = Random.Range(0.0f, 1.0f);

            colors = new Color[]{
                new Color(0.0f, 0.0f, 0.0f, 1.0f),
                new Color(1.0f, 1.0f, 1.0f, 1.0f),
                new Color(commonVal, commonVal, commonVal, 1.0f),
                new Color(Random.Range(0.2f, 0.8f), 0.0f, 0.0f, 1.0f),
                new Color(0.0f, Random.Range(0.2f, 0.8f), 0.0f, 1.0f),
                new Color(0.0f, 0.0f, Random.Range(0.2f, 0.8f), 1.0f),
                new Color(Random.Range(0.2f, 0.8f), 0.0f, Random.Range(0.2f, 0.8f), 1.0f),
                new Color(Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f), 0.0f, 1.0f),
                new Color(0.0f, Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f), 1.0f),
                new Color(0.82f, 0.41f, 0.11f),
            };
            ShuffleElements.Shuffle(colors);
        }
    }
}