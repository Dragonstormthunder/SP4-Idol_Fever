using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace IdolFever.UI
{
    public class SongSelectionZoomAndEnhance : MonoBehaviour, /*IDeselectHandler,*/ IPointerEnterHandler/*, IPointerExitHandler*/
    {

        #region Fields

        [SerializeField] RectTransform rectTransform;
        public GameObject selection;
        [SerializeField] SongRegistry.SongList index;   // index of the song

        #endregion

        #region Properties

        internal SongRegistry.SongList SongIndex
        {
            // no need get
            set { index = value; }
        }

        #endregion

        #region Unity Messages

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();

            // subscribe to the event
            SingleSongSelectionEvents.INSTANCE.onZoomAndEnhance += ZoomAndEnhance;

        }

        public void OnDestroy()
        {
            // unsubscribe to the event
            SingleSongSelectionEvents.INSTANCE.onZoomAndEnhance -= ZoomAndEnhance;
        }

        //// make the popup smaller when it has been 
        //public void OnDeselect(BaseEventData eventData)
        //{
        //    // check whether the mouse pointer is inside of the panel
        //    if (!mouseInside)
        //        gameObject.SetActive(false);
        //}

        public void OnPointerEnter(PointerEventData eventData)
        {
            SingleSongSelectionEvents.INSTANCE.ZoomAndEnhance(index);

            //rectTransform.sizeDelta = new Vector2(900, 150);
            //border.transform.SetAsFirstSibling();
            //border.enabled = true;
            //selection.SetActive(true);
        }

        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    rectTransform.sizeDelta = new Vector2(800, 150);
        //    //border.enabled = false;
        //    //selection.SetActive(false);
        //}

        #endregion

        #region Song Event Messages

        private void ZoomAndEnhance(SongRegistry.SongList _index)
        {
            if (index == _index)
            {
                // zoom in since it's the one who is selected
                rectTransform.sizeDelta = new Vector2(900, 150);
            }
            else
            {
                // zoom out since it's been deselected
                rectTransform.sizeDelta = new Vector2(800, 150);
            }
        }

        #endregion

    }
}