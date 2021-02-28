using Photon.Pun;
using UnityEngine;

namespace IdolFever {
    internal sealed class SearchingTextActivity: MonoBehaviour {
        #region Fields

        [SerializeField] private GameObject searchingTextGameObject;

        #endregion

        #region Properties
        #endregion

        public SearchingTextActivity() {
            searchingTextGameObject = null;
        }

        #region Unity User Callback Event Funcs

        private void Update() {
            searchingTextGameObject.SetActive(PhotonNetwork.CurrentRoom.PlayerCount == 1);
        }

        #endregion
    }
}