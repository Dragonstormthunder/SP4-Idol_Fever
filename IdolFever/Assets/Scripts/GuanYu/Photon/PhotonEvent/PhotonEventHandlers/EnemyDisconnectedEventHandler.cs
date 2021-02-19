using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace IdolFever {
    internal sealed class EnemyDisconnectedEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields

        [SerializeField] private GameObject dcPrefab;
        [SerializeField] private GameObject parent;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        private EnemyDisconnectedEventHandler() {
            dcPrefab = null;
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        }

        private void OnDisable() {
            PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        }

        #endregion

        public void OnEvent(EventData photonEvent) {
            if(photonEvent.Code == (byte)EventCodes.EventCode.EnemyDisconnectedEvent) {
                GameObject dcGO = Instantiate(dcPrefab);

                if(parent != null) {
                    dcGO.transform.SetParent(parent.transform);
                }
            }
        }
    }
}