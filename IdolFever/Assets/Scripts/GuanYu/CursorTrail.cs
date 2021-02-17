using UnityEngine;

namespace IdolFever {
    internal sealed class CursorTrail: MonoBehaviour {
        #region Fields

        private Transform trailTransform;
        private Camera camComponent;

        [SerializeField] private Color trailColor;
        [SerializeField] private float distFromCam;
        [SerializeField] private float startWidth;
        [SerializeField] private float endWidth;
        [SerializeField] private float trailTime;
        [SerializeField] private GameObject parent;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            if(Application.platform == RuntimePlatform.Android) {
                enabled = false;
            }
        }

        private void Start() {
            camComponent = GetComponent<Camera>();

            GameObject trailGO = new GameObject("CursorTrail");
            trailTransform = trailGO.transform;
            trailTransform.SetParent(parent.transform);

            TrailRenderer trail = trailGO.AddComponent<TrailRenderer>();

            trail.time = -1.0f;
            MoveTrailToCursor(Input.mousePosition);
            trail.time = trailTime;

            trail.startWidth = startWidth;
            trail.endWidth = endWidth;

            trail.numCapVertices = 2;
			trail.sharedMaterial = new Material(Shader.Find("Unlit/Color")) {
				color = trailColor
			};
		}

        private void Update() {
            MoveTrailToCursor(Input.mousePosition);
        }

        private void MoveTrailToCursor(Vector3 screenPosition) {
            trailTransform.position = camComponent.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distFromCam));
        }

        #endregion

        public CursorTrail() {
            camComponent = null;
            trailColor = new Color();
            distFromCam = 0.0f;
            startWidth = 0.7f;
            endWidth = 0.4f;
            trailTime = 0.24f;
            parent = null;
        }
    }
}