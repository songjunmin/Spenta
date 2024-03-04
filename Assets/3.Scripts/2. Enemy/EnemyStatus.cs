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

    // Start is called before the first frame update
    void Start()
    {

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
        float timeRemain = 1f;
        while (timeRemain > 0)
        {
            timeRemain -= Time.deltaTime;
            MeshRenderer mr = GetComponent<MeshRenderer>();
            mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, timeRemain);
            yield return null;

        }
    }
}
