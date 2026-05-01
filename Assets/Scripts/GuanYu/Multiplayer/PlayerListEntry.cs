using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace IdolFever {
    internal sealed class PlayerListEntry: MonoBehaviour {
        #region Fields

        private bool isPlayerReady;
        private int ownerID;

        [Header("UI Refs")]
        [SerializeField] private Text PlayerNameText;
        [SerializeField] private Image PlayerColorImage;
        [SerializeField] private Button PlayerReadyButton;
        [SerializeField] private Image PlayerReadyImage;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Start() {
            if(PhotonNetwork.LocalPlayer.ActorNumber != ownerID) {
                PlayerReadyButton.gameObject.SetActive(false);
            } else {
                PlayerReadyButton.onClick.AddListener(() => {
                    isPlayerReady = !isPlayerReady;
                    SetPlayerReady(isPlayerReady);

                    Hashtable props = new Hashtable() { { "IsPlayerReady", isPlayerReady } };
                    PhotonNetwork.LocalPlayer.SetCustomProperties(props);

                    /*PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("IsPlayerReady", out object fakeVal);
                    Debug.Log(isPlayerReady, this);
                    Debug.Log((bool)fakeVal, this);*/

                    /*if(PhotonNetwork.IsMasterClient) {
                        FindObjectOfType<PanelsControl>().LocalPlayerPropertiesUpdated();
                    }*/
                });
            }
        }

        #endregion

        public PlayerListEntry() {
            isPlayerReady = false;
            ownerID = 0;
            PlayerNameText = null;
            PlayerColorImage = null;
            PlayerReadyButton = null;
            PlayerReadyImage = null;
        }

        public void SetPlayerListEntryColors() {
            int playerListArrLen = PhotonNetwork.PlayerList.Length;
            for(int i = 0; i < playerListArrLen; ++i) {
                if(PhotonNetwork.PlayerList[i].ActorNumber == ownerID) {
                    PlayerColorImage.color = PlayerUniversal.Colors[i];
                    break;
                }
            }
        }

        public void Initialize(int playerId, string playerName) {
            ownerID = playerId;
            PlayerNameText.text = playerName;
        }

        public void SetPlayerReady(bool playerReady) {
            PlayerReadyButton.GetComponentInChildren<Text>().text = playerReady ? "Very Ready" : "Not Ready";
            PlayerReadyImage.enabled = playerReady;
        }
    }
}