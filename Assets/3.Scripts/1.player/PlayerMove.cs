using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using System.Diagnostics;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float moveDir;
    public float jumpPower;
    public int isJump;
    public float boxSizeX;
    public float boxSizeY;

    public Rigidbody2D rigid;
    BoxCollider2D bc;

    public RaycastHit2D hit;


    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        boxSizeX = bc.size.x;
        boxSizeY = bc.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        // 움직임
        Move();

        // 점프
        Jump();

        // Test
        UnityEngine.Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.01f), Vector2.down, Color.red);

    }

    void Move()
    {
        if (gameObject.GetComponentInChildren<PlayerSpine>().animName != "Run" && gameObject.GetComponentInChildren<PlayerSpine>().animName != "Stand")
        {
            return;
        }

        moveDir = Input.GetAxisRaw("Horizontal");

        if (moveDir != 0)
        {
            rigid.velocity = new Vector2(moveDir * speed, rigid.velocity.y);
            transform.localScale = new Vector3(moveDir, 1f, 1f);
        }
        else
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
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
        if (gameObject.GetComponentInChildren<PlayerSpine>().animName != "Run" && gameObject.GetComponentInChildren<PlayerSpine>().animName != "Stand")
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            // 아래키를 누르고있으면 아래점프
            if (Input.GetButton("Down"))
            {
                DownJump();
                return;
            }

            if (isJump == 0)
            {
                isJump = 1;
                // 점프 전 y축 속도 초기화
                rigid.velocity = new Vector2(rigid.velocity.x, 0);

                // 점프
                rigid.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            }
        }
    }
}
