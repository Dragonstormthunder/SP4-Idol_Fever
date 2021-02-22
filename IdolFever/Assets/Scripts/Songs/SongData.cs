using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IdolFever
{
    public class SongData : MonoBehaviour
    {
        #region Fields

        public TextMeshProUGUI textSongName;
        public GameObject ratingContent;

        [SerializeField] private List<GameObject> ratings;
        [SerializeField] private int rating;

        #endregion

        #region Properties
        #endregion

        #region Unity Messages

        private void Start()
        {
            // grab all the children
            foreach (Transform child in ratingContent.transform)
            {
                ratings.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }

            // clamp it to ensure no outside values
            // clamp using .Count, if we increase the items in the prefab this will
            // dynamically increase
            rating = Mathf.Clamp(rating, 1, ratings.Count);

            // activate those needed
            for (int i = 0; i < rating; ++i)
            {
                ratings[i].SetActive(true);
            }

        }

        #endregion

        public void SetSongData(string _textSongName, int _rating)
        {
            // set the song name
            textSongName.text = _textSongName;

            // settle the rating here

            rating = _rating;

            // can't do this here because start hasn't been run
            //// clamp it to ensure no outside values
            //_rating = Mathf.Clamp(_rating, 1, ratings.Count);
            //for (int i = 0; i < _rating; ++i)
            //{
            //    ratings[i].SetActive(true);
            //}

        }

    }
}
