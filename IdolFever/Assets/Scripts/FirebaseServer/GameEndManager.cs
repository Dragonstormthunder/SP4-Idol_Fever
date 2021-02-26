using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IdolFever.Server
{
    public class GameEndManager : MonoBehaviour
    {

        #region

        public ServerDatabase serverDatabase;
        public TextMeshProUGUI gemText;

        #endregion

        #region Unity Messages

        private void Start()
        {

            // for debug accounts to generate the firebase scores
            //GameConfigurations.UploadToFirebase = true;
            //GameConfigurations.SongChosen = SongRegistry.SongList.WELLERMAN;
            //GameConfigurations.LastHighScore = 321432;

            // let the other components update first
            StartCoroutine(YieldWaitOneSecond());

        }

        #endregion

        private IEnumerator YieldWaitOneSecond()
        {
            yield return new WaitForSeconds(1);

            // that means a game was played
            // upload the relevant scores to firebase
            if (GameConfigurations.UploadToFirebase)
            {

                Debug.Log("Uploading to Firebase");

                // doesn't matter if it's a multiplayer or single player game
                // we want to upload to firebase

                // set the highscore key for the song chosen
                string songKey = "";
                switch (GameConfigurations.SongChosen)
                {
                    default:
                        // there is no song match
                        Debug.LogError("No song match");
                        GameConfigurations.UploadToFirebase = false;
                        yield break;

                    case SongRegistry.SongList.FUMO_SONG:
                        songKey = ServerDatabase.DBSH_ORIGINAL_SONG;
                        break;

                    case SongRegistry.SongList.MOUNTAIN_KING:
                        songKey = ServerDatabase.DBSH_MOUNTAIN_KING;
                        break;

                    case SongRegistry.SongList.WELLERMAN:
                        songKey = ServerDatabase.DBSH_WELLERMAN;
                        break;
                }

                Debug.Log("Score Just Played: " + GameConfigurations.SongChosen);

                _ = StartCoroutine(serverDatabase.GrabOwnHighScore(songKey, (databaseHighScore) =>
                {

                    Debug.Log("Grabbed own highscore: " + databaseHighScore.ToString() + " vs. New score: " + GameConfigurations.LastHighScore.ToString("#."));

                    // if highscore in the database smaller than the current high score
                    if (databaseHighScore < (int)GameConfigurations.LastHighScore)
                    {

                        Debug.Log("Saving new high score");

                        // save the data into the database
                        _ = StartCoroutine(serverDatabase.UpdateSongHighscore(songKey, (int)GameConfigurations.LastHighScore));

                    }

                }));

                // reward gems for completing a valid song
                _ = StartCoroutine(serverDatabase.GetGems((gems) =>
                {

                    Debug.Log("Gems Before: " + gems);

                    gems += 10;

                    Debug.Log("Gems After: " + gems);

                    _ = StartCoroutine(serverDatabase.UpdateGems(gems));

                    // just check if there's an UI available
                    if (gemText != null)
                    {
                        Debug.Log("inside: " + gems);

                        // update here beause the stat box may not retrieve the correct data
                        // since asynchronous it probably completed its gem pull already
                        gemText.text = gems.ToString();

                    }

                }));

                // reset to false
                // the results have been uploaded to firebase, don't want it to call again
                GameConfigurations.UploadToFirebase = false;
            }

        }


    }
}
