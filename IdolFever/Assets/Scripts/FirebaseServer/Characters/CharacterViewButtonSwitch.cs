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
        [SerializeField] private int bonus;

        #endregion

        #region Properties

        internal CharacterFactory.eCHARACTER CharacterIndex
        {
            // no need get
            set { index = value; }
        }

        internal int CharacterBonus
        {
            // no need get
            set { bonus = value; }
        }

        #endregion

        // on click button
        public void Clicked()
        {
            // trigger the event

            CharacterStorageEvents.INSTANCE.CharacterSwitched(new KeyValuePair<CharacterFactory.eCHARACTER, int>(index, bonus));
        }

    }
}