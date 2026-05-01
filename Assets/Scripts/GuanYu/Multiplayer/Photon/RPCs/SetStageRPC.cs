using IdolFever.Game;
using Photon.Pun;
using UnityEngine;

namespace IdolFever {
    internal sealed class SetStageRPC: MonoBehaviour {
        #region Fields

        [SerializeField] private ListOfPlayers script;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        public SetStageRPC() {
            script = null;
        }

        [PunRPC] public void SetStage(int stageIndex) {
            BeatmapPlayer.StageIndex = stageIndex;

            if(PhotonNetwork.IsMasterClient) {
                script.IsStageIndexSet = true;
            }
        }
    }
}