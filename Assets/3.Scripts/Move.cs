using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class Move : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public bool isJump;
    public float boxSizeX;
    public float boxSizeY;
    public float offsetX;
    public float offsetY;


    public Rigidbody2D rigid;
    BoxCollider2D bc;

    public RaycastHit2D hit;

    public SkeletonAnimation skeletonAnimation;

    

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        boxSizeX = bc.size.x;
        boxSizeY = bc.size.y;

        offsetX = bc.offset.x;
        offsetY = bc.offset.y;
    }

    // Update is called once per frame
    void Update()
    {
        TrackEntry entry = skeletonAnimation.state.GetCurrent(0);
        string animName = (entry == null ? null : entry.Animation.Name);

        if (Input.GetAxisRaw("Horizontal") < 0 )
        {
            rigid.velocity = new Vector2(speed * -1, rigid.velocity.y);

            if (string.Compare("Run", animName) != 0)
            {
                skeletonAnimation.state.SetAnimation(0, "Run", true);
            }

            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            rigid.velocity = new Vector2(speed, rigid.velocity.y);

            if (string.Compare("Run", animName) != 0)
            {
                skeletonAnimation.state.SetAnimation(0, "Run", true);
            }

            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);

            if (string.Compare("Run", animName) == 0)
            {
                skeletonAnimation.state.SetAnimation(0, "Stand", true);
            }
        }

        // ����
        Jump();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(6))
        {
            isJump = false;
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
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - boxSizeY / 2 - 0.01f), Vector2.down, 0.01f);

        if (hit && hit.collider.gameObject.CompareTag("UpTile"))
        {
            hit.transform.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    void Jump()
    {
        // �Ʒ�Ű�� ������������ �Ʒ�����
        if (Input.GetButton("Down"))
        {
            DownJump();
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                isJump = true;
                // ���� �� y�� �ӵ� �ʱ�ȭ
                rigid.velocity = new Vector2(rigid.velocity.x, 0);

                // ����
                rigid.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            }
        }
    }
}
