using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IdolFever.Character;

public class RandomNumGenerator : MonoBehaviour
{
    public int rand;

    // Start is called before the first frame update
    void Start()
    {
        rand = 0;
    }

    public void PressRandButton()
    {
        rand = UnityEngine.Random.Range(0, 100);
        Debug.Log(rand);

        CharDrawn();
    }

    public void CharDrawn()
    {
        if (rand >= 16)//r - above 16
        {
            float rand2 = UnityEngine.Random.Range(0f, 1f);
            //g/b
            if (rand2 <= 0.5)
            {
                CharacterFactory.eCHARACTER c = CharacterFactory.eCHARACTER.R_CHARACTER_GIRL0;
                Debug.Log("You got " + c.ToString());
            }
            if (rand2 >= 0.51)
            {
                CharacterFactory.eCHARACTER c = CharacterFactory.eCHARACTER.R_CHARACTER_BOY0;
                Debug.Log("You got " + c.ToString());
            }

        }
        if (rand <= 15 && rand > 5)//sr above 20
        {
            float rand2 = UnityEngine.Random.Range(0f, 1f);
            //g/b
            if (rand2 <= 0.5)
            {
                CharacterFactory.eCHARACTER c = CharacterFactory.eCHARACTER.SR_CHARACTER_GIRL0;
                Debug.Log("You got " + c.ToString());
            }
            if (rand2 >= 0.51)
            {
                CharacterFactory.eCHARACTER c = CharacterFactory.eCHARACTER.SR_CHARACTER_BOY0;
                Debug.Log("You got " + c.ToString());
            }
        }
        if (rand <= 5)//ssr
        {
            float rand2 = UnityEngine.Random.Range(0f, 1f);
            //g/b
            if (rand2 <= 0.5)
            {
                CharacterFactory.eCHARACTER c = CharacterFactory.eCHARACTER.SSR_CHARACTER_GIRL0;
                Debug.Log("You got " + c.ToString());
            }
            if (rand2 >= 0.51)
            {
                CharacterFactory.eCHARACTER c = CharacterFactory.eCHARACTER.SSR_CHARACTER_BOY0;
                Debug.Log("You got " + c.ToString());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
