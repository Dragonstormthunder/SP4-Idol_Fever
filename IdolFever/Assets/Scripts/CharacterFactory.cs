using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever.Character
{

    public static class CharacterFactory
    {
        public enum eCHARACTER
        {
            // rare
            R_CHARACTER_BEGIN,
            R_CHARACTER_GIRL0,
            R_CHARACTER_BOY0,
            R_CHARACTER_END,

            // super rare
            SR_CHARACTER_BEGIN,
            SR_CHARACTER_GIRL0,
            SR_CHARACTER_BOY0,
            SR_CHARACTER_END,

            // super super rare
            SSR_CHARACTER_BEGIN,
            SSR_CHARACTER_GIRL0,
            SSR_CHARACTER_BOY0,
            SSR_CHARACTER_END,

        }

        public static CBaseCharacterStat CreateCharacter(eCHARACTER eCharToSpawn)
        {

            CBaseCharacterStat character = new CBaseCharacterStat();
            character.Name = eCharToSpawn.ToString();
            switch (eCharToSpawn)
            {
                default:
                    return null;
                case eCHARACTER.R_CHARACTER_GIRL0:
                    character.SkillName = "Skill";
                    character.Rarity = eRARITY.RARITY_R;
                    character.SkillCooldown = 30f;
                    character.SkillDescription = "CharacterSkillDescription";
                    break;
                case eCHARACTER.R_CHARACTER_BOY0:
                    character.SkillName = "Skill1";
                    character.Rarity = eRARITY.RARITY_R;
                    character.SkillCooldown = 30f;
                    character.SkillDescription = "CharacterSkillDescription";
                    break;
            }

            return null;

        }
    }
}