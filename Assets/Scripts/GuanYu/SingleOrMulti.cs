using UnityEngine;

namespace IdolFever {
    internal sealed class SingleOrMulti: MonoBehaviour {
        #region Fields

        [SerializeField] private AsyncSceneTransitionOutWithAlts script;

        #endregion

        #region Properties

        public static bool IsSingle {
            get;
            set;
        }

        public SingleOrMulti() {
            script = null;
        }

        #endregion

        static SingleOrMulti() {
            IsSingle = true;
        }

        #region Unity User Callback Event Funcs

        private void Awake() {
            script.Flag = IsSingle;
        }

        #endregion
    }
}