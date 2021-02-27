using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using IdolFever.Game;

namespace IdolFever.UI
{

    public class ReleaseButton : MonoBehaviour
    {
        public int id;
        public BeatmapPlayer beatmapPlayer;
        void Update()
        {
            if (Input.touchCount > 0)
            {
                for (int t = 0; t < Input.touchCount; ++t)
                {
                    if (RectTransformUtility.PixelAdjustRect(GetComponent<RectTransform>(), GetComponentInParent<Canvas>()).Contains(Input.GetTouch(t).position))
                    {
                        if (Input.GetTouch(t).phase == TouchPhase.Began) beatmapPlayer.NoteHit(id);
                        else if (Input.GetTouch(t).phase == TouchPhase.Ended) beatmapPlayer.NoteRelease(id);
                }
                }
            }
        }

    }
}
