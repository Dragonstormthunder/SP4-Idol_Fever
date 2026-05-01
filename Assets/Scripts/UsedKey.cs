using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IdolFever.UI
{

    public class UsedKey : MonoBehaviour
    {
        public KeyBindingManager key;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(key.B1_Key))
            {
                print("hit B1_Key. B1_Key: " + key.B1_Key.ToString());
            }
            if (Input.GetKeyDown(key.B2_Key))
            {
                print("hit B2_Key. B2_Key: " + key.B2_Key.ToString());
            }
            if (Input.GetKeyDown(key.B3_Key))
            {
                print("hit B3_Key. B3_Key: " + key.B3_Key.ToString());
            }
            if (Input.GetKeyDown(key.B4_Key))
            {
                print("hit B4_Key. B4_Key: " + key.B4_Key.ToString());
            }
        }
    }
}