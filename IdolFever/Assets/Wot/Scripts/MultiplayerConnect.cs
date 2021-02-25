using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever {
    internal sealed class MultiplayerConnect: MonoBehaviourPunCallbacks {
        #region Fields

        private Dictionary<string, RoomInfo> cachedRoomList;
        [SerializeField] private AsyncSceneTransitionOut asyncSceneTransitionOut;

        #endregion

        #region Properties
        #endregion

        public MultiplayerConnect() {
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
            
            //start coroutine??
            
            foreach(RoomInfo info in cachedRoomList.Values) {
                if(info.PlayerCount == 2) {
                    continue;
                }

                JoinRoom(info.Name);
                return;
            }

            CreateRoom();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList) {
            UpdateCachedRoomList(roomList);
        }

        public override void OnCreatedRoom() {
            Debug.Log("Room created!", this);
        }

        public override void OnJoinedRoom() {
            Debug.Log("Room joined!", this);
        }

        public override void OnLeftLobby() {
            //cachedRoomList.Clear();
            //ClearRoomListView();
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

        public override void OnPlayerEnteredRoom(Player newPlayer) {
            //StartCoroutine(My2ndEverCoroutine(newPlayer));
        }

        private System.Collections.IEnumerator My2ndEverCoroutine(Player newPlayer) {
            /*while(PlayerUniversal.Colors.Length == 0) {
                yield return null;
            }

            GameObject entry = Instantiate(PlayerListEntryPrefab);
            entry.transform.SetParent(InsideRoomPanel.transform);
            entry.transform.localScale = Vector3.one;
            entry.GetComponent<PlayerListEntry>().Initialize(newPlayer.ActorNumber, newPlayer.NickName);

            entry.GetComponent<PlayerListEntry>().SetPlayerListEntryColors();

            playerListEntries.Add(newPlayer.ActorNumber, entry);

            StartGameButton.gameObject.SetActive(CheckPlayersReady());*/

            yield return null;
        }

        public override void OnPlayerLeftRoom(Player otherPlayer) {
            /*Destroy(playerListEntries[otherPlayer.ActorNumber].gameObject);
            playerListEntries.Remove(otherPlayer.ActorNumber);

            StartGameButton.gameObject.SetActive(CheckPlayersReady());*/
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