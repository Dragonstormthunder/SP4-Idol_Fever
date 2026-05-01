using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever.UI
{
    public class MenuSwitch : MonoBehaviour
    {

        public GameObject menuContainer;

        // switch menu on off using hierarchy
        public void SwitchMenuOnOff(bool active)
        {
            // check for nullptr first
            menuContainer?.SetActive(active);
        }

    }
}