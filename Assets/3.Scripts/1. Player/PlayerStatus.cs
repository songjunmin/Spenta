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
    public float defense;
    public float attackSpeed;

    // ���ĸ� , ī��Ʈ�� , �ƻ� , �Ƹ�����Ƽ

    public float[] skillCoolTime = new float[4];
    public float[] skillCurTime = new float[4];
    public bool[] skillCanUse = new bool[4];
    public float[] skillDmg = new float[3];

    // ����, �и� , ���� ��Ÿ��
    public float[] nonSkillCoolTime = new float[3];
    public float[] nonSkillCurTime = new float[3];
    public bool[] nonSkillCanUse = new bool[3];

    [Serializable]
    public class List
    {
        public int[] range = new int[2];

        public void ChangeRnage(int minusNum, int plusNum)
        {
            range[0] -= minusNum;
            range[1] += plusNum;
        }

        public int GetRange1()
        {
            return range[0];
        }

        public int GetRange2()
        {
            return range[1];
        }
    }
    public List[] skillRange = new List[3];

    /*
    // ���ĸ� ��Ÿ��
    public float bohumanskillCoolTime;
    public float bohumanskillCurTime;
    public bool bohumanSkill;

    // ī��Ʈ�� ��Ÿ��
    public float cassatraskillCoolTime;
    public float cassatraskillCurTime;
    public bool cassatraSkill;

    // �ƻ� ��Ÿ��
    public float ashaskillCoolTime;
    public float ashaskillCurTime;
    public bool ashaSkill;

    // �Ƹ�����Ƽ ��Ÿ��
    public float armaityskillCoolTime;
    public float armaityskillCurTime;
    public bool armaitySkill;
    */


    // �Ƹ޻��� �Ǵ�

    // ���� �Ǵ� - ����
    public float flashRange;

    // ������ �Ǵ� - �и�
    public float parryingTime;
    public float perfectParryingTime;
    public float invincibilityTime;


    // ������ �Ǵ�
    public float peaceTime;
    public bool isFighting;
    public float needForPeace;
    public int shieldMax;

    // ����� �Ǵ�
    public int damageAbsortion;
    public bool spentaAbsortion;
    public float trapDmg;

    // �ƻ� 
    public bool isAhsaEnforce = false;

    // ?
    public float totalAmount;
    void Start()
    {
        peaceTime = 0;
        isFighting = false;
    }

    void Update()
    {
        skillCoolTimeMng();
        ShieldTest();
        PeaceTimeMng();
    }

    public void skillCoolTimeMng()
    {
        for (int i = 0; i < 4; i++)
        {
            skillCurTime[i] -= Time.deltaTime;
            nonSkillCurTime[i] -= Time.deltaTime;

            if (skillCurTime[i] < 0 && skillCanUse[i] == false)
            {
                skillCanUse[i] = true;
                
            }
            if (nonSkillCurTime[i] < 0 && nonSkillCanUse[i] == false)
            {
                nonSkillCanUse[i] = true;
            }
        }
    }
    public void Action(PlayerAction.SkillName skillName)
    {
        skillCurTime[(int)skillName] = skillCoolTime[(int)skillName];
        skillCanUse[(int)skillName] = false;
    }

    public void Action(PlayerAction.NonSkillName nonSkillName)
    {
        nonSkillCurTime[(int)nonSkillName] = nonSkillCoolTime[(int)nonSkillName];
        nonSkillCanUse[(int)nonSkillName] = false;
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
        hpBar.fillAmount = hp / maxHp;
        shieldBar.fillAmount = shield / maxShield;
    }

    public void Damaged(bool trueDamaged, float atkPower, float coefficient)
    {
        float totalDamage = atkPower * coefficient;

        float force;

        // ���� ���� ���� ������
        if (!trueDamaged)
        {
            totalDamage /= (100 + defense);
            force = 20f;
        }

        else
        {
            force = 40f;
        }

        if (shield > 0)
        {
            if (totalDamage < shield)
            {
                shield -= totalDamage;
            }
            else
            {
                hp -= (totalDamage - shield);
                shield = 0;
            }
        }

        peaceTime = needForPeace;

        GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x * -1, 0.5f) * force,ForceMode2D.Impulse);

        ChangeHp();

    }

    public void HpAbsorption(float dmg)
    {
        hp += damageAbsortion * dmg / 100;
        ChangeHp();
    }
}
