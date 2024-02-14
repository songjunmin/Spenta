using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    public float defense;
    public float AttackPower;
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

    public void Damaged(float dmg, float coefficient)
    {
        hp -= ( dmg * coefficient * 100 / (100 + defense) );
        hpBar.fillAmount = hp / maxHp;
    }
}
