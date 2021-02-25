using UnityEngine;

namespace IdolFever {
    internal sealed class SkyMtlControl: MonoBehaviour {
        #region Fields

        [SerializeField] private float rotationVel;
        [SerializeField] private Material skyMtl;

        #endregion

        #region Properties
        #endregion

        public SkyMtlControl() {
            rotationVel = 0.0f;
            skyMtl = null;
        }

        #region Unity User Callback Event Funcs

        private void FixedUpdate() {
            skyMtl.SetFloat("_Rotation", Time.time * rotationVel);
        }

        private void OnDisable() {
            skyMtl.SetFloat("_Rotation", 0.0f);
        }

        #endregion
    }
}