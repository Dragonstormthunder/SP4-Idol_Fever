using UnityEngine;

namespace IdolFever {
    internal sealed class PlayerBlock: MonoBehaviour {
        #region Fields

        private int actorNumber;
        private string name; 

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

        public string Name{
            get {
                return name;
            }
            set {
                name = value;
            }
        }

        #endregion

        public PlayerBlock() {
            actorNumber = -999;
            name = string.Empty;
        }

        #region Unity User Callback Event Funcs
        #endregion
    }
}