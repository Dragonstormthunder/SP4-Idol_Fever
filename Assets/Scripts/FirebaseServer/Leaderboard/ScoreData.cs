using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IdolFever.Server.Leaderboard
{
    // stores the score data
    public class ScoreData : MonoBehaviour
    {

        #region Fields

        public TextMeshProUGUI positionText;
        public TextMeshProUGUI usernameText;
        public TextMeshProUGUI scoreText;

        #endregion

        #region SetScoreDataOverloads
        public void SetScoreData(string _position, string _usernameText, string _scoreText)
        {
            positionText.text = _position;
            usernameText.text = _usernameText;
            scoreText.text = _scoreText;
        }

        public void SetScoreData(int _position, string _usernameText, string _scoreText)
        {
            positionText.text = _position.ToString();
            usernameText.text = _usernameText;
            scoreText.text = _scoreText;
        }

        public void SetScoreData(int _position, string _usernameText, int _scoreText)
        {
            positionText.text = _position.ToString();
            usernameText.text = _usernameText;
            scoreText.text = _scoreText.ToString();
        }
        #endregion

    }
}
