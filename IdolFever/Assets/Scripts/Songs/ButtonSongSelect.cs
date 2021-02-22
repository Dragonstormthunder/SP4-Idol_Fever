using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever
{
    public class ButtonSongSelect : MonoBehaviour
    {

        #region 

        [SerializeField] SongRegistry.SongList index;

        #endregion

        #region Properties

        internal SongRegistry.SongList SongIndex
        {
            // no need for get
            set { index = value; }
        }

        #endregion

        public void OnClick()
        {
            GameConfigurations.SongChosen = index;
        }

    }
}