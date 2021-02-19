using UnityEngine;

namespace IdolFever {
    internal sealed class AsyncSceneTransitionOutWithAlts: MonoBehaviour {
        #region Fields

        [SerializeField] private AsyncSceneTransitionOut[] scripts;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public AsyncSceneTransitionOutWithAlts() {
            scripts = System.Array.Empty<AsyncSceneTransitionOut>();
        }

        public void ChangeScene() {
            foreach(AsyncSceneTransitionOut script in scripts) {
                if(script.SceneName == SceneTracker.prevSceneName) {
                    script.ChangeScene();
                    return;
                }
            }

            UnityEngine.Assertions.Assert.IsTrue(false);
        }
    }
}