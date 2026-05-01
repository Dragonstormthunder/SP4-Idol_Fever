using UnityEngine;

namespace IdolFever {
    internal static class SceneTracker {
        #region Fields

        [HideInInspector] public static string prevSceneName;

        #endregion

        #region Properties
        #endregion

        static SceneTracker() {
            prevSceneName = string.Empty;
        }
    }
}