using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever {
    internal sealed class MultiplayerConnect: MonoBehaviourPunCallbacks {
        #region Fields

        [SerializeField] private AsyncSceneTransitionOut asyncSceneTransitionOut;

        #endregion

        #region Properties
        #endregion

        public MultiplayerConnect() {
            asyncSceneTransitionOut = null;
        }

        #region Unity User Callback Event Funcs

        private void Awake() {
            PhotonNetwork.AutomaticallySyncScene = false;

            PhotonNetwork.LocalPlayer.NickName = GameConfigurations.Username;

            if(!PhotonNetwork.IsConnected) {
                Debug.Log("here");
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        #endregion

        #region Pun Callback Funcs

        public override void OnConnectedToMaster() {
            Debug.Log(PhotonNetwork.InLobby, this);

            asyncSceneTransitionOut.ChangeScene();

            Debug.Log(PhotonNetwork.InLobby, this);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList) {
            /*ClearRoomListView();
            UpdateCachedRoomList(roomList);
            UpdateRoomListView();*/
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

        public override void OnJoinedRoom() {
            /*if(PlayerUniversal.Colors.Length == 0) {
                if(PhotonNetwork.IsMasterClient) {
                    PlayerUniversal.InitColors();
                } else {
                    PhotonView.Get(this).RPC("RetrievePlayerColors", RpcTarget.MasterClient);
                }
            }

            _ = StartCoroutine(nameof(My1stEverCoroutine));*/
        }

        private System.Collections.IEnumerator My1stEverCoroutine() {
            /*while(PlayerUniversal.Colors.Length == 0) {
                yield return null;
            }

            SetActivePanel(InsideRoomPanel.name);

            if(playerListEntries == null) {
                playerListEntries = new Dictionary<int, GameObject>();
            }

            foreach(Player p in PhotonNetwork.PlayerList) {
                GameObject entry = Instantiate(PlayerListEntryPrefab);
                entry.transform.SetParent(InsideRoomPanel.transform);
                entry.transform.localScale = Vector3.one;
                entry.GetComponent<PlayerListEntry>().Initialize(p.ActorNumber, p.NickName);

                if(p.CustomProperties.TryGetValue("IsPlayerReady", out object isPlayerReady)) { //Inline var declaration
                    entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool)isPlayerReady);
                }

                entry.GetComponent<PlayerListEntry>().SetPlayerListEntryColors();

                playerListEntries.Add(p.ActorNumber, entry);
            }

            StartGameButton.gameObject.SetActive(CheckPlayersReady());

            Hashtable props = new Hashtable {
                {"PlayerLoadedLevel", false}
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);*/

            yield return null;
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
    }
}