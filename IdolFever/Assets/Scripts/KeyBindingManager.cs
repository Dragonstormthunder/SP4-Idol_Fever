using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingManager : MonoBehaviour
{
    private List<string> keys;
    public List<Dropdown> dropdowns = new List<Dropdown>();
    public KeyCode B1_Key;
    public KeyCode B2_Key;
    public KeyCode B3_Key;
    public KeyCode B4_Key;

    // Start is called before the first frame update
    void Start()
    {
        keys = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "S" };
        for (int i = 0; i < dropdowns.Count; ++i) 
        {
            dropdowns[i].AddOptions(keys);
        }

        PlayerPrefs.SetString("ButtonOne", "A");
        PlayerPrefs.SetString("ButtonTwo", "S");
        PlayerPrefs.SetString("ButtonThree", "D");
        PlayerPrefs.SetString("ButtonFour", "F");
        LoadPrefabs();
    }

    private void LoadPrefabs()
    {
        B1_Key = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonOne"));
        SelectKey(dropdowns[0], B1_Key.ToString());

        B2_Key = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonTwo"));
        SelectKey(dropdowns[1], B2_Key.ToString());

        B3_Key = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonThree"));
        SelectKey(dropdowns[2], B3_Key.ToString());

        B4_Key = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ButtonFour"));
        SelectKey(dropdowns[3], B4_Key.ToString());
    }

    private void SelectKey(Dropdown _dropdown, string _s)
    {
        for (int i = 0; i < keys.Count; ++i) 
        {
            if(_s==keys[i])
            {
                _dropdown.value = i;
            }
        }
    }

    public void ChangeButtonOneKey(int id)
    {
        B1_Key = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys[id]);
        PlayerPrefs.SetString("ButtonOne", keys[id]);
    }

    public void ChangeButtonTwoKey(int id)
    {
        B2_Key = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys[id]);
        PlayerPrefs.SetString("ButtonTwo", keys[id]);
    }

    public void ChangeButtonThreeKey(int id)
    {
        B3_Key = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys[id]);
        PlayerPrefs.SetString("ButtonThree", keys[id]);
    }

    public void ChangeButtonFourKey(int id)
    {
        B4_Key = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys[id]);
        PlayerPrefs.SetString("ButtonFour", keys[id]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
