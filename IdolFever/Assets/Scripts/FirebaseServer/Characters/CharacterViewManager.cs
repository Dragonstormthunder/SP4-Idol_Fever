using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IdolFever.Character;

namespace IdolFever.Server.Characters
{
    public class CharacterViewManager : MonoBehaviour
    {

        #region Fields

        [EnumNamedArray(typeof(CharacterFactory.eCHARACTER))]
        public GameObject[] splashViewImages;

        #endregion

        public void SetWhichImageActive(CharacterFactory.eCHARACTER index)
        {

            for (int i = 0; i < splashViewImages.Length; ++i)
                if (splashViewImages[i] != null)
                splashViewImages[i].SetActive(false);

            splashViewImages[(int)index].SetActive(true);

        }



    }
}