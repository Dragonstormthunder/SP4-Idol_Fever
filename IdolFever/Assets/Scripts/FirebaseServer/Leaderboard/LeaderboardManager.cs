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
        private SongRegistry.SongList currentSelectedSong;
        //[SerializeField] SongRegistry.SongList songLeaderboardToDisplay;

        public TextMeshProUGUI[] namesText;
        public TextMeshProUGUI[] scoresText;

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
                currentSelectedSong = index;
                personalScore.text = ownscore.ToString();

                Debug.Log("Clearing Leaderboard");

                // clear the leaderboard to ready it for new scores
                ClearLeaderBoard();

                StartCoroutine(serverDatabase.GrabAllScoresOfASong(songName, (scores) =>
                {
                    int position = 0;
                    if (currentSelectedSong == index)
                        foreach (KeyValuePair<string, int> score in scores)
                        {
                            // would instiantiate here for vertical layout
                            Debug.Log(score.Key + " " + score.Value);
                            //GameObject scoreElement = Instantiate(scorePrefab, contentPanel.transform);
                            //scoreElement.GetComponent<ScoreData>().SetScoreData(++position, score.Key, score.Value);

                            // for the hardcoded UI

                            namesText[position].text = score.Key;
                            scoresText[position].text = score.Value.ToString();

                            ++position;

                            if (position > 2)
                                break;

                        }
                }));

            }));

        }

        public void ClearLeaderBoard()
        {
            //foreach (Transform child in contentPanel.transform)
            //{
            // destroy it
            //    Destroy(child.gameObject);
            //}

            for (int i = 0; i < namesText.Length; ++i)
            {
                namesText[i].text = "";
            }

            for (int i = 0; i < scoresText.Length; ++i)
            {
                scoresText[i].text = "";
            }

        }


    }
}
