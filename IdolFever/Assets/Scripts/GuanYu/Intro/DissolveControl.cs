using UnityEngine;
using UnityEngine.UI;

namespace DissolveControl {
    internal sealed class DissolveControl: MonoBehaviour {
        #region Fields

        private float stepThreshold;
        [SerializeField] private float spd;
        private Material dissolveMtl;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            dissolveMtl = GetComponent<Image>().material;
        }

	    private void Update() {
            stepThreshold -= spd * Time.deltaTime;

            if(stepThreshold < 0.0f) {
                stepThreshold = 0.0f;
            }

            dissolveMtl.SetFloat("_StepThreshold", stepThreshold);

            if(Mathf.Approximately(stepThreshold, 0.0f)) {
                gameObject.SetActive(false);
                dissolveMtl.SetFloat("_StepThreshold", 1.0f);
            }
        }

        #endregion

        public DissolveControl() {
            stepThreshold = 1.0f;
            spd = 0.0f;
            dissolveMtl = null;
        }
    }
}