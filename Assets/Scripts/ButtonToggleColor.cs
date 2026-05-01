using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IdolFever.UI
{
    // toggle the colors of the button
    public class ButtonToggleColor : MonoBehaviour
    {

        // state of the button at the time
        public bool switchedOn = false;

        private Color color;

        public bool ActiveState
        {
            get { return switchedOn; }
        }

        [SerializeField] private Button attachedButton; // button to this instance

        // to change the color of the images / text meshes
        // use lists for convience, what happens if there's both to change
        public List<TextMeshProUGUI> attachedTextMeshProUGUIs;
        public List<Image> attachedImages;

        public List<Button> associatedButtons;  // associated buttons

        // if there's a need for a menu controller
        [SerializeField] MenuSwitch menuSwitch;

        public string buttonColorOn;
        public string buttonColorOff;

        public string textColorOn;
        public string textColorOff;

        private void Start()
        {
            // set components
            attachedButton = GetComponent<Button>();

            // set menu
            menuSwitch = GetComponent<MenuSwitch>();

            // set menu proper elements
            menuSwitch?.SwitchMenuOnOff(switchedOn);

            // set the correct color
            SetAppropriateColor();
        }

        public void ToggleSwitchStateAndAssociatedButtons()
        {
            // already activated
            if (switchedOn)
                return;

            switchedOn = !switchedOn;

            menuSwitch?.SwitchMenuOnOff(switchedOn);

            SetAppropriateColor();

            for (int i = 0; i < associatedButtons.Count; ++i)
            {
                associatedButtons[i].GetComponent<ButtonToggleColor>().ToggleElements(!switchedOn);
            }
        }

        public void ToggleElements(bool active)
        {

            switchedOn = active;

            menuSwitch?.SwitchMenuOnOff(switchedOn);

            SetAppropriateColor();

        }

        public void ToggleSwitchState()
        {

            switchedOn = !switchedOn;

            menuSwitch?.SwitchMenuOnOff(switchedOn);

            SetAppropriateColor();

        }

        private void SetAppropriateColor()
        {
            if (switchedOn)
            {
                if (ColorUtility.TryParseHtmlString(buttonColorOn, out color))
                {
                    attachedButton.GetComponent<Image>().color = color;
                }

                if (ColorUtility.TryParseHtmlString(textColorOn, out color))
                {

                    for (int i = 0; i < attachedTextMeshProUGUIs.Count; ++i)
                        attachedTextMeshProUGUIs[i].color = color;

                    for (int i = 0; i < attachedImages.Count; ++i)
                        attachedImages[i].color = color;

                }
            }
            else
            {
                if (ColorUtility.TryParseHtmlString(buttonColorOff, out color))
                {
                    attachedButton.GetComponent<Image>().color = color;
                }

                if (ColorUtility.TryParseHtmlString(textColorOff, out color))
                {

                    for (int i = 0; i < attachedTextMeshProUGUIs.Count; ++i)
                        attachedTextMeshProUGUIs[i].color = color;

                    for (int i = 0; i < attachedImages.Count; ++i)
                        attachedImages[i].color = color;

                }
            }
        }
    }
}