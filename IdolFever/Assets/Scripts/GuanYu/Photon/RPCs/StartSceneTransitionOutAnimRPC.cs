using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IdolFever {
    internal sealed class StartSceneTransitionOutAnimRPC: MonoBehaviour {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        [PunRPC] public void StartSceneTransitionOutAnim(string GOName) {
            PanelsControl.IsStartSceneTransitionOutAnimReceived = true;

            GameObject sceneTransitionGO = GameObject.Find(GOName);
            Animator animator = sceneTransitionGO.GetComponent<Animator>();

            GameObject progressBarGO = animator.transform.Find("ProgressBar").gameObject;
            GameObject progressGO = progressBarGO.transform.Find("Progress").gameObject;
            Image img = progressGO.GetComponent<Image>();

            img.fillAmount = 0.0f;

            animator.SetTrigger("Start");

            SceneTracker.prevSceneName = SceneManager.GetActiveScene().name;
        }
    }
}