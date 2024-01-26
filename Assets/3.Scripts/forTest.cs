using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

public class forTest : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    
    public SkeletonData skeletonData;
    public AnimationReferenceAsset[] AnimClip;

    public enum AnimState
    {
        Block,Blow,Knock,Lion,Run,Shot,Stand
    }

    // 현재 처리해야될 애니메이션이 무엇인지
    private AnimState _AnimState;

    // 현재 나오고 있는 애니메이션이 무엇인지
    private string CurrentAnimation;

    public Rigidbody2D rigid;

    // 현재 x축 속도
    private flaot xSpeed;
    
    public TrackEntry entry;

    public string animName;

    public string nowState;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        nowState = "Stand";

    }
    // Block    Blow    Knock    Lion    Run    Shot    Stand
    // Update is called once per frame
    void Update()
    {
        entry = skeletonAnimation.state.GetCurrent(0);
        animName = (entry == null ? null : entry.Animation.Name);

        AnimCntl();

        xSpeed = Input.GetAxisRaw("Horizontal");

        if (xSpeed == 0)
        {
            _AnimState = AnimState.Stand;

            rigid.velocity = new Vector2(xSpeed, rigid.velocity.y);
            
        }
        else
        {
            _AnimState = AnimState.Run;
            transform.localScale = new Vector2(xSpeed, 1);
        }
    }

    void ResetState()
    {
        nowState = "Stand";
    }

    
    void AnimCntl()
    {
        if (string.Compare("Stand",animName) != 0 && string.Compare("Run", animName) != 0)
        {
            if (nowState == "Stand")
            {
                entry.mixDuration = 3.6f;
                skeletonAnimation.state.SetAnimation(0, "Stand", true);
                entry.mixDuration = 3.6f;
            }
        }
    }
    
    void Test()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            skeletonAnimation.state.SetAnimation(0, "Block", false);
            nowState = "Block";
            Invoke("ResetState", 1.5f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skeletonAnimation.state.SetAnimation(0, "Blow", false);
            nowState = "Blow";
            Invoke("ResetState", 4.9f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skeletonAnimation.state.SetAnimation(0, "Knock", false);
            Invoke("ResetState", 3.67f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            skeletonAnimation.state.SetAnimation(0, "Lion", false);
            Invoke("ResetState", 2.67f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            skeletonAnimation.state.SetAnimation(0, "Run", true);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            skeletonAnimation.state.SetAnimation(0, "Shot", false);
            Invoke("ResetState", 3.74f);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            skeletonAnimation.state.SetAnimation(0, "Stand", true);

        }
    }
}


