﻿using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever {
    internal sealed class MultiplayerConnect: MonoBehaviourPunCallbacks {
        #region Fields

        private bool is1stUpdated;
        private Dictionary<string, RoomInfo> cachedRoomList;
        [SerializeField] private AsyncSceneTransitionOut asyncSceneTransitionOut;

        #endregion

        #region Properties
        #endregion

        public MultiplayerConnect() {
            is1stUpdated = false;
            cachedRoomList = null;
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
                if(info.PlayerCount == 2) {
                    continue;
                }

                JoinRoom(info.Name);
                yield break;
            }

            CreateRoom();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList) {
            if(!is1stUpdated) {
                is1stUpdated = true;
            }

            UpdateCachedRoomList(roomList);
        }

        public override void OnCreatedRoom() {
            Debug.Log("Room created!", this);

            /*if(PhotonNetwork.InLobby) {
                PhotonNetwork.LeaveLobby();
            }*/
        }

        public override void OnJoinedRoom() {
            Debug.Log("Room joined!", this);

            asyncSceneTransitionOut.ChangeScene();
        }

        public override void OnCreateRoomFailed(short returnCode, string message) {
            Debug.LogError("OnCreateRoomFailed " + '(' + returnCode + "): " + message);
        }

        public override void OnJoinRoomFailed(short returnCode, string message) {
            Debug.LogError("OnJoinRoomFailed " + '(' + returnCode + "): " + message);
        }





        public override void OnLeftRoom() {
            /*SetActivePanel(SelectionPanel.name);

            foreach(GameObject entry in playerListEntries.Values) {
                Destroy(entry.gameObject);
            }

            playerListEntries.Clear();
            playerListEntries = null;*/
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
            string roomName = Random.Range(400, 4000000).ToString();

            RoomOptions options = new RoomOptions { MaxPlayers = 2, PlayerTtl = 10000 };

            PhotonNetwork.CreateRoom(roomName, options, null);
        }
    }
}