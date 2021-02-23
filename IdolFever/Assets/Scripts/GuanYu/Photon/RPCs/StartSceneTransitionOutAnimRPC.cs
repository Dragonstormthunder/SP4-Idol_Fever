using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IdolFever {
    internal sealed class StartSceneTransitionOutAnimRPC: MonoBehaviour {
        #region Fields

        [SerializeField] private string sceneName;
        [SerializeField] private AsyncSceneTransitionOut asyncSceneTransitionOutScript;
        [SerializeField] private Animator animator;
        [SerializeField] private string startAnimName;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public StartSceneTransitionOutAnimRPC() {
            sceneName = string.Empty;
            asyncSceneTransitionOutScript = null;
            animator = null;
            startAnimName = string.Empty;
        }

        [PunRPC] public void StartSceneTransitionOutAnim() {
            asyncSceneTransitionOutScript.ChangeScene();
        }
    }
}