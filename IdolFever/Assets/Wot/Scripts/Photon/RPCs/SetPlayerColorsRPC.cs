using Photon.Pun;
using UnityEngine;

namespace IdolFever {
    internal sealed class SetPlayerColorsRPC: MonoBehaviour {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs
        #endregion

        [PunRPC] public void SetPlayerColors(Vector3[] vecs) {
            int arrLen = vecs.Length;
            PlayerUniversal.Colors = new Color[arrLen];

            for(int i = 0; i < arrLen; ++i) {
                Vector3 vec = vecs[i];
                PlayerUniversal.Colors[i] = new Color(vec.x, vec.y, vec.z, 1.0f);
            }
        }
    }
}