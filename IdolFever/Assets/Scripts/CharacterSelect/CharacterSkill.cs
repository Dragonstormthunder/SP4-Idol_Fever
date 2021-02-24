using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;

namespace IdolFever.Character
{

    public class CharacterSkill : MonoBehaviour
    {

        #region Enum

        internal enum SKILL_TYPE
        {
            BONUS_TO_SELF,
            HINDER_TO_ENEMY,
            NUM_SKILL_TYPE
        }

        // make sure I don't get the numbers wrong
        internal enum PHOTON_DATA_SEND
        {
            SEND_OPPONENT_INDEX,
            SEND_MULTIPLIER,
            SEND_COOLDOWN,
            SEND_SKILL_TYPE,
            NUM_PHOTON_DATA_SEND
        }

        #endregion

        #region Fields

        [Header("My Skill")]
        [SerializeField] private CharacterFactory.eCHARACTER characterIndex; // thumbnail item
        [SerializeField] private float multiplier;           // score multiplier, may change due to bonus
        [SerializeField] private float elaspedTime;          // elasped time
        [SerializeField] private float fixedCooldown;        // the constant cooldown
        [SerializeField] private float fixedSkillDuration;   // the constant duration
        [SerializeField] private SKILL_TYPE skill_type;      // skill type
        [SerializeField] private bool active;                // whether the skill is active
        [SerializeField] SkillProgressBarUI skillProgressBarUI;

        // these variables should only be filled if the opponent
        // has a multiplier that damages our score gain
        [Header("Opponent's Skill (Photon Handling)")]
        [SerializeField] private CharacterFactory.eCHARACTER opponentIndex; // thumbnail item
        [SerializeField] private float opponentMultiplier;      // may change due to bonus
        [SerializeField] private float opponentSkillDuration;        // reduce sending, so send cooldown time
        [SerializeField] private SKILL_TYPE opponentSkill_Type; // even if it's bonus to self want to show opponent's thing
        [SerializeField] private bool opponentActive = false;   // default value so in singleplayer this will not get activated
        [SerializeField] SkillProgressBarUI opponentskillProgressBarUI;

        [Header("Feedback")]
        public GameObject mySkill;
        public TextMeshProUGUI mySkillName;
        public TextMeshProUGUI mySkillMultiplier;
        public Transform myThumbnailParent;
        public GameObject opponentSkill;
        public TextMeshProUGUI opponentSkillName;
        public TextMeshProUGUI opponentSkillMultiplier;
        public Transform opponentThumbnailParent;

        [Header("Data")]
        public CharacterDecentralizeData characterDecentralizeData;

        #endregion

        #region Properties

        public CharacterFactory.eCHARACTER CharacterIndex
        {
            get { return characterIndex; }
            set { characterIndex = value; }
        }

        public float SkillMultiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }

        public float FixedCooldown
        {
            get { return fixedCooldown; }
            set { fixedCooldown = value; }
        }

        public float ElaspedTime
        {
            get { return elaspedTime; }
            set { elaspedTime = value; }
        }

        public float FixedSkillDuration
        {
            get { return fixedSkillDuration; }
            set
            {
                skillProgressBarUI.MaxValue = skillProgressBarUI.MinValue = value;
                fixedSkillDuration = value;
            }
        }

        internal SKILL_TYPE Skill_Type
        {
            get { return skill_type; }
            set { skill_type = value; }
        }

        public bool Active
        {
            get { return active; }
            set
            {
                mySkill.SetActive(value);
                active = value;
            }
        }

        public CharacterFactory.eCHARACTER OpponentCharacterIndex
        {
            get { return opponentIndex; }
            set { opponentIndex = value; }
        }

        public float OpponentMultiplier
        {
            get { return opponentMultiplier; }
            set { opponentMultiplier = value; }
        }

        public float OpponentSkillDuration
        {
            get { return opponentSkillDuration; }
            set
            {
                opponentskillProgressBarUI.MaxValue = opponentskillProgressBarUI.MinValue = value;
                opponentSkillDuration = value;
            }
        }

        internal SKILL_TYPE OpponentSkill_Type
        {
            get { return opponentSkill_Type; }
            set { opponentSkill_Type = value; }
        }

        public bool OpponentActive
        {
            get { return opponentActive; }
            set
            {
                opponentSkill.SetActive(value);
                opponentActive = value;
            }
        }

        #endregion

        #region Unity Messages

        private void Start()
        {

            // time to initialize all the values
            CharacterIndex = GameConfigurations.CharacterIndex;
            SkillMultiplier = characterDecentralizeData.AccessSkillMultiplier(CharacterIndex, GameConfigurations.CharacterBonus);
            FixedCooldown = characterDecentralizeData.AccessSkillCooldown(CharacterIndex, GameConfigurations.CharacterBonus);
            FixedSkillDuration = characterDecentralizeData.AccessSkillDuration(CharacterIndex, GameConfigurations.CharacterBonus);
            if (characterDecentralizeData.BonusToSelf(CharacterIndex))
            {
                Skill_Type = SKILL_TYPE.BONUS_TO_SELF;
            }
            else
            {
                Skill_Type = SKILL_TYPE.HINDER_TO_ENEMY;
            }

            // set to inactive, don't want to start skill in the beginning
            Active = false;
            // set to inactive as default, if a photon network comes in they will init this one
            OpponentActive = false;

            // wait for a time before starting the skill
            ElaspedTime = FixedCooldown;

            // feedback initilization
            mySkillName.text = characterDecentralizeData.AccessCharacterSkillName(CharacterIndex);
            mySkillMultiplier.text = SkillMultiplier.ToString() + "x";

            // get the thumbnail instantiation
            Instantiate(characterDecentralizeData.AccessThumbnailPrefab(CharacterIndex), myThumbnailParent);

            // prepare for the skill items
            skillProgressBarUI.MaxValue = skillProgressBarUI.MinValue = fixedSkillDuration;

        }

        public void Update()
        {

            // -------------- if our skill is active ----------------
            ElaspedTime -= Time.deltaTime;

            if (elaspedTime <= 0)
            {
                // flip the active state
                // this makes sure to flip the activity of the gameobjects as well
                Active = !Active;

                if (Active)
                {
                    // get the skill duration
                    ElaspedTime = FixedSkillDuration;

                    // reset our own skill progress bar
                    skillProgressBarUI.MinValue = skillProgressBarUI.MaxValue = FixedSkillDuration;

                    // send the raise event here
                    if (PhotonNetwork.IsConnected)
                    {

                        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                        {
                            //Receivers = ReceiverGroup.Others
                            Receivers = ReceiverGroup.All   // for editor testing
                        };

                        float[] data = new float[(int)PHOTON_DATA_SEND.NUM_PHOTON_DATA_SEND];

                        data[(int)PHOTON_DATA_SEND.SEND_OPPONENT_INDEX] = (float)CharacterIndex;
                        data[(int)PHOTON_DATA_SEND.SEND_MULTIPLIER] = SkillMultiplier;
                        data[(int)PHOTON_DATA_SEND.SEND_COOLDOWN] = FixedSkillDuration;
                        data[(int)PHOTON_DATA_SEND.SEND_SKILL_TYPE] = (float)Skill_Type;

                        //Debug.Log("Sending the opponent skill over");
                        PhotonNetwork.RaiseEvent((byte)EventCodes.EventCode.SendSkillOver, data, raiseEventOptions, SendOptions.SendReliable);
                    }
                }
                else
                {
                    // get the skill cooldown
                    elaspedTime = FixedCooldown;

                    // do no need to send the photon network because they have the cooldown on their side
                    // cuts down on the information needing to be sent
                }
            }

            // -------------- if opponent skill is active ----------------
            if (OpponentActive)
            {
                OpponentSkillDuration -= Time.deltaTime;

                // put it back to inactive and wait for the next event call to set it to active
                // if it comes
                if (OpponentSkillDuration <= 0f)
                {
                    OpponentActive = false;
                }
            }
        }

        #endregion

        public float ApplyBonuses(float score)
        {
            // if I am active, and my skill isn't the type to hinder the enemy
            if (Active && SKILL_TYPE.HINDER_TO_ENEMY != Skill_Type)
            {
                Debug.Log("Apply Bonus: Mine: " + score + " * " + multiplier + " :" + (score * multiplier));
                score *= multiplier;
            }

            // if opponent skill is active, and my opponent skill is to hinder me
            if (OpponentActive && SKILL_TYPE.HINDER_TO_ENEMY == OpponentSkill_Type)
            {
                Debug.Log("Apply Bonus: Opponent: " + score + " * " + multiplier + " :" + (score * opponentMultiplier));
                score *= opponentMultiplier;
            }

            return score;
        }

    }

}