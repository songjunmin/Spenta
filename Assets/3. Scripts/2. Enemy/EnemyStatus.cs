using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    public float defense;
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

    int FindOrder(string name)
    {
        switch (name)
        {
            case "First":
                return 0;

            case "Second":
                return 1;

            default:
                return -1;
        }
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
            transform.parent.parent.GetComponent<StageManager>().DeadEnemy(FindOrder(transform.parent.name));
            Debug.Log("Dead");
        }
        
    }

    IEnumerator Dead()
    {
        GameManager.instance.Player.GetComponent<PlayerStatus>().EnemyDead();

        transform.GetChild(0).SendMessage("Dead");

        yield return null;
        yield return null;
        transform.GetChild(0).GetComponent<Animator>().enabled = false;
        transform.GetChild(0).GetComponent<MeshRenderer>().sortingOrder = -2;
        
        float timeRemain = 0f;


        string enemyName = gameObject.name;

        // 스킬 초기화를 위해 Game Manager의 함수 호출
        GameManager.instance.EnemyDead();
       
        switch (enemyName)
        {
            case "Asmodeus":
            case "Bear":
            case "Frog":
            case "Zombie":
                GameObject tmp = Instantiate(pieceOfEnlightenment, transform.position, Quaternion.identity);
                tmp.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4,ForceMode2D.Impulse);
                break;

        }

        while (timeRemain < 1)
        {
            timeRemain += Time.deltaTime * 2;
            if (transform.localScale.x > 0)
            {
                transform.GetChild(0).rotation = Quaternion.Euler(0, 0, -65 * timeRemain);
            }
            else
            {
                transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 65 * timeRemain);
            }
            bc.enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            if (timeRemain < 0.5f)
            {
                transform.GetChild(0).position = transform.GetChild(0).position + new Vector3(0, 3 * Time.deltaTime, 0);
            }
            
            else
            {
                transform.GetChild(0).position = transform.GetChild(0).position + new Vector3(0, -6f * Time.deltaTime, 0);
            }

            yield return null;

        }

        transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
    }
}
