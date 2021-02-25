using UnityEngine;

namespace IdolFever {
    internal static class VibrationControl {
        #region Fields

        #if UNITY_ANDROID && !UNITY_EDITOR
            public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
        #else
            public static AndroidJavaObject vibrator;
        #endif

        #endregion

        #region Properties
        #endregion

        public static void StartVibration() {
            if(isAndroid()) {
                vibrator.Call("vibrate");
            }
        }

        public static void StartVibration(long milliseconds) {
            if(isAndroid()) {
                vibrator.Call("vibrate", milliseconds);
            }
        }

        public static void StartVibration(long[] pattern, int repeat) {
            if(isAndroid()) {
                vibrator.Call("vibrate", pattern, repeat);
            }
        }

        public static void CancelVibration() {
            if(isAndroid()) {
                vibrator.Call("cancel");
            }
        }

        private static bool isAndroid() {
            #if UNITY_ANDROID && !UNITY_EDITOR
	            return true;
            #else
                return false;
            #endif
        }
    }
}