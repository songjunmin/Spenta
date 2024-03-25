using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using System.Diagnostics;

public class PlayerMove : MonoBehaviour
{
    // �̵� �ӵ�
    public float speed;


    public float moveDir;
    public float lookDir;
    public float jumpPower;
    public int isJump;
    public float boxSizeX;
    public float boxSizeY;

    public Rigidbody2D rigid;
    BoxCollider2D bc;

    public RaycastHit2D hit;

    Animator animator;
    public AnimatorStateInfo animState;
    public string animName;

    Vector2 dashSpeed;

    // Start is called before the first frame update
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

        // ������
        Move();

        // ����
        Jump();

        // Test
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.01f), Vector2.down, Color.red);

        // �뽬
        Dash();
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(6))
        {
            isJump = 0;
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
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y  - 0.05f), Vector2.down, 0.01f);

        if (hit && hit.collider.gameObject.CompareTag("UpTile"))
        {
            hit.transform.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            // �Ʒ�Ű�� ������������ �Ʒ�����
            if (Input.GetButton("Down"))
            {
                DownJump();
                return;
            }

            if (isJump < 2)
            {
                isJump++;
                // ���� �� y�� �ӵ� �ʱ�ȭ
                rigid.velocity = new Vector2(rigid.velocity.x, 0);

                // ����
                rigid.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
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
}
