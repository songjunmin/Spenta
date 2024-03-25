using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public GameObject cmr;

    public float moveSpeed;

    Vector3 cmrLoc;
    public Vector3 diffLoc;
    void Start()
    {
        cmrLoc = cmr.transform.position;
    }


    void Update()
    {
        diffLoc = cmrLoc - cmr.transform.position;
        cmrLoc = cmr.transform.position;

        transform.position -= diffLoc;
        transform.position += new Vector3(Time.deltaTime * moveSpeed, 0, 0);
    }
}
