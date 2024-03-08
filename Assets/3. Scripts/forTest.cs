using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

public class forTest : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    
    public SkeletonData skeletonData;
    public AnimationReferenceAsset[] AnimClip;

    public float speed;


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
    private float xSpeed;
    
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
        }
        else
        {
            _AnimState = AnimState.Run;
            transform.localScale = new Vector2(xSpeed, 1);
        }

        SetCurrentAnimation(_AnimState);

    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(xSpeed * speed * Time.deltaTime, rigid.velocity.y);
    }


    

    
    void _ASncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale)
    {
        // 만약 이미 재생중인 애니메이션일 경우
        if (animClip.name.Equals(CurrentAnimation))
        {
            return;
        }

        // 해당 애니메이션으로 변경
        // skeletonAnimation.state.SetAnimation(0, animClip, loop).timeScale = timeScale;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;

        // 현재 애니메이션 값을 변경
        CurrentAnimation = animClip.name;
    }

    void SetCurrentAnimation(AnimState _state)
    {
        switch (_state)
        {
            case AnimState.Stand:
                _ASncAnimation(AnimClip[(int)AnimState.Stand], true, 1f);
                break;
            case AnimState.Run:
                _ASncAnimation(AnimClip[(int)AnimState.Run], true, 1f);
                break;



        }


    }
    void AnimCntl()
    {
        if (string.Compare("Stand", animName) != 0 && string.Compare("Run", animName) != 0)
        {
            if (nowState == "Stand")
            {
                //entry.mixDuration = 3.6f;
                skeletonAnimation.state.SetAnimation(0, "Stand", true);
                //entry.mixDuration = 3.6f;
            }
        }
    }
}


