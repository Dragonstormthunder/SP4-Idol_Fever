using UnityEngine;

namespace IdolFever {
    internal sealed class OptionsButton: MonoBehaviour {
        #region Fields

        private float spd;
        [SerializeField] private float startSpd;
        [SerializeField] private float pressedSpd;

        #endregion

        #region Properties
        #endregion

        public OptionsButton() {
            spd = 0.0f;
            startSpd = 0.0f;
            pressedSpd = 0.0f;
        }

        #region Unity User Callback Event Funcs

        private void Awake() {
            spd = startSpd;
        }

        private void Update() {
            gameObject.transform.Rotate(0.0f, 0.0f, Time.deltaTime * spd);
        }

        #endregion

        public void OnClick() {
            spd = pressedSpd;
        }
    }
}