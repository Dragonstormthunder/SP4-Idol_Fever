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
        private Rect rect;

        void Start()
        { 
            rect = new Rect(new Vector2(Screen.width / 2 + (id - 2) * 480, 0), new Vector2(480, Screen.height));
        }
        void Update()
        {
            if (Input.touchCount > 0)
            {
                for (int t = 0; t < Input.touchCount; ++t)
                {
                    if (rect.Contains(Input.GetTouch(t).rawPosition))
                    {
                        if (Input.GetTouch(t).phase == TouchPhase.Began) beatmapPlayer.NoteHit(id);
                        else if (Input.GetTouch(t).phase == TouchPhase.Ended) beatmapPlayer.NoteRelease(id);
                    }
                }
            }
        }

    }
}
