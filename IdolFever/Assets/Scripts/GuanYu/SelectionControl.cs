using System.Collections.Generic;
using UnityEngine;

namespace IdolFever {
    internal sealed class SelectionControl: MonoBehaviour {

        [System.Serializable]
        public struct PosXY { //??
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
	    
	    }

        #endregion

        public SelectionControl() {
            currOption = GraphicsQualityOptions.GraphicsQualityOption.NotAnOption;
            pos = System.Array.Empty<PosXY>();
        }
    }
}