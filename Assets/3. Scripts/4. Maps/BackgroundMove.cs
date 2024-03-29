using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float degree;

    public GameObject cmr;

    public Vector3 startVec;
    public Vector3 startCmr;
    public Vector3 nowCmr;

    void Start()
    {
        cmr = GameManager.instance.transform.GetChild(2).gameObject;

        startVec = transform.position;
        startCmr = cmr.transform.position;
    }


    void Update()
    {
        nowCmr = cmr.transform.position;
        transform.position =(nowCmr - startCmr) * degree + startVec;
    }
}
