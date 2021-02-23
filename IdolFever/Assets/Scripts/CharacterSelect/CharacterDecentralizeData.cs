using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever.Character
{
    public class CharacterDecentralizeData : MonoBehaviour
    {

        #region Fields

        [EnumNamedArray(typeof(CharacterFactory.eCHARACTER))]
        public GameObject[] thumbnailPrefabs = new GameObject[(int)CharacterFactory.eCHARACTER.NUM_CHARACTER];

        [EnumNamedArray(typeof(CharacterFactory.eCHARACTER))]
        public GameObject[] splashArtPrefabs = new GameObject[(int)CharacterFactory.eCHARACTER.NUM_CHARACTER];

        [EnumNamedArray(typeof(CharacterFactory.eCHARACTER))]
        public GameObject[] featureArtPrefabs = new GameObject[(int)CharacterFactory.eCHARACTER.NUM_CHARACTER];

        [EnumNamedArray(typeof(CharacterFactory.eCHARACTER))]
        public float[] skillMultiplier = new float[(int)CharacterFactory.eCHARACTER.NUM_CHARACTER];

        [EnumNamedArray(typeof(CharacterFactory.eCHARACTER))]
        public float[] skillCooldown = new float[(int)CharacterFactory.eCHARACTER.NUM_CHARACTER];

        [EnumNamedArray(typeof(CharacterFactory.eCHARACTER))]
        public float[] skillDuration = new float[(int)CharacterFactory.eCHARACTER.NUM_CHARACTER];

        #endregion

        #region Unity Messages

        #endregion

        public GameObject AccessThumbnailPrefab(CharacterFactory.eCHARACTER index)
        {
            return thumbnailPrefabs[(int)index];
        }

        public GameObject AccessSplashArtPrefab(CharacterFactory.eCHARACTER index)
        {
            return splashArtPrefabs[(int)index];
        }

        public GameObject AccessFeatureArtPrefab(CharacterFactory.eCHARACTER index)
        {
            return featureArtPrefabs[(int)index];
        }

        public string AccessCharacterSkillName(CharacterFactory.eCHARACTER index)
        {
            switch (index)
            {
                default:
                    return "";

                case CharacterFactory.eCHARACTER.R_CHARACTER_GIRL0:
                    return "Uplifting Song";

                case CharacterFactory.eCHARACTER.R_CHARACTER_BOY0:
                    return "Swirling Song";

            }

        }

        public string AccessCharacterSkillDescription(CharacterFactory.eCHARACTER index, int bonus)
        {
            --bonus; // >1 chara grant bonus
            switch (index)
            {
                default:
                    return "";

                case CharacterFactory.eCHARACTER.R_CHARACTER_GIRL0:
                    return (skillMultiplier[(int)index] + 0.3f * bonus) + "x to score gain for " + (skillDuration[(int)index] + 0.3f * bonus) + " seconds. Cooldown: " + skillCooldown[(int)index] + " seconds.";

                case CharacterFactory.eCHARACTER.R_CHARACTER_BOY0:
                    return skillMultiplier[(int)index] + "x to the opponent's score gain " + (skillDuration[(int)index] + 0.3f * bonus) + " seconds. Cooldown: " + (skillDuration[(int)index] - 0.3f * bonus) + " seconds.";

            }
        }

    }
}