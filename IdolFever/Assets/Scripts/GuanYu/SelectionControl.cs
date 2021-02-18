using UnityEngine;

namespace IdolFever {
    internal sealed class SelectionControl: MonoBehaviour {

        [System.Serializable]
        public struct PosXY {
            public float x;
            public float y;
        }

        #region Fields

        [SerializeField] private GraphicsQualityOptions.GraphicsQualityOption currOption;
        [SerializeField] private PosXY[] pos;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

	    private void Update() {
            PosXY posXY = pos[(int)currOption];
            ((RectTransform)transform).localPosition = new Vector3(posXY.x, posXY.y , 0.0f);
	    }

        #endregion

        public SelectionControl() {
            currOption = GraphicsQualityOptions.GraphicsQualityOption.NotAnOption;
            pos = System.Array.Empty<PosXY>();
        }

        public void ChangeToLow() {
            currOption = GraphicsQualityOptions.GraphicsQualityOption.Low;
        }

        public void ChangeToHigh() {
            currOption = GraphicsQualityOptions.GraphicsQualityOption.High;
        }
    }
}