using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string changeTo;

    public void ChangetoScene()
    {
        if (changeTo.Length <= 0)
            Debug.Log("There is no scene to change to.");
        else
        {
            SceneManager.LoadScene(changeTo);
        }
    }
}
