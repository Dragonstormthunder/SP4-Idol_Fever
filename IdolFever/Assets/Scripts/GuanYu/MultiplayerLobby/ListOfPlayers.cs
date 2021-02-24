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
        private List<int> roomIndices;
        [SerializeField] private GameObject[] playerBlocks;
        [SerializeField] private int maxRooms;
        [SerializeField] private ServerDatabase serverDatabaseScript;

        #endregion

        #region Properties
        #endregion

        public ListOfPlayers() {
            cachedRoomList = null;
            roomIndices = null;
            playerBlocks = System.Array.Empty<GameObject>();
            maxRooms = 0;
            serverDatabaseScript = null;
        }

        #region Unity User Callback Event Funcs

        private void Awake() {
            cachedRoomList = new Dictionary<string, RoomInfo>();

            roomIndices = new List<int>();
            for(int i = 0; i < maxRooms; ++i) {
                roomIndices.Add(i);
            }

            PhotonNetwork.LocalPlayer.NickName = GameConfigurations.Username;
            if(!PhotonNetwork.IsConnected) {
                PhotonNetwork.ConnectUsingSettings();
            }
		}

        public override void OnRoomListUpdate(List<RoomInfo> roomList) {
            Debug.Log("OnRoomListUpdate", this);
            UpdateCachedRoomList(roomList);
        }

        private void UpdateCachedRoomList(List<RoomInfo> roomList) {
            foreach(RoomInfo info in roomList) {
                ///Remove room from cached room list if it got closed, became invisible or was marked as removed
                if(!info.IsOpen || !info.IsVisible || info.RemovedFromList) {
                    if(cachedRoomList.ContainsKey(info.Name)) {
                        cachedRoomList.Remove(info.Name);
                    }
                    continue;
                }

                if(cachedRoomList.ContainsKey(info.Name)) {
                    cachedRoomList[info.Name] = info; //Update cached room info
                } else {
                    cachedRoomList.Add(info.Name, info); //Add new room info to cache
                }
            }
        }

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
            string roomName = roomIndices[0].ToString();
            roomIndices.RemoveAt(0);

            RoomOptions options = new RoomOptions { MaxPlayers = (byte)playerBlocks.Length, PlayerTtl = 10000 };

            PhotonNetwork.CreateRoom(roomName, options, null);
        }

        public override void OnCreatedRoom() {
            Debug.Log("Room created!", this);
        }

        public override void OnJoinedRoom() {
            Debug.Log("Room joined!", this);

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