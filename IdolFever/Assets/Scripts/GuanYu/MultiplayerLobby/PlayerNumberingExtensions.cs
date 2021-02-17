using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace IdolFever {
    internal static class PlayerNumberingExtensions {
        #region Fields
        #endregion

        #region Properties
        #endregion

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
                player.SetCustomProperties(new Hashtable() { { PlayerNumbering.RoomPlayerIndexedProp, (byte)playerNumber } });
            }
        }
    }
}