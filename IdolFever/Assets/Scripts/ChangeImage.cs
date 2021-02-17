using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IdolFever.UI
{
    public class ChangeImage : MonoBehaviour
    {
        public Image imgOrignal1;
        public Image imgPressed1;

        public Image imgOrignal2;
        public Image imgPressed2;

        public Image imgOrignal3;
        public Image imgPressed3;

        public Image imgOrignal4;
        public Image imgPressed4;

        public KeyBindingManager key;

        // Start is called before the first frame update
        void Start()
        {
            imgOrignal1.enabled = true;
            imgPressed1.enabled = false;

            imgOrignal2.enabled = true;
            imgPressed2.enabled = false;

            imgOrignal3.enabled = true;
            imgPressed3.enabled = false;

            imgOrignal4.enabled = true;
            imgPressed4.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(key.B1_Key))
            {
                imgOrignal1.enabled = true;
                imgPressed1.enabled = false;
            }
            if (Input.GetKeyDown(key.B1_Key))
            {
                imgOrignal1.enabled = false;
                imgPressed1.enabled = true;
            }

            if (Input.GetKeyUp(key.B2_Key))
            {
                imgOrignal2.enabled = true;
                imgPressed2.enabled = false;
            }
            if (Input.GetKeyDown(key.B2_Key))
            {
                imgOrignal2.enabled = false;
                imgPressed2.enabled = true;
            }

            if (Input.GetKeyUp(key.B3_Key))
            {
                imgOrignal3.enabled = true;
                imgPressed3.enabled = false;
            }
            if (Input.GetKeyDown(key.B3_Key))
            {
                imgOrignal3.enabled = false;
                imgPressed3.enabled = true;
            }

            if (Input.GetKeyUp(key.B4_Key))
            {
                imgOrignal4.enabled = true;
                imgPressed4.enabled = false;
            }
            if (Input.GetKeyDown(key.B4_Key))
            {
                imgOrignal4.enabled = false;
                imgPressed4.enabled = true;
            }

            //if (Input.GetKey(key.B1_Key))
            //{
            //    isImgOn = true;
            //}
            //else
            //{
            //    isImgOn = false;
            //}
        }
    }
}
