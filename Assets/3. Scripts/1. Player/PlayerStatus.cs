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


    // 보후만 , 카사트라 , 아샤 , 아르마이티

    public float[] skillCoolTime = new float[4];
    public float[] skillCurTime = new float[4];
    public bool[] skillCanUse = new bool[4];
    public float[] skillDmg = new float[3];

    // 점멸, 패링 , 공격 쿨타임
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

    private void OnDrawGizmos()
    {
        // 공격 사거리 11
        // 스킬 사거리 23


        // 스킨 범위
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y + 1f), new Vector2(skillRange[2].GetRange1(), 1));

    }

    /*
    // 보후만 쿨타임
    public float bohumanskillCoolTime;
    public float bohumanskillCurTime;
    public bool bohumanSkill;

    // 카사트라 쿨타임
    public float cassatraskillCoolTime;
    public float cassatraskillCurTime;
    public bool cassatraSkill;

    // 아샤 쿨타임
    public float ashaskillCoolTime;
    public float ashaskillCurTime;
    public bool ashaSkill;

    // 아르마이티 쿨타임
    public float armaityskillCoolTime;
    public float armaityskillCurTime;
    public bool armaitySkill;
    */


    // 아메샤의 권능

    // 빛의 권능 - 점멸
    public float flashRange;
    public bool flashReset;
    public float flashResetTime;

    // 진리의 권능 - 패링
    public float parryingTime;
    public float perfectParryingTime;
    public float invincibilityTime;


    // 생명의 권능
    public float peaceTime;
    public bool isFighting;
    public float needForPeace;

    // 사랑의 권능
    public int damageAbsortion;
    public bool spentaAbsortion;
    public float trapDmg;

    // 아샤 
    public bool isAhsaEnforce = false;


    PlayerMove pm;
    PlayerSpine ps;
    void Start()
    {
        peaceTime = 0;
        isFighting = false;

        pm = GetComponent<PlayerMove>();
        ps = transform.GetChild(0).GetComponent<PlayerSpine>();
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

    public IEnumerator FlashCoolTimeReset()
    {
        flashResetTime = 1f;

        while (flashResetTime > 0)
        {
            flashResetTime -= Time.deltaTime;
            yield return null;
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

    public void Damaged(bool trueDamaged, float atkPower, float coefficient , float attVecX )
    {
        float totalDamage = atkPower * coefficient;

        float force;

        // 일반 데미지
        if (!trueDamaged)
        {
            totalDamage /= (100 + defense);
            force = 15f;
        }

        // 함정 등의 고정 데미지
        else
        {
            force = 0f;
        }

        if (ps.animState.IsName("Block"))
        {
            if ((transform.position.x - attVecX) * transform.localScale.x < 0)
            {
                Debug.Log("막음");
                return;
            }
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

        else
        {
            hp -= totalDamage;
        }
        peaceTime = needForPeace;

        if (!trueDamaged)
        {
            pm.HitOn();
            if (transform.position.x > attVecX)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0.5f) * force, ForceMode2D.Impulse);
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0.5f) * force, ForceMode2D.Impulse);
            }
        }
        ChangeHp();
    }

    public void HpAbsorption(float dmg)
    {
        hp += damageAbsortion * dmg / 100;
        ChangeHp();
    }



    public void EnemyDead()
    {
        if (flashResetTime > 0)
        {
            nonSkillCurTime[0] = 0;
        }
    }
}
