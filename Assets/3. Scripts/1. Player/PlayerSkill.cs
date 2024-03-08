using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public Animator animator;
    
    PlayerStatus playerStatus;
    PlayerMove playerMove;

    public float dashSpeed;

    public GameObject ashaFire;

    private void Start()
    {
        playerStatus = gameObject.GetComponentInParent<PlayerStatus>();
        playerMove = gameObject.GetComponentInParent<PlayerMove>();
    }
    public void Action(PlayerAction.NonSkillName nonSkillName)
    {
        switch (nonSkillName)
        {
            case PlayerAction.NonSkillName.Dash:
                break;

            case PlayerAction.NonSkillName.Parrying:

                break;
        }
    }

    public void Action(PlayerAction.SkillName skillName)
    {
        switch (skillName)
        {
            case PlayerAction.SkillName.Asha:
                
                break;

            default:
                break;
        }
    }

    public void DashOn()
    {
        playerMove.ChangeVelocity(new Vector2(dashSpeed * playerMove.lookDir, 0));
    }

    public void DashOff()
    {
        playerMove.ChangeVelocity(new Vector2(0, 0));
    }

    public void AshaFire()
    {
        Instantiate(ashaFire, transform.position + new Vector3(0f,7f,0f), Quaternion.identity);
    }
}
