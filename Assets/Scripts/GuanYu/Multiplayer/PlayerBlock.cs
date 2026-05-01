using UnityEngine;

namespace IdolFever {
    internal sealed class PlayerBlock: MonoBehaviour {
        #region Fields

        private int actorNumber;
        private string nickname; 

        #endregion

        #region Properties

        public int ActorNumber {
            get {
                return actorNumber;
            }
            set {
                actorNumber = value;
            }
        }

        public string Nickname{
            get {
                return nickname;
            }
            set {
                nickname = value;
            }
        }

        #endregion

        public PlayerBlock() {
            actorNumber = -999;
            nickname = string.Empty;
        }

        #region Unity User Callback Event Funcs
        #endregion
    }
}