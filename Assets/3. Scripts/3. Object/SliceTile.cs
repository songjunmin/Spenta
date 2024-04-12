using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceTile : MonoBehaviour
{

    PlatformEffector2D pe;

    private void Start()
    {
        pe = GetComponent<PlatformEffector2D>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ( pe.surfaceArc == 0 )
        {
            pe.surfaceArc = 180;
        }
    }
}
