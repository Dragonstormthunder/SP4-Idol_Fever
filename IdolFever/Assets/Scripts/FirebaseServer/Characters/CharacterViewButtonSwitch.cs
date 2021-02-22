using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IdolFever.Character;

namespace IdolFever.Server.Characters
{
    public class CharacterViewButtonSwitch : MonoBehaviour
    {

        #region Fields

        [SerializeField] private CharacterFactory.eCHARACTER index;

        #endregion

        #region Properties

        internal CharacterFactory.eCHARACTER CharacterIndex
        {
            // no need get
            set { index = value; }
        }

        #endregion

        // on click button
        public void Clicked()
        {
            // trigger the event
            CharacterStorageEvents.INSTANCE.CharacterSwitched(index);
        }

    }
}