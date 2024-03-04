using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearHand : MonoBehaviour
{
    // 공격력 / 계수 / 공격타입(0 : 일반 공격 / 1 : 스킬)
    float[] param = new float[3];

    public GameObject hitEffect;
    public Transform hitPlace;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Enemy")
            {
                param[0] = GameManager.instance.Player.GetComponent<PlayerStatus>().attackPower;

                if (GameManager.instance.Player.GetComponentInChildren<PlayerSpine>().animator.GetCurrentAnimatorStateInfo(0).IsName("attack_2"))
                {
                    param[1] = GameManager.instance.Player.GetComponent<PlayerStatus>().skillDmg[0];
                    param[2] = 1f;
                }
                else if (GameManager.instance.Player.GetComponentInChildren<PlayerSpine>().animator.GetCurrentAnimatorStateInfo(0).IsName("attack_1"))
                {
                    param[1] = 1f;
                    param[2] = 0f;
                }
                collision.GetComponent<EnemyStatus>().Damaged(param[0], param[1], param[2]);

                Instantiate(hitEffect, hitPlace.position, Quaternion.Euler(new Vector3(-270, -90, 90)));
            }
        }
    }
}
