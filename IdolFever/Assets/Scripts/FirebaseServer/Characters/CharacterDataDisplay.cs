using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IdolFever.Character;

namespace IdolFever.Server.Characters
{
    public class CharacterDataDisplay : MonoBehaviour
    {

        #region Fields 

        [SerializeField] CharacterFactory.eCHARACTER index;
        [SerializeField] int bonus;             // number of charas
        [SerializeField] string description;    // description

        #endregion

        #region Properties

        public int Bonus
        {
            get { return bonus; }
            set { bonus = value; }
        }

        #endregion


    }
}
