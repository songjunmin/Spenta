using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatus : MonoBehaviour
{
    public float hp;
    public float maxHp;

    public enum SkillName
    {
        bohuman, cassatra, asha, armaity
    }

    public float[] coolTime = new float[4];
    public float[] curTime = new float[4];
    public bool[] canUse = new bool[4];

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

    void Start()
    {
        
    }

    void Update()
    {
        CoolTimeMng();
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
    public void UseSkill(int skillNum)
    {
        curTime[skillNum] = coolTime[skillNum];
        canUse[skillNum] = false;
    }
}
