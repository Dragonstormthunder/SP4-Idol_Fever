using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IdolFever.Character;

namespace IdolFever.Server.Characters
{
    public class CharacterViewButtonSwitch : MonoBehaviour
    {

        #region Fields

        public CharacterFactory.eCHARACTER index;
        public CharacterViewManager characterViewManager;

        #endregion

        // switch the view
        public void SwitchView()
        {
            characterViewManager.SetWhichImageActive(index);
        }

    }
}