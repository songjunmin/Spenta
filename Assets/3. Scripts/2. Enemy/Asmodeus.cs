using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asmodeus : MonoBehaviour
{
    Animator anim;

    public BoxCollider2D weapon;

    // 0 : 공격 계수 / 1 : 스킬 계수
    public float[] dmg;
    public float attackPower;

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
    public bool isDead;

    public GameObject[] hearts;

    void Start()
    {
        rigid = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SelectDir();
    }


    void Update()
    {
        
        if (isDead)
        {
            return;
        }

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
                if (GameManager.instance.Player.transform.position.x < transform.position.x + 5f &&
                    GameManager.instance.Player.transform.position.x > transform.position.x - 5f)
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

        Collider2D[] hits = Physics2D.OverlapAreaAll(new Vector2(transform.position.x -10f, transform.position.y - 6f), new Vector2(transform.position.x + 10f, transform.position.y));

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
        // 다른 동작 중이라면
        if (!animState.IsName("idle"))
        {
            rigid.velocity = Vector2.zero;
        }
        // 아직 플레이어를 찾지 못했다면
        else if (!isPlayer)
        {
            rigid.velocity = new Vector2(moveDir * moveSpeed, rigid.velocity.y);
        }
        // 만약 플레이어를 찾았고, 플레이어와의 거리가 멀다면
        else if (GameManager.instance.Player.transform.position.x < transform.position.x - 5f)
        {
            // transform.parent.localScale = new Vector2(Mathf.Abs(transform.parent.localScale.x) * -1, transform.parent.localScale.y);
            // rigid.velocity = new Vector2(-1 * moveSpeed, rigid.velocity.y);
        }
        else if (GameManager.instance.Player.transform.position.x > transform.position.x + 5f)
        {
            // transform.parent.localScale = new Vector2(Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y);
            // rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
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
            transform.parent.localScale = new Vector2(Mathf.Abs(transform.parent.localScale.x) * moveDir, transform.parent.localScale.y);
        }

        Invoke("SelectDir", 2f);
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

        look = transform.parent.localScale.x / 2;
        anim.SetTrigger("skill");
    }
    public void SkillDmg()
    {
        GameManager.instance.Player.GetComponent<PlayerStatus>().Damaged(false,attackPower, dmg[1] , transform.position.x);
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

    public void SetHeartLoc()
    {
        Vector3 loc = GameManager.instance.Player.transform.position + new Vector3(0,2,0);
        hearts[0].transform.position = loc;
        hearts[1].transform.position = loc;  

        if ((loc.x - transform.position.x) * transform.parent.localScale.x < 0)
        {
            StartCoroutine(SetHeartSize(false));
        }
        else
        {
            StartCoroutine(SetHeartSize(true));
        }
    }

    IEnumerator SetHeartSize(bool canStart)
    {
        if (canStart)
        {
            float flowedTime = 0f;

            while (flowedTime < 1)
            {
                flowedTime += Time.deltaTime;
                hearts[0].transform.localScale = new Vector3(flowedTime * 3, flowedTime * 3, 0);
                hearts[1].transform.localScale = new Vector3(flowedTime * 3, flowedTime * 3, 0);
                hearts[2].transform.localScale = new Vector3(flowedTime * 3, flowedTime * 3, 0);
                yield return null;
            }
        }
        else
        {
            hearts[0].transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void CheckPlayerInHeart()
    {
        if (hearts[0].transform.localScale.x == 0)
        {
            return;
        }
        hearts[0].transform.localScale = new Vector3(0, 0, 0);
        hearts[1].transform.localScale = new Vector3(0, 0, 0);
        hearts[2].transform.localScale = new Vector3(0, 0, 0);

        Vector3 targetLoc;
        if (transform.parent.localScale.x > 0)
        {
            targetLoc = hearts[0].transform.position + new Vector3(0.7f, -0.6f, 0f);
        }
        else
        {
            targetLoc = hearts[0].transform.position + new Vector3(0.7f, 0.6f, 0f);
        }

        Collider2D[] hits = Physics2D.OverlapBoxAll(targetLoc, new Vector2(2.5f, 1.7f), 0);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.tag == "Player")
            {
                Debug.Log("피격");
                GameManager.instance.GetAbnormal(GameManager.AbnormalStatus.둔화, 10f);
                // SkillDmg();
                return;
            }
        }

        Vector3 loc = GameManager.instance.Player.transform.position + new Vector3(0, 2, 0);

    }
}
