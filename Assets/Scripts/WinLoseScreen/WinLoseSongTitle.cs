using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IdolFever
{
    // make the album have the correct song title beneath it
    public class WinLoseSongTitle : MonoBehaviour
    {
        #region Fields
        public TextMeshProUGUI text; // the text to write the song in
        #endregion

        #region Properties
        #endregion

        #region Unity Messages

        private void Start()
        {
            text.text = SongRegistry.GetSongName(GameConfigurations.SongChosen);
        }

        #endregion

    }
}
