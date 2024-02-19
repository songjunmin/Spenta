using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public void SendInteraction()
    {
        Debug.Log("받음");
        string name = gameObject.name;
        
        if (name == "normalBonfire")
        {
            Debug.Log("받고 실행함");
            GameManager.instance.GetComponent<WarrantSystem>().OpenAmeshaWarrant();
            
        }
    }
}
