using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flog : MonoBehaviour
{
    Animator anim;

    // 0 : 공격 계수 / 1 : 스킬 계수
    public float[] dmg;
    public float attackPower;

    public AnimatorStateInfo animState;

    public bool isPlayer;

    public float attackCoolTime;
    public float attackCurTime;

    public float skillCoolTime;
    public float skillCurTime;
    
    public float moveCoolTime;
    public float moveCurTime;

    public int moveDir;
    public float moveSpeed;

    Rigidbody2D rigid;
    public bool isDead;

    public GameObject poison;

    public float distance;

    void Start()
    {
        rigid = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }



    public float startX, startY, lenX, lenY;
    private void OnDrawGizmos()
    {
        // 공격 사거리 11
        // 스킬 사거리 23
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(new Vector3(startX * transform.parent.localScale.x, startY, 0) + transform.position, new Vector3(lenX, lenY, 0));
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }

        if (!animState.IsName("walking"))
        {
            rigid.velocity = Vector2.zero;
        }

        skillCurTime -= Time.deltaTime;
        attackCurTime -= Time.deltaTime;
        moveCurTime -= Time.deltaTime;

        animState = anim.GetCurrentAnimatorStateInfo(0);

        CheckPlayer();

        distance = Mathf.Abs(GameManager.instance.Player.transform.position.x - transform.position.x);
        if (GameManager.instance.Player.transform.position.x > transform.position.x)
        {
            moveDir = 1;
        }
        else
        {
            moveDir = -1;
        }


        if (isPlayer && animState.IsName("idle"))
        {
            
            if (distance < 18 && skillCurTime < 0)
            {
                Skill();
                skillCurTime = skillCoolTime;
            }

            else if (distance > 7f && moveCurTime < 0)
            {
                Move();
                moveCurTime = moveCoolTime;
            }

            else if (distance < 7 && attackCurTime < 0)
            {
                if (GameManager.instance.Player.transform.position.x < transform.position.x + 10f &&
                    GameManager.instance.Player.transform.position.x > transform.position.x - 10f)
                {
                    Attack();
                    attackCurTime = attackCoolTime;
                }
            }
        }
    }


    public void Dead()
    {
        anim.SetTrigger("dead");
        rigid.velocity = Vector3.zero;
        transform.parent.GetChild(1).GetChild(0).gameObject.SetActive(false);
        isDead = true;
    }
    public void CheckPlayer()
    {
        if (isPlayer)
        {
            return;
        }

        Collider2D[] hits = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - 20f, transform.position.y - 12f), new Vector2(transform.position.x + 20f, transform.position.y));

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.layer == 3)
            {
                isPlayer = true;
                skillCurTime = skillCoolTime;
                CancelInvoke();
            }
        }
    }

    public void Move()
    {
        if (GameManager.instance.Player.transform.position.x < transform.position.x)
        {
            transform.parent.localScale = new Vector2(Mathf.Abs(transform.parent.localScale.x) * -1, transform.parent.localScale.y);
        }
        else
        {
            transform.parent.localScale = new Vector2(Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y);
        }
        anim.SetTrigger("move");
    }

    public void MoveOn()
    {
        rigid.velocity = new Vector2(moveSpeed * moveDir, rigid.velocity.y);
    }

    public void MoveOff()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
    }

    public void Skill()
    {
        if (GameManager.instance.Player.transform.position.x < transform.position.x)
        {
            transform.parent.localScale = new Vector2(Mathf.Abs(transform.parent.localScale.x) * -1, transform.parent.localScale.y);
        }
        else
        {
            transform.parent.localScale = new Vector2(Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y);
        }
        anim.SetTrigger("skill");
    }
    public void SkillDmg()
    {
        float dmgs = dmg[1] * (23 - distance) / 4;
        GameManager.instance.Player.GetComponent<PlayerStatus>().Damaged(false,attackPower, dmgs);
    }
    public void Attack()
    {
        if (GameManager.instance.Player.transform.position.x < transform.position.x)
        {
            transform.parent.localScale = new Vector2(Mathf.Abs(transform.parent.localScale.x) * -1, transform.parent.localScale.y);
        }
        else
        {
            transform.parent.localScale = new Vector2(Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y);
        }
        anim.SetTrigger("attack");
    }

    public int GetNowAnim()
    {
        if (animState.IsName("attack"))
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }


    public void SetPoisonLoc()
    {
        Vector3 loc = GameManager.instance.Player.transform.position + new Vector3(0, 4, 0);
        poison.transform.position = loc;

        
        if ((loc.x - transform.position.x) * transform.parent.localScale.x < 0)
        {
            poison.GetComponent<SkeletonUtilityBone>().scale = true;
        }
        else
        {
            poison.GetComponent<SkeletonUtilityBone>().scale = false;
        }
    }

    public void CheckPlayerLoc()
    {
        if (poison.GetComponent<SkeletonUtilityBone>().scale)
        {
            return;
        }

        GetComponent<MeshRenderer>().sortingOrder = 3;

        if (Mathf.Abs(poison.transform.position.x - GameManager.instance.Player.transform.position.x) < 3)
        {
            Vector3 loc = GameManager.instance.Player.transform.position + new Vector3(0, 4, 0);
            poison.transform.position = loc;

            GameManager.instance.Player.GetComponent<PlayerStatus>().Damaged(false, attackPower, dmg[0]);

        }
        else
        {
            poison.GetComponent<SkeletonUtilityBone>().scale = false;
        }
    }

    public void OffLayer()
    {
        GetComponent<MeshRenderer>().sortingOrder = 0;
    }
}
