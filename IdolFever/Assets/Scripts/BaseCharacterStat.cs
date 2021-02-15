using System.Collections;
using System.Collections.Generic;

namespace IdolFever.Character
{

    enum eRARITY
    {
        RARITY_R,
        RARITY_SR,
        RARITY_SSR,
        NUM_RARITY
    }

    public class CBaseCharacterStat
    {

        string name;            // name of the character
        eRARITY rarity;         // rarity of the character
        
        string skillName;           // name of the character's skill
        string skillDescription;    // description of the skill for the character gallery
        float skillCooldown;        // duration of skill cool down rate

        // default constructor
        CBaseCharacterStat()
        {
        }

        CBaseCharacterStat(string name, eRARITY rarity, string skillName, string skillDescription, float skillCooldown)
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



    }

}