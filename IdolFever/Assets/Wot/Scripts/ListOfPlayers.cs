using IdolFever.Server;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace IdolFever {
    internal sealed class ListOfPlayers: MonoBehaviourPunCallbacks {
        #region Fields

        [SerializeField] private GameObject[] playerBlocks;

        #endregion

        #region Properties
        #endregion

        public ListOfPlayers() {
            playerBlocks = System.Array.Empty<GameObject>();
        }

        #region Unity User Callback Event Funcs

        private void Start() {
            if(!PhotonNetwork.IsConnected) {
                Debug.LogWarning("Photon is not connected.", this);
                return;
            }

            UpdatePlayerBlocks();
        }

        #endregion

        #region Pun Callback Funcs
        #endregion

        private void UpdatePlayerBlocks() {
            int index = 1;
            foreach(Player player in PhotonNetwork.PlayerList) {
                GameObject playerBlockGO = playerBlocks[player == PhotonNetwork.LocalPlayer ? 0 : index];

                PlayerBlock playerBlockScript = playerBlockGO.GetComponent<PlayerBlock>();
                playerBlockScript.ActorNumber = player.ActorNumber;
                playerBlockScript.Nickname = player.NickName;

                TextMeshProUGUI tmpComponent = playerBlockGO.transform.Find("PlayerBlockText").GetComponent<TextMeshProUGUI>();
                tmpComponent.text = playerBlockScript.Nickname;

                if(player != PhotonNetwork.LocalPlayer) {
                    ++index;
                }
            }

            /*playerListEntry.SetPlayerListEntryColors();

            playerListEntry.SetPlayerReady(false);
            Hashtable props = new Hashtable() { { "IsPlayerReady", false } };
            p.SetCustomProperties(props);*/
        }
    }
}