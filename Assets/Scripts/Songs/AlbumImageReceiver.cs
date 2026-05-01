using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever
{

    public class AlbumImageReceiver : MonoBehaviour
    {

        #region Fields

        [EnumNamedArray(typeof(SongRegistry.SongList))]
        public GameObject[] thumbnailPrefabs = new GameObject[(int)SongRegistry.SongList.NOT_OPTION];

        #endregion

        #region Properties
        #endregion

        #region Unity Messages

        public void Start()
        {
            // subscribe to the event
            SingleSongSelectionEvents.INSTANCE.onLeaderboardChange += OnLeaderboardChange;

        }

        public void OnDestroy()
        {
            // unsubscribe
            SingleSongSelectionEvents.INSTANCE.onLeaderboardChange -= OnLeaderboardChange;
        }

        #endregion

        private void OnLeaderboardChange(SongRegistry.SongList index)
        {
            for (int i = 0; i < thumbnailPrefabs.Length; ++i)
            {
                thumbnailPrefabs[i].SetActive(false);
            }
            thumbnailPrefabs[(int)index].SetActive(true);
        }

    }

}