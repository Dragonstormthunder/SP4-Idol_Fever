using UnityEngine;

namespace IdolFever {
    internal sealed class SelectionControl: MonoBehaviour {
        #region Fields

        [SerializeField] private GameObject[] myOptions;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

	    private void Update() {
            Vector3 myLocalPos = ((RectTransform)myOptions[(int)Options.GraphicsOption].transform).localPosition;
            ((RectTransform)transform).localPosition = new Vector3(myLocalPos.x, myLocalPos.y, 0.0f);
	    }

        #endregion

        public SelectionControl() {
            myOptions = System.Array.Empty<GameObject>();
        }

        public void OnClickLowButton() {
            Options.ChangeToLowGraphics();
        }

        public void OnClickHighButton() {
            Options.ChangeToHighGraphics();
        }
    }
}