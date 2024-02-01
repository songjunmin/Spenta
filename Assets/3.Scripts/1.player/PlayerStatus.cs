using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public float shield;
    public float maxShield;

    public Image hpBar;
    public Image shieldBar;

    public float attackPower;
    public float attackSpeed;

    // 보후만 , 카사트라 , 아샤 , 아르마이티

    public float[] coolTime = new float[4];
    public float[] curTime = new float[4];
    public bool[] canUse = new bool[4];
    public float[] skillDmg = new float[3];

    [Serializable]
    public class List
    {
        public int[] range = new int[2];

        public void ChangeRnage(int minusNum, int plusNum)
        {
            range[0] -= minusNum;
            range[1] += plusNum;
        }
    }
    public List[] skillRange = new List[3];

    /*
    // 보후만 쿨타임
    public float bohumanCoolTime;
    public float bohumanCurTime;
    public bool bohumanSkill;

    // 카사트라 쿨타임
    public float cassatraCoolTime;
    public float cassatraCurTime;
    public bool cassatraSkill;

    // 아샤 쿨타임
    public float ashaCoolTime;
    public float ashaCurTime;
    public bool ashaSkill;

    // 아르마이티 쿨타임
    public float armaityCoolTime;
    public float armaityCurTime;
    public bool armaitySkill;
    */


    // 아메샤의 권능
    public float peaceTime;
    public bool isFighting;
    public float needForPeace;
    public int shieldMax;
    public int damageAbsortion;
    public bool spentaAbsortion;



    public float totalAmount;
    void Start()
    {
        peaceTime = 0;
        isFighting = false;
    }

    void Update()
    {
        CoolTimeMng();
        ShieldTest();
        PeaceTimeMng();
    }

    public void CoolTimeMng()
    {
        for (int i = 0; i < 4; i++)
        {
            curTime[i] -= Time.deltaTime;

            if (curTime[i] < 0 && canUse[i] == false)
            {
                canUse[i] = true;
            }
        }
    }
    public void UseSkill(PlayerAction.SkillName skillName)
    {
        curTime[(int)skillName] = coolTime[(int)skillName];
        canUse[(int)skillName] = false;
    }

    public void PeaceTimeMng()
    {
        if (peaceTime > 0)
        {
            peaceTime -= Time.deltaTime;

            if (peaceTime <= 0)
            {
                isFighting = false;
                shield = maxShield;
                ChangeHp();
            }
        }
    }

    public void ShieldTest()
    {
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            shield -= 5;
            ChangeHp();
        }

        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            hp -= 10;
            peaceTime = needForPeace;
            ChangeHp();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            hp += 10;
            ChangeHp();
        }
    }

    public void ChangeHp()
    {
        totalAmount = (hp / maxHp) * 200 + (shield / maxShield) * 20;

        if (totalAmount > 200)
        {
            hpBar.fillAmount = (hp / maxHp) * (200 / totalAmount);
            shieldBar.fillAmount = (shield / maxShield) * (200 / totalAmount);

        }
        else
        {
            hpBar.fillAmount = hp / maxHp;
            shieldBar.fillAmount = shield / maxShield;
        }

        shieldBar.rectTransform.localPosition = new Vector2(hpBar.fillAmount * 200, shieldBar.rectTransform.localPosition.y);


    }
}
