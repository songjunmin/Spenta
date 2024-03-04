using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asmodeus : MonoBehaviour
{
    Animator anim;

    public BoxCollider2D weapon;

    // 0 : 공격 계수 / 1 : 스킬 계수
    public float[] dmg;

    public AnimatorStateInfo animState;

    public bool isPlayer;

    public float attackCoolTime;
    public float attackCurTime;

    public float skillCoolTime;
    public float skillCurTime;

    public int moveDir;
    public float moveSpeed;

    Rigidbody2D rigid;
    float look;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SelectDir();
    }


    void Update()
    {
        // Debug.DrawLine(new Vector2(transform.position.x + 3f * look, transform.position.y + 7f), new Vector2(transform.position.x + 27f * look, transform.position.y),Color.blue);

        skillCurTime -= Time.deltaTime;
        attackCurTime -= Time.deltaTime;

        animState = anim.GetCurrentAnimatorStateInfo(0);

        Move();
        CheckPlayer();

        if (isPlayer && animState.IsName("idle"))
        {
            if (skillCurTime < 0)
            {
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
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
        }
    }

    public void CheckPlayer()
    {
        if (isPlayer)
        {
            return;
        }

        Collider2D[] hits = Physics2D.OverlapAreaAll(new Vector2(transform.position.x -20f, transform.position.y - 12f), new Vector2(transform.position.x + 20f, transform.position.y));

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
        
        if (!animState.IsName("idle"))
        {
            rigid.velocity = Vector2.zero;
        }

        else if (!isPlayer)
        {
            rigid.velocity = new Vector2(moveDir * moveSpeed, rigid.velocity.y);
        }
        else if (GameManager.instance.Player.transform.position.x < transform.position.x - 10f)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
            rigid.velocity = new Vector2(-1 * moveSpeed, rigid.velocity.y);
        }
        else if (GameManager.instance.Player.transform.position.x > transform.position.x + 10f)
        {

            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
    }

    public void SelectDir()
    {
        if (moveDir != 0)
        {
            moveDir = 0;
        }
        else
        {
            int randInt = Random.Range(0, 2);
            if (randInt == 0)
            {
                moveDir--;

            }
            else
            {
                moveDir++;
            }
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * moveDir, transform.localScale.y);
        }

        Invoke("SelectDir", 2f);
    }
    public void Skill()
    {
        look = transform.localScale.x / 4;
        anim.SetTrigger("skill");
    }
    public void SkillDmg()
    {
        Collider2D[] hits = Physics2D.OverlapAreaAll(new Vector2(transform.position.x + 3f * look, transform.position.y + 7f), new Vector2(transform.position.x + 27f * look, transform.position.y));

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.layer == 3)
            {
                hit.GetComponent<PlayerStatus>().Damaged(false, gameObject.GetComponent<EnemyStatus>().attackPower, dmg[1]);


            }
        }
    }
    public void Attack()
    {
        anim.SetTrigger("attack");
    }

    public void AttackOn()
    {
        weapon.enabled = true;
    }

    public void AttackOff()
    {
        weapon.enabled = false;
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
