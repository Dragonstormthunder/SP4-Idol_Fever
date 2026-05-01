using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IdolFever.UI
{
    public class VirtualDPad : MonoBehaviour
    {
        public enum D_PAD_DIR
        {
            PAD_TAPPED,
            PAD_LEFT,
            PAD_RIGHT,
            PAD_UP,
            PAD_DOWN,
            NUM_PAD
        }

        // don't want other locations accessing it changing the value
        public D_PAD_DIR DPadDirection
        {
            get { return dpadDirection; }
        }

        public Text directionText;
        private Touch theTouch;
        private Vector2 touchStartPosition;
        private Vector2 touchEndPosition;
        [SerializeField] private D_PAD_DIR dpadDirection;

        void Update()
        {
            if (Input.touchCount > 0)
            {
                theTouch = Input.GetTouch(0);

                if (theTouch.phase == TouchPhase.Began)
                {
                    touchStartPosition = theTouch.position;
                }
                else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
                {
                    touchEndPosition = theTouch.position;

                    float x = touchEndPosition.x - touchStartPosition.x;
                    float y = touchEndPosition.y - touchStartPosition.y;

                    if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                    {
                        dpadDirection = D_PAD_DIR.PAD_TAPPED;
                    }
                    else if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        dpadDirection = x > 0 ? D_PAD_DIR.PAD_RIGHT : D_PAD_DIR.PAD_LEFT;
                    }
                    else
                    {
                        dpadDirection = y > 0 ? D_PAD_DIR.PAD_UP : D_PAD_DIR.PAD_DOWN;
                    }
                }
            }

            directionText.text = dpadDirection.ToString();

        }

    }
}