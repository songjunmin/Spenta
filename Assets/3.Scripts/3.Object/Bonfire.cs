using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public void SendInteraction()
    {
        Debug.Log("����");
        string name = gameObject.name;
        
        if (name == "normalBonfire")
        {
            Debug.Log("�ް� ������");
            GameManager.instance.GetComponent<WarrantSystem>().OpenAmeshaWarrant();
            
        }
    }
}
