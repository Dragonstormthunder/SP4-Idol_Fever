using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever
{

    public class SingleSongSelectionEvents : MonoBehaviour
    {

        #region Fields

        public static SingleSongSelectionEvents INSTANCE;

        #endregion

        #region Unity Messages

        // singleton
        private void Awake()
        {
            INSTANCE = this;
        }

        #endregion

        #region Events

        #region Leaderboard Change Event

        internal event Action<SongRegistry.SongList> onLeaderboardChange;
        internal void LeaderboardChange(SongRegistry.SongList index)
        {
            // check if null
            onLeaderboardChange?.Invoke(index);
        }

        #endregion

        #region Zoom And Enhance Event

        internal event Action<SongRegistry.SongList> onZoomAndEnhance;
        internal void ZoomAndEnhance(SongRegistry.SongList index)
        {
            // check if null
            onZoomAndEnhance?.Invoke(index);
        }

        #endregion


        #endregion

    }
}
