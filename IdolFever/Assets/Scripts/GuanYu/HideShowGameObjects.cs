using System.Linq;
using UnityEngine;

namespace IdolFever {
    internal sealed class HideShowGameObjects: MonoBehaviour {
        #region Fields

        [SerializeField] private string[] namesOfInterest;

        #endregion

        #region Properties
        #endregion

        public HideShowGameObjects() {
            namesOfInterest = System.Array.Empty<string>();
        }

        #region Unity User Callback Event Funcs
        #endregion

        public void HideGOs() {
            foreach(string name in namesOfInterest) {
                var GOsOfInterest = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == name);
                foreach(GameObject GO in GOsOfInterest) {
                    GO.SetActive(false);
                }
            }
        }

        public void ShowGOs() {
            foreach(string name in namesOfInterest) {
                var GOsOfInterest = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == name);
                foreach(GameObject GO in GOsOfInterest) {
                    GO.SetActive(true);
                }
            }
        }
    }
}