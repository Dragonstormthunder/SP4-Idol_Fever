using IdolFever.Character;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;
using UnityEngine;

namespace IdolFever {
    internal sealed class ListOfPlayers: MonoBehaviourPunCallbacks {
        #region Fields

        [SerializeField] private CharacterDecentralizeData charDecentralizedData;
        [SerializeField] private GameObject[] playerBlocks;

        #endregion

        #region Properties

        internal bool IsStageIndexSet {
            get;
            set;
        }

        #endregion

        internal ListOfPlayers() {
            IsStageIndexSet = false;
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

        public override void OnPlayerEnteredRoom(Player newPlayer) {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount, this);

            UpdatePlayerBlocks();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer) {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount, this);

            UpdatePlayerBlocks();
        }

        #endregion

        private void UpdatePlayerBlocks() {
            int amtOfPlayerBlocks = playerBlocks.Length;
            Player[] players = PhotonNetwork.PlayerList;
            int index = 0;

            for(int i = 0; i < amtOfPlayerBlocks; ++i){
                GameObject playerBlockGO = playerBlocks[i];

                PlayerBlock playerBlockScript = playerBlockGO.GetComponent<PlayerBlock>();

                if(i < PhotonNetwork.CurrentRoom.PlayerCount) {
                    if(i == 0) {
                        playerBlockScript.ActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
                        playerBlockScript.Nickname = PhotonNetwork.LocalPlayer.NickName;
                    } else {
                        Player myPlayer = players[index];

                        if(myPlayer == PhotonNetwork.LocalPlayer) {
                            myPlayer = players[++index];
                        }

                        playerBlockScript.ActorNumber = myPlayer.ActorNumber;
                        playerBlockScript.Nickname = myPlayer.NickName;
                    }

                    GameObject charThumbnailIcon = playerBlockGO.transform.Find("CharacterThumbnailIcon").gameObject;
                    charThumbnailIcon.SetActive(true);

                    GameObject mask = charThumbnailIcon.transform.Find("CircleMask").gameObject;
                    _ = Instantiate(charDecentralizedData.AccessThumbnailPrefab(GameConfigurations.CharacterIndex), mask.transform); //Instantiate thumbnail
                } else {
                    playerBlockScript.ActorNumber = -999;
                    playerBlockScript.Nickname = string.Empty;

                    //clear?????
                }

                TextMeshProUGUI tmpComponent = playerBlockGO.transform.Find("PlayerBlockText").GetComponent<TextMeshProUGUI>();
                tmpComponent.text = playerBlockScript.Nickname;
            }
        }

        public void OnStartButtonClick() {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            PhotonView.Get(this).RPC("SetStage", RpcTarget.All, Random.Range(0, 1));

            _ = StartCoroutine(nameof(MyFunc));
        }

        private IEnumerator MyFunc() {
            while(!IsStageIndexSet) {
                yield return null;
            }

            IsStageIndexSet = false;

            PhotonView.Get(this).RPC("ToGameplay", RpcTarget.All, null);
        }
    }
}