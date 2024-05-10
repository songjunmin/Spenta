using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicManager : MonoBehaviour
{
    public Toggle toggle;
    public Dropdown dropdown;
    
    public void ClickToggle()
    {
        if (toggle.isOn)
        {
            dropdown.interactable = false;
        }
        else
        {
            dropdown.interactable = true;
            ChangeGraphic();
        }
    }

    public void ChangeGraphic()
    {
        Debug.Log(dropdown.value);

        switch (dropdown.value)
        {
            case 0: Screen.SetResolution(1600, 900, false); break;
            case 1: Screen.SetResolution(1280, 720, false); break;
            case 2: Screen.SetResolution(960, 540, false); break;
            case 3: Screen.SetResolution(640, 360, false); break;

        }    
    }
}
