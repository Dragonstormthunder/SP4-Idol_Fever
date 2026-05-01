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
            Debug.Log("Energy Text Start Coroutine");
            // update the number of gems
            StartCoroutine(UpdateEnergy());
        }


        IEnumerator UpdateEnergy()
        {
            while (true)
            {
                Debug.Log("Energy Text While Loop start");
                System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
                int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
                StartCoroutine(serverDatabase.GetEnergy((energy) =>
                {
                    Debug.Log("Energy Text while loop got energy");
                    StartCoroutine(serverDatabase.GetMaxEnergy((maxEnergy) =>
                    {
                        Debug.Log("Energy Text while loop got max energy");
                        StartCoroutine(serverDatabase.GetLastLogin((lastLogin) =>
                        {
                            Debug.Log("Energy Text while loop got last login start");
                            if (lastLogin != -1)
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
                            Debug.Log("Energy Text while loop got last login end");
                        }));
                    }));
                }));
                Debug.Log("Energy Text while loop before yield");
                yield return new WaitForSecondsRealtime(cur_time % 150 + 2);
            }
        }
    }

}
