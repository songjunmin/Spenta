using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public string objectName;
    private void Start()
    {
        objectName = gameObject.name;
    }

    public void SendInteraction()
    {
        switch (objectName)
        {
            case "NormalBonfire":
                GameManager.instance.GetComponent<WarrantSystem>().OpenAmeshaWarrant();
                break;

            case "SpecialBonfire":
                GameManager.instance.GetComponent<WarrantSystem>().OpenSpentaWarrant();
                break;

        }
    }
}
