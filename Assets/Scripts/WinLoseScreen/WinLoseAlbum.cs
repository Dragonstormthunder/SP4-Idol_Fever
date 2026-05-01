using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IdolFever.WinLoseScreen
{

    // make the album have the correct picture
    public class WinLoseAlbum : MonoBehaviour
    {

        #region Fields
        [EnumNamedArray(typeof(SongRegistry.SongList))]
        public Sprite[] sprites = new Sprite[(int)SongRegistry.SongList.NOT_OPTION];

        // image item
        [SerializeField] private Image image;

        #endregion

        #region Properties
        #endregion

        #region Unity Messages

        private void Start()
        {
            image.sprite = sprites[(int)GameConfigurations.SongChosen];
        }

        #endregion

    }

}
