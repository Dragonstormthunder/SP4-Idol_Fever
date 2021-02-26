using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever.Server
{
    public class GameEndManager : MonoBehaviour
    {

        #region

        public ServerDatabase serverDatabase;

        #endregion

        #region Unity Messages

        private void Start()
        {

            GameConfigurations.UploadToFirebase = true;
            GameConfigurations.SongChosen = SongRegistry.SongList.MOUNTAIN_KING;
            GameConfigurations.LastHighScore = 12345;

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
                        return; // kill the function here

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

                // reset to false
                // the results have been uploaded to firebase, don't want it to call again
                GameConfigurations.UploadToFirebase = false;
            }
        }

        #endregion


    }
}
