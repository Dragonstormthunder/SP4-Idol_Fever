using System.Collections;
using System.Collections.Generic;

namespace IdolFever.Character
{

    public enum eRARITY
    {
        RARITY_R,
        RARITY_SR,
        RARITY_SSR,
        NUM_RARITY
    }

    public class CBaseCharacterStat
    {

        // default constructor
        public CBaseCharacterStat()
        {
        }

        // overloaded constructor
        public CBaseCharacterStat(string name, eRARITY rarity, string skillName, string skillDescription, float skillCooldown)
        {
            this.name = name;
            this.rarity = rarity;
            this.skillName = skillName;
            this.skillDescription = skillDescription;
            this.skillCooldown = skillCooldown;
        }

        // properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public eRARITY Rarity
        {
            get { return rarity; }
            set { rarity = value; }
        }

        public string SkillName
        {
            get { return skillName; }
            set { skillName = value; }
        }

        public string SkillDescription
        {
            get { return skillDescription; }
            set { skillDescription = value; }
        }

        public float SkillCooldown
        {
            get { return skillCooldown; }
            set { skillCooldown = value; }
        }

        private string name;                // name of the character
        private int bonus;                  // bonus of the character
                                            // e.g. reroll bonus
        private eRARITY rarity;             // rarity of the character

        private string skillName;           // name of the character's skill
        private string skillDescription;    // description of the skill for the character gallery
        private float skillCooldown;        // duration of skill cool down rate

    }

}