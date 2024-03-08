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
    float look;
    public bool isDead;

    void Start()
    {
        rigid = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        // Debug.DrawLine(new Vector2(transform.position.x + 3f * look, transform.position.y + 7f), new Vector2(transform.position.x + 27f * look, transform.position.y),Color.blue);

        skillCurTime -= Time.deltaTime;
        attackCurTime -= Time.deltaTime;
        moveCurTime -= Time.deltaTime;

        animState = anim.GetCurrentAnimatorStateInfo(0);

        CheckPlayer();

        if (isPlayer && animState.IsName("idle"))
        {
            

            if (Mathf.Abs(GameManager.instance.Player.transform.position.x - transform.position.x) > 5f && moveCurTime < 0)
            {
                Move();
                moveCurTime = moveCoolTime;
            }
            else if (skillCurTime < 0)
            {
                Debug.Log(1);

                Skill();
                skillCurTime = skillCoolTime;
            }

            else if (attackCurTime < 0)
            {
                if (GameManager.instance.Player.transform.position.x < transform.position.x + 10f &&
                    GameManager.instance.Player.transform.position.x > transform.position.x - 10f)
                {
                    Attack();
                    attackCurTime = attackCoolTime;
                }

            }
            

        }
        else if (isPlayer)
        {
            if (GameManager.instance.Player.transform.position.x < transform.position.x)
            {
                transform.parent.localScale = new Vector2(Mathf.Abs(transform.parent.localScale.x) * -1, transform.parent.localScale.y);
            }
            else
            {
                transform.parent.localScale = new Vector2(Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y);
            }
        }
    }


    public void Dead()
    {
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
        look = transform.parent.localScale.x / 4;
        anim.SetTrigger("skill");
    }
    public void SkillDmg()
    {
        GameManager.instance.Player.GetComponent<PlayerStatus>().Damaged(false, gameObject.GetComponentInParent<EnemyStatus>().attackPower, dmg[1]);
    }
    public void Attack()
    {
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

}
