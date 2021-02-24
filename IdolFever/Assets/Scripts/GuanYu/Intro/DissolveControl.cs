using UnityEngine;
using UnityEngine.UI;

namespace DissolveControl {
    internal sealed class DissolveControl: MonoBehaviour {
        #region Fields

        private float stepThreshold;
        private Material dissolveMtl;
        [SerializeField] private float startStepThreshold;
        [SerializeField] private float vel;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            dissolveMtl = GetComponent<Image>().material;
            dissolveMtl.SetFloat("_StepThreshold", startStepThreshold);
            stepThreshold = startStepThreshold;
        }

	    private void Update() {
            stepThreshold += vel * Time.deltaTime;

            if(vel < 0.0f) {
                if(stepThreshold < 0.0f) {
                    stepThreshold = 0.0f;
                }
            } else {
                if(stepThreshold > 1.0f) {
                    stepThreshold = 1.0f;
                }
            }

            dissolveMtl.SetFloat("_StepThreshold", stepThreshold);

            if(Mathf.Approximately(stepThreshold, 0.0f)) {
                gameObject.SetActive(false);
            }
        }

        private void OnDisable() {
            dissolveMtl.SetFloat("_StepThreshold", 1.0f);
        }

        #endregion

        public DissolveControl() {
            stepThreshold = 0.0f;
            vel = 0.0f;
            dissolveMtl = null;
        }
    }
}