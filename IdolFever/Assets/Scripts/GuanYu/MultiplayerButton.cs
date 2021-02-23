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

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public MultiplayerButton() {
            cachedRoomList = new Dictionary<string, RoomInfo>();
            serverDatabaseScript = null;
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

                //Join room??
            }

            //Create room??
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList) {
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
    }
}