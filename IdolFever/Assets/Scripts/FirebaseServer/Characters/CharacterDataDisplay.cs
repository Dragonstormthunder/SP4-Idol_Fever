using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever.Server.Characters
{
    public class CharacterDataDisplay : MonoBehaviour
    {

        #region Fields 

        [SerializeField] int bonus;             // number of charas
        [SerializeField] string description;    // description

        #endregion

        #region Properties

        public int Bonus
        {
            get { return bonus; }
            set { bonus = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        #endregion

        public void SetDescription(Character.CharacterFactory.eCHARACTER index)
        {
            switch (index)
            {
                default:
                    description = "Nothing For Now: Bonus: " + bonus;
                    break;
            }
        }



    }
}
