using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using IdolFever.Server;

namespace IdolFever
{
    public class SingleSelectSongManager : MonoBehaviour
    {

        #region Fields

        public GameObject contentPanel;
        //public ServerDatabase serverDatabase;
        public GameObject songPrefab;

        #endregion

        private void Start()
        {
            string name = "";
            int rating = 1;

            // use the song registry to create all these items
            for (SongRegistry.SongList index = 0; index < SongRegistry.SongList.NOT_OPTION; ++index)
            {

                // the song prefab
                GameObject song = Instantiate(songPrefab, contentPanel.transform);

                // initialize the song data
                SongData songData = song.GetComponent<SongData>();
                SongDataFactory.GenerateSongData(index, ref name, ref rating);
                //Debug.Log("Song: " + name + " " + rating);
                songData.SetSongData(name, rating);

                // initialize the leaderboard controller
                LeaderboardController leaderboardController = song.GetComponent<LeaderboardController>();
                leaderboardController.SongIndex = index;

                // initialize the zoom and enhance
                UI.SongSelectionZoomAndEnhance songSelectionZoomAndEnhance = song.GetComponent<UI.SongSelectionZoomAndEnhance>();
                songSelectionZoomAndEnhance.SongIndex = index;

            }


        }

    }
}