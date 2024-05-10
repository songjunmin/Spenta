using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicManager : MonoBehaviour
{
    public Toggle toggle;
    public Dropdown dropdown;

    private List<string> resolutions = new List<string>();

    bool isMax = true;

    private void Awake()
    {
        resolutions = new List<string>();
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            resolutions.Add($"{Screen.resolutions[i].width}x{Screen.resolutions[i].height}");
        }
        dropdown.AddOptions(resolutions);
    }

    public void ClickToggle()
    {
        if (toggle.isOn)
        {
            isMax = true;
        }
        else
        {
            isMax = true;
        }
    }

    public void ChangeGraphic()
    {
        Debug.Log(dropdown.value);

        int resolutionIndex = dropdown.value;
        int width = Screen.resolutions[resolutionIndex].width;
        int height = Screen.resolutions[resolutionIndex].height;
        Screen.SetResolution(width, height, isMax);

        //switch (dropdown.value)
        //{
        //    case 0: Screen.SetResolution(1600, 900, false); break;
        //    case 1: Screen.SetResolution(1280, 720, false); break;
        //    case 2: Screen.SetResolution(960, 540, false); break;
        //    case 3: Screen.SetResolution(640, 360, false); break;
        //}    
    }
}
