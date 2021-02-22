using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskProgress : MonoBehaviour
{
    private Image taskMeterImg;
    private float taskProgress;
    public float maxtaskProgress;

    /// Sets the health bar value
    /// value should be between 0 to 1</param>
    public void SetTaskProgressValue(float value)
    {
        taskProgress = value;
        taskMeterImg.fillAmount = taskProgress / maxtaskProgress;

        float factor = taskMeterImg.fillAmount * 0.5f + 0.8f;
        if (factor > 1.0f)
        {
            factor -= 1.0f;
        }
        SetTaskProgressColor(Color.HSVToRGB(factor, 1.0f, 1.0f));
    }

    public float GetTaskProgressValue()
    {
        return taskProgress;
    }

    public void AddTaskProgress(float a)
    {
        SetTaskProgressValue(taskProgress + a);
    }

    public void SetTaskProgress(float score)
    {
        SetTaskProgressValue(taskProgress);
    }

    public void SetTaskProgressColor(Color healthColor)
    {
        taskMeterImg.color = healthColor;
    }

    private void Start()
    {
        taskMeterImg = GetComponent<Image>();
        taskProgress = 0;
    }
}
