using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever
{

    public class CharacterStorageEvents : MonoBehaviour
    {

        #region Fields

        public static CharacterStorageEvents INSTANCE;

        #endregion

        #region Unity Messages

        // singleton
        private void Awake()
        {
            INSTANCE = this;
        }

        #endregion

        #region Events

        #region Different Character Selected

        //internal event Action<Character.CharacterFactory.eCHARACTER> onCharacterSwitched;
        //internal void CharacterSwitched(Character.CharacterFactory.eCHARACTER index)
        //{
        //    onCharacterSwitched?.Invoke(index);
        //}

        internal event Action<KeyValuePair<Character.CharacterFactory.eCHARACTER, int>> onCharacterSwitched;
        internal void CharacterSwitched(KeyValuePair<Character.CharacterFactory.eCHARACTER, int> index)
        {
            onCharacterSwitched?.Invoke(index);
        }

        #endregion

        #region Character Description Event

        internal event Action<Server.Characters.CharacterDataDisplay> onCharacterDescriptionSwitched;
        internal void CharacterDescriptionSwitched(Server.Characters.CharacterDataDisplay characterDataDisplay)
        {
            onCharacterDescriptionSwitched?.Invoke(characterDataDisplay);
        }

        #endregion

        #endregion

    }
}