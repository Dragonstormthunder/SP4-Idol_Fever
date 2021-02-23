using IdolFever.Server;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever {
    internal sealed class MultiplayerButton: MonoBehaviourPunCallbacks {
        #region Fields

        private Dictionary<string, RoomInfo> cachedRoomList;
        [SerializeField] private ServerDatabase serverDatabaseScript;
        [SerializeField] private AsyncSceneTransitionOut asyncSceneTransitionOutScript;
        [SerializeField] private PanelsControl panelsControl;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public MultiplayerButton() {
            cachedRoomList = new Dictionary<string, RoomInfo>();
            serverDatabaseScript = null;
            asyncSceneTransitionOutScript = null;
        }

        public void OnClick() {
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
            }

            CreateRoom();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList) {
            Debug.Log("OnRoomListUpdate");
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

        private void JoinRoom(string name) {
            if(PhotonNetwork.InLobby) {
                PhotonNetwork.LeaveLobby();
            }
            PhotonNetwork.JoinRoom(name);
        }

        private void CreateRoom() {
            string roomName = Random.Range(6969, 420420).ToString();

            RoomOptions options = new RoomOptions { MaxPlayers = 2, PlayerTtl = 10000 };

            PhotonNetwork.CreateRoom(roomName, options, null);

            asyncSceneTransitionOutScript.ChangeScene();
        }

        public override void OnCreatedRoom() {
            Debug.Log("OnCreatedRoom() : You Have Created...");
        }

        public override void OnJoinedRoom() {
            print("here000");
        }

/*        public override void OnJoinedRoom() {
            if(PlayerUniversal.Colors.Length == 0) {
                if(PhotonNetwork.IsMasterClient) {
                    PlayerUniversal.InitColors();
                } else {
                    PhotonView.Get(this).RPC("RetrievePlayerColors", RpcTarget.MasterClient);
                }
            }

            _ = StartCoroutine(panelsControl.My1stEverCoroutine());
        }*/
    }
}