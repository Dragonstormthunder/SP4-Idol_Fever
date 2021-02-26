using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace IdolFever {
    internal sealed class ListOfPlayers: MonoBehaviourPunCallbacks {
        #region Fields

        [SerializeField] private GameObject[] playerBlocks;

        #endregion

        #region Properties
        #endregion

        public ListOfPlayers() {
            playerBlocks = System.Array.Empty<GameObject>();
        }

        #region Unity User Callback Event Funcs

        private void Start() {
            if(!PhotonNetwork.IsConnected) {
                Debug.LogWarning("Photon is not connected.", this);
                return;
            }

            UpdatePlayerBlocks();
        }

        #endregion

        #region Pun Callback Funcs

        public override void OnPlayerEnteredRoom(Player newPlayer) {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount, this);

            UpdatePlayerBlocks();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer) {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount, this);

            UpdatePlayerBlocks();
        }

        #endregion

        private void UpdatePlayerBlocks() {
            int amtOfPlayerBlocks = playerBlocks.Length;
            Player[] players = PhotonNetwork.PlayerList;
            int index = 0;

            for(int i = 0; i < amtOfPlayerBlocks; ++i){
                GameObject playerBlockGO = playerBlocks[i];

                PlayerBlock playerBlockScript = playerBlockGO.GetComponent<PlayerBlock>();

                if(i < PhotonNetwork.CurrentRoom.PlayerCount) {
                    if(i == 0) {
                        playerBlockScript.ActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
                        playerBlockScript.Nickname = PhotonNetwork.LocalPlayer.NickName;
                    } else {
                        Player myPlayer = players[index];

                        if(myPlayer == PhotonNetwork.LocalPlayer) {
                            myPlayer = players[++index];
                        }

                        playerBlockScript.ActorNumber = myPlayer.ActorNumber;
                        playerBlockScript.Nickname = myPlayer.NickName;
                    }
                } else {
                    playerBlockScript.ActorNumber = -999;
                    playerBlockScript.Nickname = string.Empty;
                }

                TextMeshProUGUI tmpComponent = playerBlockGO.transform.Find("PlayerBlockText").GetComponent<TextMeshProUGUI>();
                tmpComponent.text = playerBlockScript.Nickname;
            }
        }

        public void OnStartButtonClick() {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            PhotonView.Get(this).RPC("ToGameplay", RpcTarget.All, null);
        }
    }
}