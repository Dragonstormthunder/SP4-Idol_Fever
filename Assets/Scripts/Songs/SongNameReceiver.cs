using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IdolFever
{
    public class SongNameReceiver : MonoBehaviour
    {

        #region Fields
        [SerializeField] private TextMeshProUGUI text;
        #endregion

        #region Properties
        #endregion

        #region Unity Messages

        public void Start()
        {
            // subscribe to the event
            SingleSongSelectionEvents.INSTANCE.onLeaderboardChange += OnLeaderboardChange;

            text = GetComponent<TextMeshProUGUI>();

        }

        public void OnDestroy()
        {
            // unsubscribe
            SingleSongSelectionEvents.INSTANCE.onLeaderboardChange -= OnLeaderboardChange;
        }

        #endregion

        private void OnLeaderboardChange(SongRegistry.SongList index)
        {

            switch(index)
            {
                default:
                    text.text = "";
                    break;

                case SongRegistry.SongList.FUMO_SONG:
                    text.text = "Fumo Song";
                    break;

                case SongRegistry.SongList.MOUNTAIN_KING:

                    text.text = "Hall of the Mountain King";
                    break;

                case SongRegistry.SongList.WELLERMAN:

                    text.text = "Wellerman";
                    break;

            }

        }


    }
}
