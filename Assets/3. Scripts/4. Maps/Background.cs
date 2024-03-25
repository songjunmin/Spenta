using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject cmr;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =  cmr.transform.position - new Vector3(0,0,cmr.transform.position.z) + new Vector3(0,3f,0);
    }
}
