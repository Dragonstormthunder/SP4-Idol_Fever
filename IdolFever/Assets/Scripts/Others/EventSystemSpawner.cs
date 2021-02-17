using UnityEngine;
using UnityEngine.EventSystems;

namespace IdolFever {
    internal sealed class EventSystemSpawner: MonoBehaviour {
		#region Fields

		[SerializeField] private GameObject parentGO;

		#endregion

		#region Properties
		#endregion

		#region Unity User Callback Event Funcs

		private void OnEnable() {
			EventSystem sceneEventSystem = FindObjectOfType<EventSystem>();

			if(sceneEventSystem == null) {
				GameObject eventSystem = new GameObject("EventSystem");

				eventSystem.AddComponent<EventSystem>();
				eventSystem.AddComponent<StandaloneInputModule>();

				eventSystem.transform.SetParent(parentGO.transform);
			}
		}

		#endregion

		public EventSystemSpawner() {
			parentGO = null;
		}
	}
}