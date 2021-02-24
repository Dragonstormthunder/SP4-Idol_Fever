using TMPro;
using UnityEngine;

namespace IdolFever.Server
{
    public class EnergyText : MonoBehaviour
    {

        #region Fields

        public ServerDatabase serverDatabase;
        public TextMeshProUGUI energyText;

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            // update the number of gems
            StartCoroutine(serverDatabase.GetEnergy((energy) =>
            {
                energyText.text = energy.ToString();
            }));
        }

    }

}
