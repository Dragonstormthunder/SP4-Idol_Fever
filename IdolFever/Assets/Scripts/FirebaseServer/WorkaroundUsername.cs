using UnityEngine;

namespace IdolFever.Server {
	internal sealed class WorkaroundUsername: MonoBehaviour {
		#region Fields

		public ServerDatabase serverDatabaseScript;

		#endregion

		#region Properties
		#endregion

		#region Unity User Callback Event Funcs

		private void Start() {
			_ = StartCoroutine(serverDatabaseScript.GetUsername((playerName) => {
				GameConfigurations.Username = playerName;
				Debug.Log("Username:" + GameConfigurations.Username);
			}));
		}

		#endregion
	}
}