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

    public Animator animator;

    PlayerMove playerMove;
    public enum AnimState
    {
        Block, Blow, Knock, Lion, Run, Shot, Stand
    }

    
    public bool isMove;

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



        }
    }
    public void Action(PlayerAction.NonSkillName nonSkillName)
    {
        switch (nonSkillName)
        {
            case PlayerAction.NonSkillName.Block:
                animator.SetTrigger("block");
                break;

        }
    }

                   


    void _ASncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale)
    {

    }

    void SetCurrentAnimation(AnimState _state)
    {
       
    }
}
