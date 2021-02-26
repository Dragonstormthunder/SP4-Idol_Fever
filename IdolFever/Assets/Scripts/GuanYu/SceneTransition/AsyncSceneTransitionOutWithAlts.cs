using Photon.Pun;
using System;
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
            scripts = Array.Empty<AsyncSceneTransitionOut>();
        }

        public void ChangeSceneByPrevSceneName() {
            foreach(AsyncSceneTransitionOut script in scripts) {
                if(script.SceneName == SceneTracker.prevSceneName) {
                    script.ChangeScene();
                    return;
                }
            }

            UnityEngine.Assertions.Assert.IsTrue(false);
        }

        /*public void ChangeSceneBy() {
            scripts[Convert.ToInt32(PhotonNetwork.IsConnected)].ChangeScene();
        }*/
    }
}