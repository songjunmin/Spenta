using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    void Start()
    {

    }

    void PortalInteraction()
    {
        GameManager.instance.NextStage();
    }

    public void SendInteraction()
    {
        GameManager.instance.NextStage();
    }
}
