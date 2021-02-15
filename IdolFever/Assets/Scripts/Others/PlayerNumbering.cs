using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace IdolFever {
    internal sealed class PlayerNumbering: MonoBehaviourPunCallbacks {
        #region Fields

        private static PlayerNumbering instance;

        public static Player[] SortedPlayers;

        private delegate void PlayerNumberingChanged();
        private static event PlayerNumberingChanged OnPlayerNumberingChanged;

        public const string RoomPlayerIndexedProp = "pNr";

        [SerializeField] private bool dontDestroyOnLoad;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            if(instance != null && instance != this && instance.gameObject != null) {
                DestroyImmediate(instance.gameObject);
            }

            instance = this;
            if(dontDestroyOnLoad) { 
                DontDestroyOnLoad(gameObject);
            }

            RefreshData();
        }

        #endregion

        public PlayerNumbering() {
            dontDestroyOnLoad = false;
        }

        public override void OnJoinedRoom() {
            RefreshData();
        }

        public override void OnLeftRoom() {
            PhotonNetwork.LocalPlayer.CustomProperties.Remove(RoomPlayerIndexedProp);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer) {
            RefreshData();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer) {
            RefreshData();
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) {
            if(changedProps != null && changedProps.ContainsKey(RoomPlayerIndexedProp)) {
                RefreshData();
            }
        }

        private void RefreshData() {
            if(PhotonNetwork.CurrentRoom == null) {
                return;
            }

            if(PhotonNetwork.LocalPlayer.GetPlayerNumber() >= 0) {
                SortedPlayers = PhotonNetwork.CurrentRoom.Players.Values.OrderBy((p) => p.GetPlayerNumber()).ToArray();
                if(OnPlayerNumberingChanged != null) {
                    OnPlayerNumberingChanged();
                }
                return;
            }

            HashSet<int> usedInts = new HashSet<int>();
            Player[] sorted = PhotonNetwork.PlayerList.OrderBy((p) => p.ActorNumber).ToArray();

            string allPlayers = "all players: ";
            foreach(Player player in sorted) {
                allPlayers += player.ActorNumber + "=pNr:" + player.GetPlayerNumber() + ", ";

                int number = player.GetPlayerNumber();

                if(player.IsLocal) {
                    Debug.Log("PhotonNetwork.CurrentRoom.PlayerCount = " + PhotonNetwork.CurrentRoom.PlayerCount);

                    for(int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; ++i) {
                        if(!usedInts.Contains(i)) {
                            player.SetPlayerNumber(i);
                            break;
                        }
                    }

                    break;
                } else {
                    if(number < 0) {
                        break;
                    } else {
                        usedInts.Add(number);
                    }
                }
            }

            SortedPlayers = PhotonNetwork.CurrentRoom.Players.Values.OrderBy((p) => p.GetPlayerNumber()).ToArray();
            if(OnPlayerNumberingChanged != null) {
                OnPlayerNumberingChanged();
            }
        }
    }

    /// <summary>Extension used for PlayerRoomIndexing and Player class.</summary>
    internal static class PlayerNumberingExtensions {
        /// <summary>Extension for Player class to wrap up access to the player's custom property.
		/// Make sure you use the delegate 'OnPlayerNumberingChanged' to know when you can query the PlayerNumber. Numbering can change over time or not be yet assigned during the initial phase ( when player creates a room for example)
		/// </summary>
        /// <returns>persistent index in room. -1 for no indexing</returns>
        public static int GetPlayerNumber(this Player player) {
            if(player == null || !PhotonNetwork.IsConnectedAndReady) {
                return -1;
            }

            if(PhotonNetwork.OfflineMode) {
                return 0;
            }

			if(player.CustomProperties.TryGetValue(PlayerNumbering.RoomPlayerIndexedProp, out object value)) {
				return (byte)value;
			}
			return -1;
        }

        public static void SetPlayerNumber(this Player player, int playerNumber) {
            if(player == null || PhotonNetwork.OfflineMode) {
                return;
            }

            if(playerNumber < 0) {
                Debug.LogWarning("Setting invalid playerNumber: " + playerNumber + " for: " + player.ToStringFull());
            }

            if(!PhotonNetwork.IsConnectedAndReady) {
                Debug.LogWarning("SetPlayerNumber was called in state: " + PhotonNetwork.NetworkClientState + ". Not IsConnectedAndReady.");
                return;
            }

            int current = player.GetPlayerNumber();
            if(current != playerNumber) {
                Debug.Log("PlayerNumbering: Set number " + playerNumber);
                player.SetCustomProperties(new Hashtable() { { PlayerNumbering.RoomPlayerIndexedProp, (byte)playerNumber } });
            }
        }
    }
}