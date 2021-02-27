using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyCost : MonoBehaviour
{

    [SerializeField] IdolFever.Server.ServerDatabase b;
    [SerializeField] IdolFever.AsyncSceneTransitionOutWithAlts o;
    private bool enoughEnergy = false;
    public Image buttonImage;
    public Button button;

    // Use this for initialization
    void Start()
    {
        // update the number of gems
        StartCoroutine(CheckEnoughEnergy());
    }

    private void Update()
    {
        if (enoughEnergy == false)
        {
            button.transition = Selectable.Transition.None;
            buttonImage.color = Color.gray;
        }
        else
        {
            button.transition = Selectable.Transition.ColorTint;
            buttonImage.color = Color.white;
        }
    }

    public IEnumerator CheckEnoughEnergy()
    {
        while (enoughEnergy == false)
        {
            _ = StartCoroutine(b.GetEnergy((d) =>
              {
                  if (d - 3 > 0)
                  {
                      enoughEnergy = true;
                  }
              }));

            if (enoughEnergy)
                break;
            else
                yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    public void ChangeEnergy(int i)
    {
        StartCoroutine(b.GetEnergy((d) =>
        {
            Debug.Log("Energy: " + (d).ToString());
            if (d + i > 0)
            {
                StartCoroutine(b.UpdateEnergy(d + i));
                o.ChangeSceneByFlag();
            }
        }));
    }

}
