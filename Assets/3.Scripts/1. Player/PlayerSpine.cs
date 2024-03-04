using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class PlayerSpine : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

    public Spine.AnimationState state;

    public SkeletonData skeletonData;
    public AnimationReferenceAsset[] AnimClip;

    public Animator animator;

    PlayerMove playerMove;

    public BoxCollider2D spearBoxCollider;
    public BoxCollider2D spearHandBoxCollider;
    public enum AnimState
    {
        Block, Blow, Knock, Lion, Run, Shot, Stand
    }

    
    public bool isMove;
    public bool isJump;

    void Start()
    {
        playerMove = gameObject.GetComponentInParent<PlayerMove>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerMove.moveDir == 0)
        {
            isMove = false;
        }
        else
        {
            isMove = true;
        }

        animator.SetBool("isMove", isMove);

        if (gameObject.GetComponentInParent<PlayerMove>().isJump == 0)
        {
            isJump = false;
        }
        else
        {
            isJump = true;
        }
        animator.SetBool("isJump", isJump);
    }

    void SpineMng()
    {
        
    }

    public void Action(PlayerAction.SkillName skillName)
    {
       switch (skillName)
        {
            case PlayerAction.SkillName.Asha:
                animator.SetTrigger("asha");
                break;

            case PlayerAction.SkillName.Cassatra:
                animator.SetTrigger("cassatra");
                break;

            case PlayerAction.SkillName.Bohuman:
                animator.SetTrigger("bohuman");
                break;


        }
    }
    public void Action(PlayerAction.NonSkillName nonSkillName)
    {
        switch (nonSkillName)
        {
            case PlayerAction.NonSkillName.Parrying:
                animator.SetTrigger("parrying");
                break;

            case PlayerAction.NonSkillName.Attack:
                animator.SetTrigger("attack");
                break;

            case PlayerAction.NonSkillName.Dash:
                animator.SetTrigger("dash");
                break;

        }
    }

                   


    void _ASncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale)
    {

    }

    void SetCurrentAnimation(AnimState _state)
    {
       
    }

    public void OnSpearBoxCollider()
    {
        spearBoxCollider.enabled = true;
    }

    public void OffSpearBoxCollider()
    {
        spearBoxCollider.enabled = false;
    }
    public void OnSpearHandBoxCollider()
    {
        spearHandBoxCollider.enabled = true;
    }

    public void OffSpearHandBoxCollider()
    {
        spearHandBoxCollider.enabled = false;
    }
}
