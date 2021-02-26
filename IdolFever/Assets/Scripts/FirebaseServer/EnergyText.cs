using TMPro;
using UnityEngine;
using System.Collections;

namespace IdolFever.Server
{
    public class EnergyText : MonoBehaviour
    {

        #region Fields

        public ServerDatabase serverDatabase;
        public TextMeshProUGUI energyText;
        public Transform progressbar;

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            // update the number of gems
            StartCoroutine(UpdateEnergy());
        }


        IEnumerator UpdateEnergy()
        {
            while (true)
            {
                System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
                int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
                StartCoroutine(serverDatabase.GetEnergy((energy) =>
                {
                    StartCoroutine(serverDatabase.GetMaxEnergy((maxEnergy) =>
                    {
                        StartCoroutine(serverDatabase.GetLastLogin((lastLogin) =>
                        {
                            if (lastLogin == 0) energy = maxEnergy;
                            else
                            {
                                energy += cur_time / 150 - lastLogin / 150;
                                if (energy > maxEnergy) energy = maxEnergy;
                            }
                            StartCoroutine(serverDatabase.UpdateEnergy(energy));
                            StartCoroutine(serverDatabase.UpdateLastLogin());
                            energyText.text = energy.ToString() + " / " + maxEnergy.ToString();
                            
                            if (progressbar != null)
                            {
                                progressbar.localScale = new Vector3((float)energy / (float)maxEnergy, progressbar.localScale.y, 0);
                            }

                        }));
                    }));
                }));
                yield return new WaitForSecondsRealtime(cur_time % 150 + 2);
            }
        }
    }

}
