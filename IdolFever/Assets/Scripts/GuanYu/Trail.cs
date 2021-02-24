using UnityEngine;

namespace IdolFever {
    internal sealed class Trail: MonoBehaviour {
        #region Fields

        [SerializeField] private float distFromCam;
        [SerializeField] private Camera camComponent;
        [SerializeField] private GameObject parent;
        [SerializeField] private GameObject particleSystemPrefab;
        [SerializeField] private Material[] mtls;

        #endregion

        #region Properties
        #endregion

        public Trail() {
            distFromCam = 0.0f;
            camComponent = null;
            parent = null;
            particleSystemPrefab = null;
            mtls = System.Array.Empty<Material>();
        }

        #region Unity User Callback Event Funcs

        private void Start() {
            /*ParticleSystem particleSystemComponentParent = parent.GetComponent<ParticleSystem>();

            foreach(Material mtl in mtls) {
                GameObject particleSystemGO = Instantiate(particleSystemPrefab);
                particleSystemGO.transform.SetParent(parent.transform);

                ParticleSystem particleSystemComponent = particleSystemGO.GetComponent<ParticleSystem>();
                particleSystemComponent.GetComponent<Renderer>().material = mtl;
                particleSystemComponentParent.subEmitters.AddSubEmitter(particleSystemComponent, ParticleSystemSubEmitterType.Birth, ParticleSystemSubEmitterProperties.InheritEverything);
            }*/
        }

        private void Update() {
            MoveTrailToCursor(Input.mousePosition);
        }

        #endregion

        private void MoveTrailToCursor(Vector3 screenPosition) {
            transform.position = camComponent.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distFromCam));
        }
    }
}