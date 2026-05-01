using TMPro;
using UnityEngine;

namespace IdolFever {
    internal sealed class SongSelectedText: MonoBehaviour {
        #region Fields

        [SerializeField] private TextMeshProUGUI tmpComponent;

        #endregion

        #region Properties
        #endregion

        public SongSelectedText() {
            tmpComponent = null;
        }

        #region Unity User Callback Event Funcs

        private void Awake() {
            tmpComponent.text = "Song selected:\n" + GameConfigurations.SongChosen;
        }

        #endregion
    }
}