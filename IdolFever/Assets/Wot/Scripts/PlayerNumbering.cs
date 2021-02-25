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
			OnPlayerNumberingChanged?.Invoke();
		}
    }
}