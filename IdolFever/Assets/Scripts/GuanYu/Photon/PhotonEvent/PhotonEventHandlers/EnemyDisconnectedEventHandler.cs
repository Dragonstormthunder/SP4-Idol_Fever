using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace IdolFever {
    internal sealed class EnemyDisconnectedEventHandler: MonoBehaviour, IOnEventCallback {
        #region Fields

        [SerializeField] private GameObject dcPrefab;
        [SerializeField] private GameObject parent;
        [SerializeField] private string myName;
        [SerializeField] private int siblingIndex;
        [SerializeField] private float x;
        [SerializeField] private float y;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        private EnemyDisconnectedEventHandler() {
            dcPrefab = null;
            parent = null;
            myName = string.Empty;
            siblingIndex = 0;
            x = 0.0f;
            y = 0.0f;
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            GameObject dcGO = Instantiate(dcPrefab, new Vector3(x, y, 0.0f), Quaternion.identity);
            dcGO.name = myName;

            if(parent != null) {
                dcGO.transform.SetParent(parent.transform, false);
                dcGO.transform.SetSiblingIndex(siblingIndex);
            }
        }

        private void OnEnable() {
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        }

        private void OnDisable() {
            PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        }

        #endregion

        public void OnEvent(EventData photonEvent) {
            if(photonEvent.Code == (byte)EventCodes.EventCode.EnemyDisconnectedEvent) {
                GameObject dcGO = Instantiate(dcPrefab, new Vector3(x, y, 0.0f), Quaternion.identity);
                dcGO.name = myName;

                if(parent != null) {
                    dcGO.transform.SetParent(parent.transform, false);
                    dcGO.transform.SetSiblingIndex(siblingIndex);
                }
            }
        }
    }
}