using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever
{

    public class SongSelectHarcoded : MonoBehaviour
    {

        #region Fields

        [EnumNamedArray(typeof(SongRegistry.SongList))]
        public GameObject[] buttons = new GameObject[(int)SongRegistry.SongList.NOT_OPTION];

        public Vector3 bigButtonScale;
        public Vector3 smallButtonScale;
        public float buttonPadding;

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
            //for (int i = 0; i < buttons.Length; ++i)
            //{
            //    buttons[i].transform.localScale = new Vector3(smallButtonScale.x, smallButtonScale.y, smallButtonScale.z);
            //    buttons[i].transform.localPosition = new Vector3(0, 0, 0);
            //}

            buttons[(int)index].transform.localScale = new Vector3(bigButtonScale.x, bigButtonScale.y, bigButtonScale.z);
            buttons[(int)index].transform.localPosition = new Vector3(0, 0, 0);
            buttons[(int)index].transform.SetAsLastSibling();

            // shameless hardcoding
            switch (index)
            {
                default:
                    break;

                case SongRegistry.SongList.FUMO_SONG:

                    // top button
                    buttons[(int)SongRegistry.SongList.WELLERMAN].transform.localScale = new Vector3(smallButtonScale.x, smallButtonScale.y, smallButtonScale.z);
                    buttons[(int)SongRegistry.SongList.WELLERMAN].transform.localPosition = new Vector3(0, buttonPadding, 0);

                    // bottom button
                    buttons[(int)SongRegistry.SongList.MOUNTAIN_KING].transform.localScale = new Vector3(smallButtonScale.x, smallButtonScale.y, smallButtonScale.z);
                    buttons[(int)SongRegistry.SongList.MOUNTAIN_KING].transform.localPosition = new Vector3(0, -buttonPadding, 0);

                    break;

                case SongRegistry.SongList.MOUNTAIN_KING:

                    // top button
                    buttons[(int)SongRegistry.SongList.FUMO_SONG].transform.localScale = new Vector3(smallButtonScale.x, smallButtonScale.y, smallButtonScale.z);
                    buttons[(int)SongRegistry.SongList.FUMO_SONG].transform.localPosition = new Vector3(0, buttonPadding, 0);

                    // bottom button
                    buttons[(int)SongRegistry.SongList.WELLERMAN].transform.localScale = new Vector3(smallButtonScale.x, smallButtonScale.y, smallButtonScale.z);
                    buttons[(int)SongRegistry.SongList.WELLERMAN].transform.localPosition = new Vector3(0, -buttonPadding, 0);

                    break;

                case SongRegistry.SongList.WELLERMAN:

                    // top button
                    buttons[(int)SongRegistry.SongList.MOUNTAIN_KING].transform.localScale = new Vector3(smallButtonScale.x, smallButtonScale.y, smallButtonScale.z);
                    buttons[(int)SongRegistry.SongList.MOUNTAIN_KING].transform.localPosition = new Vector3(0, buttonPadding, 0);

                    // bottom button
                    buttons[(int)SongRegistry.SongList.FUMO_SONG].transform.localScale = new Vector3(smallButtonScale.x, smallButtonScale.y, smallButtonScale.z);
                    buttons[(int)SongRegistry.SongList.FUMO_SONG].transform.localPosition = new Vector3(0, -buttonPadding, 0);

                    break;

            }

        }

    }

}
