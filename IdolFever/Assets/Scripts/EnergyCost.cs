using UnityEngine;
using System.Collections;

public class EnergyCost : MonoBehaviour
{

    [SerializeField] IdolFever.Server.ServerDatabase b;
    // [SerializeField] IdolFever.AsyncSceneTransitionOut o;
    [SerializeField] IdolFever.AsyncSceneTransitionOutWithAlts o;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeEnergy(int i)
    {
        StartCoroutine(b.GetEnergy((d) => {
            if(d+i > 0)
            {
                b.UpdateEnergy(d + i);
                o.ChangeSceneByFlag();
            }
        }));
    }

}
