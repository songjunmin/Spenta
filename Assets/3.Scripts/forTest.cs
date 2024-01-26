using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

public class forTest : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Block    Blow    Knock    Lion    Run    Shot    Stand
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            skeletonAnimation.state.SetAnimation(0, "Block", false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skeletonAnimation.state.SetAnimation(0, "Blow", false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skeletonAnimation.state.SetAnimation(0, "Knock", false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            skeletonAnimation.state.SetAnimation(0, "Lion", false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            skeletonAnimation.state.SetAnimation(0, "Run", true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            skeletonAnimation.state.SetAnimation(0, "Shot", false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            skeletonAnimation.state.SetAnimation(0, "Stand", true);
        }
    }
}
