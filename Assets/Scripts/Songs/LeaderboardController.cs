using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace IdolFever
{
    // event controllers
    public class LeaderboardController : MonoBehaviour, IPointerEnterHandler
    {

        #region Fields

        [SerializeField] private SongRegistry.SongList index;

        #endregion

        #region Properties

        internal SongRegistry.SongList SongIndex
        {
            // no need get for this
            set { index = value; }
        }

        #endregion

        #region Unity Messages

        // summon items
        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log("On Pointer Enter Leaderboard Controller: " + index);
            SingleSongSelectionEvents.INSTANCE.LeaderboardChange(index);
        }

        #endregion

    }
}
