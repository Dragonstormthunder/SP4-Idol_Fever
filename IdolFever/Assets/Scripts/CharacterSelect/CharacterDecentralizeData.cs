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

                case CharacterFactory.eCHARACTER.SR_CHARACTER_GIRL0:
                    return "Uplifting Song+";

                case CharacterFactory.eCHARACTER.SR_CHARACTER_BOY0:
                    return "Swirling Song+";

                case CharacterFactory.eCHARACTER.SSR_CHARACTER_GIRL0:
                    return "Uplifting Song++";

                case CharacterFactory.eCHARACTER.SSR_CHARACTER_BOY0:
                    return "Swirling Song++";


            }

        }

        public string AccessCharacterSkillDescription(CharacterFactory.eCHARACTER index, int number)
        {
            //--number; // >1 chara grant bonus
            switch (index)
            {
                default:
                    return "";

                case CharacterFactory.eCHARACTER.R_CHARACTER_GIRL0:
                case CharacterFactory.eCHARACTER.SR_CHARACTER_GIRL0:
                case CharacterFactory.eCHARACTER.SSR_CHARACTER_GIRL0:
                    return AccessSkillMultiplier(index, number) + "x to score gain for " + AccessSkillDuration(index, number) + " seconds. Cooldown: " + AccessSkillCooldown(index, number) + " seconds.";

                case CharacterFactory.eCHARACTER.R_CHARACTER_BOY0:
                case CharacterFactory.eCHARACTER.SR_CHARACTER_BOY0:
                case CharacterFactory.eCHARACTER.SSR_CHARACTER_BOY0:
                    return AccessSkillMultiplier(index, number) + "x to the opponent's score gain " + AccessSkillDuration(index, number) + " seconds. Cooldown: " + AccessSkillCooldown(index, number) + " seconds.";

            }
        }

        public float AccessSkillMultiplier(CharacterFactory.eCHARACTER index, int number)
        {
            --number; // >1 chara grant bonus

            switch (index)
            {
                default:
                    return 0f;

                case CharacterFactory.eCHARACTER.R_CHARACTER_GIRL0:
                case CharacterFactory.eCHARACTER.SR_CHARACTER_GIRL0:
                case CharacterFactory.eCHARACTER.SSR_CHARACTER_GIRL0:
                    return skillMultiplier[(int)index] + 0.3f * number;

                case CharacterFactory.eCHARACTER.R_CHARACTER_BOY0:
                case CharacterFactory.eCHARACTER.SR_CHARACTER_BOY0:
                case CharacterFactory.eCHARACTER.SSR_CHARACTER_BOY0:
                    return skillMultiplier[(int)index];

            }
        }

        public float AccessSkillDuration(CharacterFactory.eCHARACTER index, int number)
        {
            --number; // >1 chara grant bonus

            switch (index)
            {
                default:
                    return 0f;

                case CharacterFactory.eCHARACTER.R_CHARACTER_GIRL0:
                case CharacterFactory.eCHARACTER.SR_CHARACTER_GIRL0:
                case CharacterFactory.eCHARACTER.SSR_CHARACTER_GIRL0:
                    return skillDuration[(int)index] + 0.3f * number;

                case CharacterFactory.eCHARACTER.R_CHARACTER_BOY0:
                case CharacterFactory.eCHARACTER.SR_CHARACTER_BOY0:
                case CharacterFactory.eCHARACTER.SSR_CHARACTER_BOY0:
                    return (skillDuration[(int)index] + 0.3f * number);

            }
        }

        public float AccessSkillCooldown(CharacterFactory.eCHARACTER index, int number)
        {
            --number; // >1 chara grant bonus

            switch (index)
            {
                default:
                    return 0f;

                case CharacterFactory.eCHARACTER.R_CHARACTER_GIRL0:
                case CharacterFactory.eCHARACTER.SR_CHARACTER_GIRL0:
                case CharacterFactory.eCHARACTER.SSR_CHARACTER_GIRL0:
                    return skillCooldown[(int)index];

                case CharacterFactory.eCHARACTER.R_CHARACTER_BOY0:
                case CharacterFactory.eCHARACTER.SR_CHARACTER_BOY0:
                case CharacterFactory.eCHARACTER.SSR_CHARACTER_BOY0:
                    return skillDuration[(int)index] - 0.3f * number;

            }
        }

        public bool BonusToSelf(CharacterFactory.eCHARACTER index)
        {

            switch (index)
            {
                default:
                    return true;

                case CharacterFactory.eCHARACTER.R_CHARACTER_GIRL0:
                case CharacterFactory.eCHARACTER.SR_CHARACTER_GIRL0:
                case CharacterFactory.eCHARACTER.SSR_CHARACTER_GIRL0:
                    return true;

                case CharacterFactory.eCHARACTER.R_CHARACTER_BOY0:
                case CharacterFactory.eCHARACTER.SR_CHARACTER_BOY0:
                case CharacterFactory.eCHARACTER.SSR_CHARACTER_BOY0:
                    return false;

            }

        }

    }
}