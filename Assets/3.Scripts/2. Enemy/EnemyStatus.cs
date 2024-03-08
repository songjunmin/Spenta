using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    public float defense;
    public float attackPower;
    public float hp;
    public float maxHp;

    public Image hpBar;

    BoxCollider2D bc;

    public GameObject sparkOfKnowledge;
    public GameObject pieceOfEnlightenment;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // attack type 0 : 일반 / 1 : 스킬
    public void Damaged(float dmg, float coefficient, float attackType)
    {
        float damaged = dmg * coefficient * 100 / (100 + defense);

        hp -= damaged;
        hpBar.fillAmount = hp / maxHp;

        if (attackType == 0)
        {
            GameManager.instance.Player.GetComponent<PlayerStatus>().HpAbsorption(damaged);
        }
        else
        {
            if (GameManager.instance.Player.GetComponent<PlayerStatus>().spentaAbsortion)
            {
                GameManager.instance.Player.GetComponent<PlayerStatus>().HpAbsorption(damaged);
            }
        }

        if (hp <= 0)
        {
            StartCoroutine(Dead());
            Debug.Log("Dead");
        }
        
    }

    IEnumerator Dead()
    {
        transform.GetChild(0).GetComponent<Animator>().enabled = false;
        transform.GetChild(0).SendMessage("Dead");
        
        float timeRemain = 0f;


        string enemyName = gameObject.name;

       
        switch (enemyName)
        {
            case "Asmodeus":
            case "Bear":
                GameObject tmp = Instantiate(pieceOfEnlightenment, transform.position, Quaternion.identity);
                tmp.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4,ForceMode2D.Impulse);
                break;

        }

        while (timeRemain < 1)
        {
            timeRemain += Time.deltaTime;
            if (transform.localScale.x > 0)
            {
                transform.GetChild(0).rotation = Quaternion.Euler(0, 0, -65 * timeRemain);
            }
            else
            {
                transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 65 * timeRemain);
            }
            bc.enabled = false;

            transform.GetChild(0).position = transform.GetChild(0).position + new Vector3(0, -1f * Time.deltaTime, 0);

            yield return null;

        }

        transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
    }
}
