using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IdolFever {
    internal sealed class ToGameplayRPC: MonoBehaviour {
        #region Fields

        [SerializeField] private AsyncSceneTransitionOut asyncSceneTransitionOutScript;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public ToGameplayRPC() {
            asyncSceneTransitionOutScript = null;
        }

        [PunRPC] public void ToGameplay() {
            asyncSceneTransitionOutScript.ChangeScene();
        }
    }
}