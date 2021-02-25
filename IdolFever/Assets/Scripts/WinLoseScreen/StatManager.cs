using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IdolFever
{
    // manages the stats from the game
    public class StatManager : MonoBehaviour
    {
        #region Field

        [Header("My Stuff")]
        public TextMeshProUGUI myName;
        public TextMeshProUGUI myScore;
        public TextMeshProUGUI myPosition;

        [Header("Opponent's Stuff")]
        public TextMeshProUGUI opponentName;
        public TextMeshProUGUI opponentScore;
        public TextMeshProUGUI opponentPosition;

        #endregion

        #region Properties
        #endregion

        #region Unity Messages

        private void Start()
        {
            //GameConfigurations.Username = "test";
            //GameConfigurations.LastHighScore = 13200;

            ////GameConfigurations.WasThereOpponent = true;
            //GameConfigurations.OpponentHighScore = 1200;
            //GameConfigurations.OpponentUsername = "reee";

            // init
            myName.text = GameConfigurations.Username;
            myScore.text = "Score: " + GameConfigurations.LastHighScore.ToString();

            // if there is an opponent then we need to set opponent values
            // and positions
            if (GameConfigurations.WasThereOpponent)
            {
                opponentName.text = GameConfigurations.OpponentUsername;
                opponentScore.text = "Score: " + GameConfigurations.OpponentHighScore.ToString();

                // i win
                if (GameConfigurations.LastHighScore > GameConfigurations.OpponentHighScore)
                {
                    myPosition.text = "1st";
                    myPosition.fontSize = 250;
                    myPosition.color = new Color(255, 192, 0);

                    opponentPosition.text = "2nd";
                    opponentPosition.fontSize = 150;
                    opponentPosition.color = new Color(166, 166, 166);
                }
                // i lost
                else if (GameConfigurations.LastHighScore < GameConfigurations.OpponentHighScore)
                {
                    opponentPosition.text = "1st";
                    opponentPosition.fontSize = 250;
                    opponentPosition.color = new Color(255, 192, 0);

                    myPosition.text = "2nd";
                    myPosition.fontSize = 150;
                    myPosition.color = new Color(166, 166, 166);
                }
                // draw
                else
                {
                    myPosition.text = "2nd";
                    myPosition.fontSize = 150;
                    myPosition.color = new Color(166, 166, 166);

                    opponentPosition.text = "2nd";
                    opponentPosition.fontSize = 150;
                    opponentPosition.color = new Color(166, 166, 166);
                }
            }
            else
            {
                // no opponent
                myPosition.text = "";

                opponentName.text = "";
                opponentScore.text = "";
                opponentPosition.text = "";
            }

        }

        #endregion

    }
}