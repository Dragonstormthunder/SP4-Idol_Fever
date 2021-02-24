using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IdolFever.Server
{
    public class TaskBarHandler : MonoBehaviour
    {
        private static Image TaskhBarImage;

        public static void SetTaskBarValue(float value)
        {
            TaskhBarImage.fillAmount = value;
            if (TaskhBarImage.fillAmount < 0.2f)
            {
                SetTaskBarColor(Color.red);
            }
            else if (TaskhBarImage.fillAmount < 0.4f)
            {
                SetTaskBarColor(Color.yellow);
            }
            else
            {
                SetTaskBarColor(Color.green);
            }
        }

        public static float GetTaskBarValue()
        {
            return TaskhBarImage.fillAmount;
        }

        public static void SetTaskBarColor(Color taskColor)
        {
            TaskhBarImage.color = taskColor;
        }


        private void Start()
        {
            TaskhBarImage = GetComponent<Image>();
        }
    }
}
