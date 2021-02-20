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

        [SerializeField] private float spd;
        [SerializeField] private float x;
        [SerializeField] private float startY;
        [SerializeField] private float endY;
        [SerializeField] private float startSize;
        [SerializeField] private float endSize;
        [SerializeField] private float startAlpha;
        [SerializeField] private float endAlpha;

        #endregion

        #region Properties

        public static bool IsLocalPlayer {
            get;
            set;
        }

        #endregion

        #region Ctors and Dtor

        static EnemyDisconnectedEventHandler() {
            IsLocalPlayer = false;
        }

        private EnemyDisconnectedEventHandler() {
            dcPrefab = null;
            parent = null;
            myName = string.Empty;
            siblingIndex = 0;

            spd = 0.0f;
            x = 0.0f;
            startY = 0.0f;
            endY = 0.0f;
            startSize = 0.0f;
            endSize = 0.0f;
            startAlpha = 0.0f;
            endAlpha = 0.0f;
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
                IsLocalPlayer = PhotonNetwork.LocalPlayer.ActorNumber == (int)photonEvent.CustomData;

                if(!IsLocalPlayer) {
                    GameObject dcGO = Instantiate(dcPrefab, new Vector3(x, startY, 0.0f), Quaternion.identity);
                    dcGO.name = myName;

                    DcTextAnim script = dcGO.GetComponent<DcTextAnim>();
                    script.spd = spd;
                    script.startY = startY;
                    script.endY = endY;
                    script.startSize = startSize;
                    script.endSize = endSize;
                    script.startAlpha = startAlpha;
                    script.endAlpha = endAlpha;

                    if(parent != null) {
                        dcGO.transform.SetParent(parent.transform, false);
                        dcGO.transform.SetSiblingIndex(siblingIndex);
                    }
                }
            }
        }
    }
}