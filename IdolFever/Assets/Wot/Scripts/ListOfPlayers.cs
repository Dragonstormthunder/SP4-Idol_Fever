using IdolFever.Server;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace IdolFever {
    internal sealed class ListOfPlayers: MonoBehaviourPunCallbacks {
        #region Fields

        private Dictionary<string, RoomInfo> cachedRoomList;
        [SerializeField] private GameObject[] playerBlocks;
        [SerializeField] private ServerDatabase serverDatabaseScript;

        #endregion

        #region Properties
        #endregion

        public ListOfPlayers() {
            cachedRoomList = null;
            playerBlocks = System.Array.Empty<GameObject>();
            serverDatabaseScript = null;
        }

        #region Unity User Callback Event Funcs

        private void Awake() {
            PhotonNetwork.AutomaticallySyncScene = false;

            cachedRoomList = new Dictionary<string, RoomInfo>();

            PhotonNetwork.LocalPlayer.NickName = GameConfigurations.Username;
            if(!PhotonNetwork.IsConnected) {
                PhotonNetwork.ConnectUsingSettings();
            }
		}

        #endregion

        #region Pun Callback Funcs

        public override void OnConnectedToMaster() {
            foreach(RoomInfo info in cachedRoomList.Values) {
                if(info.PlayerCount == 2) {
                    continue;
                }

                JoinRoom(info.Name);
                return;
            }

            CreateRoom();
        }

        private void JoinRoom(string name) {
            if(PhotonNetwork.InLobby) {
                PhotonNetwork.LeaveLobby();
            }
            PhotonNetwork.JoinRoom(name);
        }

        private void CreateRoom() {
            string roomName = Random.Range(400, 4000000).ToString();

            RoomOptions options = new RoomOptions { MaxPlayers = (byte)playerBlocks.Length, PlayerTtl = 10000 };

            PhotonNetwork.CreateRoom(roomName, options, null);


            Debug.Log(PhotonNetwork.InLobby, this);
        }

        public override void OnCreatedRoom() {
            Debug.Log("Room created!", this);
            Debug.Log(PhotonNetwork.InLobby, this);
        }

        public override void OnJoinedRoom() {
            Debug.Log("Room joined!", this);
            Debug.Log(PhotonNetwork.InLobby, this);

            int index = 1;
            foreach(Player player in PhotonNetwork.PlayerList) {
                GameObject playerBlockGO = playerBlocks[player == PhotonNetwork.LocalPlayer ? 0 : index];

                PlayerBlock playerBlockScript = playerBlockGO.GetComponent<PlayerBlock>();
                playerBlockScript.ActorNumber = player.ActorNumber;
                playerBlockScript.Nickname = player.NickName;

                TextMeshProUGUI tmpComponent = playerBlockGO.transform.Find("PlayerBlockText").GetComponent<TextMeshProUGUI>();
                tmpComponent.text = playerBlockScript.Nickname;

                if(player != PhotonNetwork.LocalPlayer) {
                    ++index;
                }
            }
                /*playerListEntry.Initialize(p.ActorNumber, p.NickName);
                playerListEntry.SetPlayerListEntryColors();

                playerListEntry.SetPlayerReady(false);
                Hashtable props = new Hashtable() { { "IsPlayerReady", false } };
                p.SetCustomProperties(props);

                playerListEntries.Add(p.ActorNumber, entry);*/

            /*PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("IsPlayerReady", out object fakeVal);
            Debug.Log(fakeVal, this);
            startButton.SetActive(CheckPlayersReady());*/
        }

        #endregion
    }
}