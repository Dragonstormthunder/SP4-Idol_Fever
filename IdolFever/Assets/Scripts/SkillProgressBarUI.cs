using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillProgressBarUI : MonoBehaviour
{

    #region Fields

    [SerializeField] private float maxValue;
    [SerializeField] private float minValue;
    [SerializeField] private Image image;

    #endregion

    #region Properties

    public float MaxValue
    {
        get { return maxValue; }
        set { maxValue = value; }
    }

    public float MinValue
    {
        get { return minValue; }
        set { minValue = value; }
    }

    #endregion

    #region Unity Messages

    private void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {

        minValue -= Time.deltaTime;

        // scale the item
        transform.localScale = new Vector2(minValue / maxValue, transform.localScale.y);

        // set the color
        float factor = minValue / maxValue * 0.5f + 0.8f;
        if (factor > 1.0f)
        {
            factor -= 1.0f;
        }

        image.color = Color.HSVToRGB(factor, 1.0f, 1.0f);

    }

    #endregion

}
