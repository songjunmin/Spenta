using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    float[] param = new float[3];

    public GameObject hitEffect;
    public Transform hitPlace;

    // 공격력 / 계수 / 공격타입(0 : 일반 공격 / 1 : 스킬)
    // Start is called before the first frame update
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
            Debug.Log(collision.name);

            if (collision.tag == "Enemy")
            {
                param[0] = GameManager.instance.Player.GetComponent<PlayerStatus>().attackPower;
                param[1] = GameManager.instance.Player.GetComponent<PlayerStatus>().skillDmg[1];
                param[2] = 1;
                collision.GetComponent<EnemyStatus>().Damaged(param[0], param[1], param[2]);

                Instantiate(hitEffect, collision.bounds.ClosestPoint(transform.position), Quaternion.Euler(new Vector3(-270,-90,90)));
            }
        }
    }
}
