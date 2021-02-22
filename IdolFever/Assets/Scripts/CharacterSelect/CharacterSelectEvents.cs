using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever
{
    public class CharacterSelectEvents : MonoBehaviour
    {
        #region Fields

        public static CharacterSelectEvents INSTANCE;

        #endregion

        #region Unity Messages

        // singleton
        private void Awake()
        {
            INSTANCE = this;
        }

        #endregion

        #region Events

        #region Character Selected

        //internal event Action<Character> onCharacterSwitched;
        //internal void CharacterSwitched(Character.CharacterFactory.eCHARACTER index)
        //{
        //    onCharacterSwitched?.Invoke(index);
        //}

        #endregion

        #endregion


    }

}
