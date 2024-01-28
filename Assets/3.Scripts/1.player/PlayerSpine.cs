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

    // ���� ó���ؾߵ� �ִϸ��̼��� ��������
    private AnimState _AnimState;

    // ���� ������ �ִ� �ִϸ��̼��� ��������
    private string CurrentAnimation;

    public TrackEntry entry;

    public string animName;

    public string nowState;
    public float moveDir;

    void Start()
    {
        nowState = "Stand";

    }

    // Update is called once per frame
    void Update()
    {
        SpineMng();   
    }

    void SpineMng()
    {
        entry = skeletonAnimation.state.GetCurrent(0);
        animName = entry == null ? null : entry.Animation.Name;

        moveDir = GameManager.instance.Player.GetComponent<PlayerMove>().moveDir;

        if (moveDir == 0)
        {
            _AnimState = AnimState.Stand;
        }
        else
        {
            _AnimState = AnimState.Run;
        }

        SetCurrentAnimation(_AnimState);
    }


    void _ASncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale)
    {
        // ���� �̹� ������� �ִϸ��̼��� ���
        if (animClip.name.Equals(CurrentAnimation))
        {
            return;
        }

        // �ش� �ִϸ��̼����� ����
        skeletonAnimation.state.SetAnimation(0, animClip, loop).timeScale = timeScale;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;

        // ���� �ִϸ��̼� ���� ����
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
