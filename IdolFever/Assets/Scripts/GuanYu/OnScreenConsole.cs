using UnityEngine;

namespace IdolFever {
    internal sealed class OnScreenConsole: MonoBehaviour {
        #region Fields

        private string myLog;
        private string fileName;
        private bool doShow;
        private int kChars;

        [SerializeField] private bool dontDestroyOnLoad;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            if(dontDestroyOnLoad) {
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnEnable() {
            Application.logMessageReceived += Log;
        }
        private void OnDisable() {
            Application.logMessageReceived -= Log;
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.RightShift)) {
                doShow = !doShow;
            }
        }

        private void OnGUI() {
            if(!doShow) {
                return;
            }
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
               new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));
            GUI.TextArea(new Rect(10, 10, 540, 370), myLog);
        }

        #endregion

        public OnScreenConsole() {
            myLog = "";
            fileName = "";
            doShow = false;
            kChars = 700;

            dontDestroyOnLoad = true;
        }

        private void Log(string logStr, string stackTrace, LogType type) {
            myLog += logStr + '\n';
            if(myLog.Length > kChars) {
                myLog = myLog.Substring(myLog.Length - kChars);
            }

            if(fileName == "") {
                string d = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/YOUR_LOGS";
                System.IO.Directory.CreateDirectory(d);
                string r = Random.Range(1000, 9999).ToString();
                fileName = d + "/log-" + r + ".txt";
            }
            try {
                System.IO.File.AppendAllText(fileName, logStr + "\n");
            } catch {
            }
        }
    }
}