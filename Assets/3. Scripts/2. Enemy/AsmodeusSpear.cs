using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsmodeusSpear : MonoBehaviour
{
    float[] param = new float[2];

    public EnemyStatus es;
    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Player")
            {
                Asmodeus asmodeus = es.gameObject.GetComponentInChildren<Asmodeus>();
                param[0] = asmodeus.attackPower;
                param[1] = asmodeus.dmg[asmodeus.GetNowAnim()];
                collision.GetComponent<PlayerStatus>().Damaged(false,param[0], param[1], transform.position.x);
            }
        }
    }
}
