using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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