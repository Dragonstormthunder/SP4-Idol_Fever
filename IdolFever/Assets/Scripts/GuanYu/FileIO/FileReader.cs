using System.IO;
using UnityEngine;

namespace IdolFever {
    internal sealed class FileReader: MonoBehaviour {
        #region Fields

        private StreamReader streamReader;
        [SerializeField] private string filePath;

        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            using(StreamWriter w = File.AppendText(filePath)) {
            }
            streamReader = new StreamReader(filePath);
        }

        private void OnDisable() {
            streamReader.Close();
        }

        #endregion

        public FileReader() {
            streamReader = null;
            filePath = string.Empty;
        }

        public string ReadTextFromFile() {
            return streamReader.ReadToEnd();
        }
    }
}