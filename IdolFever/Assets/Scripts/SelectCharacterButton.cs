using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever.Character
{
    
    public class SelectCharacterButton : MonoBehaviour
    {

        #region Fields

        private Character.CharacterFactory.eCHARACTER characterIndex;
        private int bonus;

        #endregion


        #region Properties

        public Character.CharacterFactory.eCHARACTER CharacterIndex
        {
            get { return characterIndex; }
            set { characterIndex = value; }
        }

        public int CharacterBonus
        {
            get { return bonus; }
            set { bonus = value; }
        }

        #endregion

        #region Unity Message
        #endregion

        public void OnClick()
        {
            GameConfigurations.CharacterIndex = characterIndex;
            GameConfigurations.CharacterBonus = bonus;

            //Debug.Log("Character: " + GameConfigurations.CharacterIndex.ToString());
            //Debug.Log("Bonus: " + GameConfigurations.CharacterBonus);


        }

    }

}
