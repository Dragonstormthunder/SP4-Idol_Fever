using IdolFever.Server;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever {
    internal sealed class ListOfPlayers: MonoBehaviourPunCallbacks {
        #region Fields

        private Dictionary<string, RoomInfo> cachedRoomList;
        private List<int> roomIndices;
        [SerializeField] private byte maxPlayers;
        [SerializeField] private int maxRooms;
        [SerializeField] private ServerDatabase serverDatabaseScript;

        #endregion

        #region Properties
        #endregion

        public ListOfPlayers() {
            cachedRoomList = null;
            roomIndices = null;
            maxPlayers = 0;
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

            _ = StartCoroutine(serverDatabaseScript.GetUsername((playerName) => {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                if(!PhotonNetwork.IsConnected) {
                    PhotonNetwork.ConnectUsingSettings();
                }
            }));
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

            RoomOptions options = new RoomOptions { MaxPlayers = maxPlayers, PlayerTtl = 10000 };

            PhotonNetwork.CreateRoom(roomName, options, null);
        }

        public override void OnCreatedRoom() {
            Debug.Log("Room created!", this);
        }

        public override void OnJoinedRoom() {
            Debug.Log("Room joined!", this);
        }

        #endregion
    }
}