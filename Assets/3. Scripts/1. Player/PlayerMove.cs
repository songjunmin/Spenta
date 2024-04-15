using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class PlayerMove : MonoBehaviour
{
    // 이동 속도
    public float speed;

    public float moveDir;
    public float lookDir;
    public float jumpPower;
    public int isJump;
    public float boxSizeX;
    public float boxSizeY;
    public bool isHit; // 피격판정 중인지 판단


    public Rigidbody2D rigid;
    BoxCollider2D bc;

    public RaycastHit2D hit;

    Animator animator;
    public AnimatorStateInfo animState;
    public string animName;



    Vector2 dashSpeed;

    Collider2D[] isGround1;
    Collider2D[] isGround2;



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + new Vector3(0.5f,0,0), 0.3f);
        Gizmos.DrawSphere(transform.position + new Vector3(-0.5f, 0, 0), 0.3f);
    }

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        boxSizeX = bc.size.x;
        boxSizeY = bc.size.y;

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animState = animator.GetCurrentAnimatorStateInfo(0);
        animName = animState.shortNameHash.ToString();

        if (isHit)
        {
            return;
        }

        // 움직임
        Move();

        // 점프
        Jump();
        
        // Test
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.01f), Vector2.down, Color.red);

        // 대쉬
        Dash();
        
    }

    private void FixedUpdate()
    {
        IsGround();
    }

    void Move()
    {
        if (!animState.IsName("Stand") && !animState.IsName("Run") && !animState.IsName("Jump"))
        {
            return;
        }

        moveDir = Input.GetAxisRaw("Horizontal");

        if (moveDir != 0)
        {
            rigid.velocity = new Vector2(moveDir * speed, rigid.velocity.y);
            transform.localScale = new Vector3(moveDir, 1f, 1f);
            lookDir = moveDir;
        }
        else
        {
            // rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("UpTile"))
        {
            collision.isTrigger = false;
        }
    }

    void DownJump()
    {
        foreach (Collider2D collider in isGround1)
        {
            if (collider.gameObject.layer == 6)
            {
                if (collider.gameObject.CompareTag("UpTile"))
                {
                    collider.GetComponent<PlatformEffector2D>().surfaceArc = 0f;
                }
                    return;
            }
        }

        foreach (Collider2D collider in isGround2)
        {
            if (collider.gameObject.layer == 6)
            {
                if (collider.gameObject.CompareTag("UpTile"))
                {
                    collider.GetComponent<PlatformEffector2D>().surfaceArc = 0f;
                }
                return;
            }
        }
        
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            // 아래키를 누르고있으면 아래점프
            if (Input.GetButton("Down"))
            {
                DownJump();
                return;
            }

            if (isJump < 2)
            {
                isJump++;
                // 점프 전 y축 속도 초기화
                rigid.velocity = new Vector2(rigid.velocity.x, 0);

                // 점프
                rigid.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            }
        }
    }
    public void IsGround()
    {
    
        if (rigid.velocity.y > 0.01f)
        {
            return;
        }

        isGround1 = Physics2D.OverlapCircleAll(transform.position + new Vector3(0.5f, 0, 0), 0.3f);
        isGround2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(-0.5f, 0, 0), 0.3f);
        foreach (Collider2D collider in isGround1)
        {
            if (collider.gameObject.layer == 6)
            {
                isJump = 0;
                return;
            }
        }

        foreach (Collider2D collider in isGround2)
        {
            if (collider.gameObject.layer == 6)
            {
                isJump = 0;
                return;
            }
        }

    }

    public void ChangeVelocity(Vector2 vector2)
    {
        dashSpeed = vector2;
    }

    void Dash()
    {
        if (animState.IsName("Lion"))
        {
            rigid.velocity = dashSpeed;
        }
    }

    public void HitOn()
    {
        isHit = true;
        rigid.velocity = Vector3.zero;
        Invoke("HitOff", 0.3f);
    }

    public void HitOff()
    {
        isHit = false;
    }
}
