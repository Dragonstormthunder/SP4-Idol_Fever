using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

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

        #endregion

        #region Fields

        [Header("My Skill")]
        [SerializeField] private float multiplier;           // score multiplier
        [SerializeField] private float elaspedTime;          // elasped time
        [SerializeField] private float fixedCooldown;        // the constant cooldown
        [SerializeField] private float fixedSkillDuration;   // the constant duration
        [SerializeField] private SKILL_TYPE skill_type;      // skill type
        [SerializeField] private bool active;                // whether the skill is active

        // these variables should only be filled if the opponent
        // has a multiplier that damages our score gain
        [Header("Opponent's Skill (Photon Handling)")]
        [SerializeField] private float opponentMultiplier;
        [SerializeField] private float opponentCooldown;
        [SerializeField] private bool opponentActive = false;

        [Header("Feedback")]
        public GameObject mySkill;
        public GameObject opponentSkill;

        #endregion

        #region Properties

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
            set { fixedSkillDuration = value; }
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

        public float OpponentMultiplier
        {
            get { return opponentMultiplier; }
            set { opponentMultiplier = value; }
        }

        public float OpponentCooldown
        {
            get { return opponentCooldown; }
            set { opponentCooldown = value; }
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

        public void Update()
        {

            // -------------- if our skill is active ----------------
            elaspedTime -= Time.deltaTime;

            if (elaspedTime <= 0)
            {
                // flip the active state
                Active = !Active;

                if (active)
                {
                    // get the skill duration
                    elaspedTime = FixedSkillDuration;

                    // send the raise event here
                    if (PhotonNetwork.IsConnected && SKILL_TYPE.HINDER_TO_ENEMY == skill_type)
                    {

                        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                        {
                            //Receivers = ReceiverGroup.Others
                            Receivers = ReceiverGroup.All   // for editor testing
                        };

                        float[] data = new float[2];

                        // multiplier
                        data[0] = SkillMultiplier;
                        data[1] = FixedSkillDuration;

                        //Debug.Log("Sending the opponent skill over");
                        PhotonNetwork.RaiseEvent((byte)EventCodes.EventCode.SendSkillOver, data, raiseEventOptions, SendOptions.SendReliable);
                    }
                }
                else
                {
                    // get the skill cooldown
                    elaspedTime = FixedCooldown;
                }
            }

            // -------------- if opponent skill is active ----------------
            if (OpponentActive)
            {
                OpponentCooldown -= Time.deltaTime;

                // put it back to inactive and wait for the next event call to set it to active
                // if it comes
                if (OpponentCooldown <= 0f)
                {
                    OpponentActive = false;
                }
            }
        }

        #endregion

        public float ApplyBonuses(float score)
        {
            if (active && SKILL_TYPE.HINDER_TO_ENEMY != skill_type)
            {
                Debug.Log("Apply Bonus: Mine: " + score + " to " + (score * multiplier));
                score *= multiplier;
            }

            if (opponentActive)
            {
                Debug.Log("Apply Bonus: Opponent: " + score + " to " + (score * opponentMultiplier));
                score *= opponentMultiplier;
            }

            return score;
        }

    }

}