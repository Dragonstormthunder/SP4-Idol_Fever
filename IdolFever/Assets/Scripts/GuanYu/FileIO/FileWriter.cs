using System.IO;
using UnityEditor;
using UnityEngine;

namespace IdolFever {
    internal sealed class FileWriter: MonoBehaviour {
        #region Fields

        private StreamWriter streamWriter;
        [SerializeField] private string filePath;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            streamWriter = new StreamWriter(filePath, true);
            WriteTextToFile("Test");
        }

        private void OnDisable() {
            streamWriter.Close();
        }

        #endregion

        public FileWriter() {
            streamWriter = null;
            filePath = string.Empty;
        }

        public void WriteTextToFile(string text) {
            streamWriter.WriteLine(text);
        }
    }
}