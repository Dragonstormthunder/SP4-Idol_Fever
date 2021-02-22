using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdolFever.Server
{
    public class TaskFactory : MonoBehaviour
    {
        public static string TaskDescriptions(DailyTask.TaskType task, ref string name)
        {
            switch (task)
            {
                default:
                    name = "";
                    return "";

                case DailyTask.TaskType.COMPLETE_ONE_GAME_OF_MULTI:
                    name = "Complete Multiplayer Once";
                    return "";

                case DailyTask.TaskType.COMPLETE_FIVE_GAME:
                    name = "Complete 5 Round of Game";
                    return "";

                case DailyTask.TaskType.COMPLETE_ALL_TASK:
                    name = "Complete all Task";
                    return "";       
            }
        }
    }
}
