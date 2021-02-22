using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IdolFever.Server.Leaderboard
{
    public class LeaderboardManager : MonoBehaviour
    {
        #region Field

        public ServerDatabase serverDatabase;
        public GameObject contentPanel;
        public GameObject scorePrefab;
        public TextMeshProUGUI personalScore;
        //[SerializeField] SongRegistry.SongList songLeaderboardToDisplay;

        #endregion

        #region Properties

        //internal SongRegistry.SongList LeaderBoardToDisplay
        //{
        //    //get { return songLeaderboardToDisplay; }
        //    set
        //    {
        //        songLeaderboardToDisplay = value;
        //    }
        //}

        #endregion

        #region Unity Functions

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

            //Debug.Log("LeaderboardManager leaderboard change event triggered: " + index.ToString());

            string songName = "";

            switch (index)
            {
                // if it doesn't exist
                default:
                    return; // kill it here

                case SongRegistry.SongList.MOUNTAIN_KING:
                    songName = ServerDatabase.DBSH_MOUNTAIN_KING;
                    break;

                case SongRegistry.SongList.FUMO_SONG:
                    songName = ServerDatabase.DBSH_ORIGINAL_SONG;
                    break;

                case SongRegistry.SongList.WELLERMAN:
                    songName = ServerDatabase.DBSH_WELLERMAN;
                    break;
            }

            StartCoroutine(serverDatabase.GrabOwnHighScore(songName, (ownscore) =>
            {
                personalScore.text = ownscore.ToString();

                Debug.Log("Clearing Leaderboard");

                // clear the leaderboard to ready it for new scores
                ClearLeaderBoard();

                StartCoroutine(serverDatabase.GrabAllScoresOfASong(songName, (scores) =>
                {
                    int position = 0;
                    foreach (KeyValuePair<string, int> score in scores)
                    {
                        //Debug.Log(score.Key + " " + score.Value);
                        GameObject scoreElement = Instantiate(scorePrefab, contentPanel.transform);
                        scoreElement.GetComponent<ScoreData>().SetScoreData(++position, score.Key, score.Value);
                    }
                }));

            }));

        }

        public void ClearLeaderBoard()
        {
            foreach (Transform child in contentPanel.transform)
            {
                // destroy it
                Destroy(child.gameObject);
            }
        }


    }
}
