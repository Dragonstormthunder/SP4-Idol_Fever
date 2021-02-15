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
            R_CHARACTER_0,
            //R_CHARACTER_1,
            //R_CHARACTER_2,
            R_CHARACTER_END,

            // super rare
            SR_CHARACTER_BEGIN,
            //SR_CHARACTER_0,
            //SR_CHARACTER_1,
            //SR_CHARACTER_2,
            SR_CHARACTER_END,

            // super super rare
            SSR_CHARACTER_BEGIN,
            //SSR_CHARACTER_0,
            //SSR_CHARACTER_1,
            //SSR_CHARACTER_2,
            SSR_CHARACTER_END,
        }

        public static CBaseCharacterStat CreateCharacter(eCHARACTER eCharToSpawn)
        {

            CBaseCharacterStat character = new CBaseCharacterStat();

            switch (eCharToSpawn)
            {
                default:
                    return null;
                case eCHARACTER.R_CHARACTER_0:
                    character.Name = "Character1";
                    character.SkillName = "Skill1";
                    character.Rarity = eRARITY.RARITY_R;
                    character.SkillCooldown = 30f;
                    character.SkillDescription = "CharacterSkillDescription1";
                    break;
            }

            return null;

        }

    }
}