using UnityEngine;

namespace IdolFever {
    internal sealed class SetIsSingle: MonoBehaviour {
        #region Fields

        [SerializeField] private bool val;

        #endregion

        #region Properties
        #endregion

        public SetIsSingle() {
            val = false;
        }

        #region Unity User Callback Event Funcs
        #endregion

        public void OnClick() {
            SingleOrMulti.IsSingle = val;
        }
    }
}