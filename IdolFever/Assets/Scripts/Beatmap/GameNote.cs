using UnityEngine;
using System.Collections;
using IdolFever.Beatmap;

public class GameNote : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        BeatmapReader.Open("Assets/BeatmapTest.mid");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
