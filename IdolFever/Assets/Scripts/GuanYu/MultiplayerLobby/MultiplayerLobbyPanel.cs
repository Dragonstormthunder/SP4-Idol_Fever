using ExitGames.Client.Photon;
using IdolFever.Server;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever {
    internal sealed class MultiplayerLobbyPanel: MonoBehaviourPunCallbacks {
        #region Fields

        private Dictionary<string, RoomInfo> cachedRoomList;
        private Dictionary<int, GameObject> playerListEntries;
        [SerializeField] private GameObject playerListEntryPrefab;
        [SerializeField] private GameObject startButton;
        [SerializeField] private ServerDatabase serverDatabaseScript;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            _ = StartCoroutine(serverDatabaseScript.GetUsername((playerName) => {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                if(!PhotonNetwork.IsConnected) {
                    PhotonNetwork.ConnectUsingSettings();
                }
            }));
        }

        #endregion

        public MultiplayerLobbyPanel() {
            cachedRoomList = new Dictionary<string, RoomInfo>();
            playerListEntries = null;
            startButton = null;
            playerListEntryPrefab = null;
            serverDatabaseScript = null;
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
            Debug.Log("OnRoomListUpdate"); //??
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
        }

        public override void OnCreatedRoom() {
            Debug.Log("OnCreatedRoom(): You Have Created...");
        }

		public override void OnJoinedRoom() {
			if(PlayerUniversal.Colors.Length == 0) {
				if(PhotonNetwork.IsMasterClient) {
					PlayerUniversal.InitColors();
				} else {
					PhotonView.Get(this).RPC("RetrievePlayerColors", RpcTarget.MasterClient);
				}
			}

			_ = StartCoroutine(My1stEverCoroutine());
		}

        private System.Collections.IEnumerator My1stEverCoroutine() {
            while(PlayerUniversal.Colors.Length == 0) {
                yield return null;
            }

            if(playerListEntries == null) {
                playerListEntries = new Dictionary<int, GameObject>();
            }

            foreach(Player p in PhotonNetwork.PlayerList) {
                GameObject entry = Instantiate(playerListEntryPrefab);
                entry.transform.SetParent(transform);
                entry.transform.localScale = Vector3.one;

                PlayerListEntry playerListEntry = entry.GetComponent<PlayerListEntry>();
                playerListEntry.Initialize(p.ActorNumber, p.NickName);
                playerListEntry.SetPlayerListEntryColors();

                playerListEntry.SetPlayerReady(false);
                Hashtable props = new Hashtable() { { "IsPlayerReady", false } };
                p.SetCustomProperties(props);

                playerListEntries.Add(p.ActorNumber, entry);
            }

            PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("IsPlayerReady", out object fakeVal);
            Debug.Log(fakeVal, this);
            startButton.SetActive(CheckPlayersReady());

            yield return null;
        }

        private bool CheckPlayersReady() {
            if(!PhotonNetwork.IsMasterClient) {
                return false;
            }

            foreach(Player p in PhotonNetwork.PlayerList) {
                if(p.CustomProperties.TryGetValue("IsPlayerReady", out object isPlayerReady)) { //Inline var declaration
                    if(!(bool)isPlayerReady) {
                        return false;
                    }
                } else {
                    return false;
                }
            }

            return true;
        }

        public void LocalPlayerPropertiesUpdated() {
            startButton.SetActive(CheckPlayersReady());
        }
    }
}