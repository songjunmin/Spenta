using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    PlayerStatus playerStatus;
    PlayerMove playerMove;

    public float flashRange;
    public float dashSpeed;

    public GameObject ashaFire;


    private void Start()
    {
        playerStatus = GetComponentInParent<PlayerStatus>();
        playerMove = GetComponentInParent<PlayerMove>();
    }
    public void Action(PlayerAction.NonSkillName nonSkillName)
    {
        switch (nonSkillName)
        {
            case PlayerAction.NonSkillName.Dash:
                if (transform.parent.GetComponent<PlayerStatus>().nonSkillCanUse[0])
                {
                    /*
                    Vector2 startVec = transform.position + new Vector3(0, 0.5f);
                    RaycastHit2D hit = Physics2D.Raycast(startVec, new Vector2(transform.parent.localScale.x, 0), flashRange, 6);

                    if (!hit)
                    {
                        transform.parent.position += new Vector3(flashRange * transform.parent.localScale.x, 0, 0);
                    }
                    */


                }
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
        StartCoroutine(Dash());
    }
    IEnumerator Dash()
    {
        float remainTime = 0.12f;
        while(remainTime > 0)
        {

            Vector3 velocity = transform.parent.GetComponent<Rigidbody2D>().velocity;
            transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.parent.localScale.x * dashSpeed, velocity.y);

            remainTime -= Time.deltaTime;
            yield return null;
        }

        transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, transform.parent.GetComponent<Rigidbody2D>().velocity.y);

    }

    public void AshaFire()
    {
        Instantiate(ashaFire, transform.position + new Vector3(0f,3.5f,0f), Quaternion.identity);
    }
}
