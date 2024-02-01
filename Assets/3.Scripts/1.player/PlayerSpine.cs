using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class PlayerSpine : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

    public SkeletonData skeletonData;
    public AnimationReferenceAsset[] AnimClip;
    public enum AnimState
    {
        Block, Blow, Knock, Lion, Run, Shot, Stand
    }

    // 현재 처리해야될 애니메이션이 무엇인지
    private AnimState _AnimState;

    // 현재 나오고 있는 애니메이션이 무엇인지
    private string CurrentAnimation;

    public TrackEntry entry;

    public string animName;

    public float moveDir;

    void Start()
    {
        skeletonAnimation.AnimationState.Complete += delegate (TrackEntry trackEntry)
        {
            if (trackEntry.animation.name != "Run" && trackEntry.animation.name != "Stand")
            {
                _AnimState = AnimState.Stand;
                SetCurrentAnimation(_AnimState);
            }
        };

        entry = skeletonAnimation.state.GetCurrent(0);
        entry.mixDuration = 0.6f;

    }

    // Update is called once per frame
    void Update()
    {
        entry = skeletonAnimation.state.GetCurrent(0);
        animName = entry == null ? null : entry.Animation.Name;

        SpineMng();
    }

    void SpineMng()
    {
        // 아무것도 하지 않고 있을 경우
        if (animName == "Stand" || animName == "Run")
        {
            moveDir = GameManager.instance.Player.GetComponent<PlayerMove>().moveDir;

            if (moveDir == 0)
            {
                _AnimState = AnimState.Stand;
            }
            else
            {
                _AnimState = AnimState.Run;
            }

            
        }

        // 애니메이션 실행
        SetCurrentAnimation(_AnimState);
    }

    public void SetAnimState(PlayerAction.SkillName skillName)
    {
        switch(skillName)
        {
            case PlayerAction.SkillName.Bohuman:
                _AnimState = AnimState.Lion;
                break;

            case PlayerAction.SkillName.Asha:
                _AnimState = AnimState.Knock;
                break;

            case PlayerAction.SkillName.Cassatra:
                _AnimState = AnimState.Shot;
                break;

            default:
                break;
        }
        SetCurrentAnimation(_AnimState);
    }


    void _ASncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale)
    {
        // 만약 이미 재생중인 애니메이션일 경우
        if (animClip.name.Equals(CurrentAnimation))
        {
            return;
        }

        // 해당 애니메이션으로 변경
        skeletonAnimation.state.SetAnimation(0, animClip, loop).timeScale = timeScale;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;

        // 현재 애니메이션 값을 변경
        CurrentAnimation = animClip.name;
    }

    void SetCurrentAnimation(AnimState _state)
    {
        // Block, Blow, Knock, Lion, Run, Shot, Stand
        switch (_state)
        {
            case AnimState.Block:
                _ASncAnimation(AnimClip[(int)AnimState.Block], false, 1f);
                break;

            case AnimState.Blow:
                _ASncAnimation(AnimClip[(int)AnimState.Blow], false, 1f);
                break;

            case AnimState.Knock:
                _ASncAnimation(AnimClip[(int)AnimState.Knock], false, 1f);
                break;

            case AnimState.Lion:
                _ASncAnimation(AnimClip[(int)AnimState.Lion], false, 1f);
                break;

            case AnimState.Run:
                _ASncAnimation(AnimClip[(int)AnimState.Run], true, 1f);
                break;

            case AnimState.Shot:
                _ASncAnimation(AnimClip[(int)AnimState.Shot], false, 1f);
                break;

            case AnimState.Stand:
                _ASncAnimation(AnimClip[(int)AnimState.Stand], true, 1f);
                break;
            
        }
    }
}
