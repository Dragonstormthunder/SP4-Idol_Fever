﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace IdolFever {
    internal sealed class MultiplayerConnect: MonoBehaviourPunCallbacks {
        #region Fields

        private bool is1stUpdated;
        private Dictionary<string, RoomInfo> cachedRoomList;
        [SerializeField] private byte maxPlayers;
        [SerializeField] private AsyncSceneTransitionOut asyncSceneTransitionOut;

        #endregion

        #region Properties
        #endregion

        public MultiplayerConnect() {
            is1stUpdated = false;
            cachedRoomList = null;
            maxPlayers = 0;
            asyncSceneTransitionOut = null;
        }

        #region Unity User Callback Event Funcs

        private void Awake() {
            PhotonNetwork.AutomaticallySyncScene = false;

            PhotonNetwork.LocalPlayer.NickName = GameConfigurations.Username;

            if(!PhotonNetwork.IsConnected) {
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        #endregion

        #region Pun Callback Funcs

        public override void OnConnectedToMaster() {
            if(!PhotonNetwork.InLobby) {
                PhotonNetwork.JoinLobby();
            }
        }

        public override void OnJoinedLobby() {
            cachedRoomList = new Dictionary<string, RoomInfo>();

            _ = StartCoroutine(nameof(RoomListFunc));
        }

        private System.Collections.IEnumerator RoomListFunc() {
            while(!is1stUpdated) {
                yield return null;
            }

            foreach(RoomInfo info in cachedRoomList.Values) {
                if(info.PlayerCount == info.MaxPlayers) {
                    continue;
                }
                if(info.CustomProperties.TryGetValue("songSelected", out object songSelected)) { //Inline var declaration
                    if((int)GameConfigurations.SongChosen != (int)songSelected) {
                        Debug.Log("here1", this);
                        continue;
                    }
                } else {
                    Debug.Log("here2", this);
                    continue;
                }

                Debug.Log("here3", this);
                JoinRoom(info.Name);
                yield break;
            }

            Debug.Log("here4", this);
            CreateRoom();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList) {
            Debug.Log("here_la2");

            if(!is1stUpdated) {
                is1stUpdated = true;
            }

            UpdateCachedRoomList(roomList);
        }

        public override void OnCreatedRoom() {
            Debug.Log("Room created!", this);

            if(PhotonNetwork.InLobby) {
                PhotonNetwork.LeaveLobby();
            }
        }

        public override void OnJoinedRoom() {
            Debug.Log("Room joined!", this);

            asyncSceneTransitionOut.ChangeScene();
        }

        public override void OnRoomPropertiesUpdate(Hashtable hashtable) {
            Debug.Log("here_la");
        }

        public override void OnCreateRoomFailed(short returnCode, string message) {
            Debug.LogError("OnCreateRoomFailed " + '(' + returnCode + "): " + message);
        }

        public override void OnJoinRoomFailed(short returnCode, string message) {
            Debug.LogError("OnJoinRoomFailed " + '(' + returnCode + "): " + message);
        }




        public override void OnMasterClientSwitched(Player newMasterClient) {
            /*if(PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber) {
                StartGameButton.gameObject.SetActive(CheckPlayersReady());
            }*/
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) {
            /*if(playerListEntries == null) {
                playerListEntries = new Dictionary<int, GameObject>();
            }

            if(playerListEntries.TryGetValue(targetPlayer.ActorNumber, out GameObject entry)) { //Inline var declaration
                if(changedProps.TryGetValue("IsPlayerReady", out object isPlayerReady)) { //Inline var declaration
                    entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool)isPlayerReady);
                }
            }*/

            //StartGameButton.gameObject.SetActive(CheckPlayersReady()); //here
        }

        #endregion

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
            PhotonNetwork.JoinRoom(name);
        }

        private void CreateRoom() {
            RoomOptions options = new RoomOptions {
                MaxPlayers = maxPlayers,
                PlayerTtl = 0,
                CustomRoomPropertiesForLobby = new string[] { "songSelected" },
                CustomRoomProperties = new Hashtable { { "songSelected", (int)GameConfigurations.SongChosen } }
            };

            PhotonNetwork.CreateRoom(null, options, null);
        }
    }
}