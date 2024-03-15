using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static PlayerStatus;
using static WarrantSystem;

[System.Serializable]
public class SaveData
{
    // WarrantSystem
    public SpentaWarrant[] maxSpentaWarrant = new SpentaWarrant[3];
    public SpentaWarrant[] nowSpentaWarrant = new SpentaWarrant[3];
    public AmeshaWarrant[] maxAmeshaWarrant = new AmeshaWarrant[5];
    public AmeshaWarrant[] nowAmeshaWarrant = new AmeshaWarrant[5];



    // PlayerStatus
    public float hp;
    public float maxHp;
    public float shield;
    public float maxShield;
    public float attackPower;
    public float defense;
    public float attackSpeed;
    public float[] skillCoolTime = new float[4];
    public float[] skillCurTime = new float[4];
    public bool[] skillCanUse = new bool[4];
    public float[] skillDmg = new float[3];
    public float[] nonSkillCoolTime = new float[3];
    public float[] nonSkillCurTime = new float[3];
    public bool[] nonSkillCanUse = new bool[3];
    public List[] skillRange = new List[3];
    public float flashRange;
    public bool flashReset;
    public float parryingTime;
    public float perfectParryingTime;
    public float invincibilityTime;
    public float needForPeace;
    public int damageAbsortion;
    public bool spentaAbsortion;
    public float trapDmg;

    // PlayerMove
    public float speed;

    // GameManager
    public float pieceOfEnlightenment;
    public float sparkOfKnowledge;
    public float[] abnormalTime;

    // SceneLoad
    public int stage;
    public List<int> stageList;
}

public class SaveLoadMng : MonoBehaviour
{
    string path;

    WarrantSystem ws;
    PlayerStatus ps;
    PlayerMove pm;
    SceneLoad sl;


    private void Start()
    {
        ws = GameManager.instance.GetComponent<WarrantSystem>();
        ps = GameManager.instance.Player.GetComponent<PlayerStatus>();
        pm = GameManager.instance.Player.GetComponent<PlayerMove>();
        sl = GameManager.instance.GetComponent<SceneLoad>();

    }

    public void JsonLoad()
    {
        path = Path.Combine(Application.dataPath, "database.json");
        SaveData saveData = new SaveData();

        if (!File.Exists(path))
        {
            Debug.Log("세이브 경로 에러");
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null)
            {
                // WarrantSystem
                ws.maxSpentaWarrant = saveData.maxSpentaWarrant;
                ws.nowSpentaWarrant = saveData.nowSpentaWarrant;
                ws.maxAmeshaWarrant = saveData.maxAmeshaWarrant;
                ws.nowAmeshaWarrant = saveData.nowAmeshaWarrant;

                // PlayerStatus
                ps.hp = saveData.hp;
                ps.maxHp = saveData.maxHp;
                ps.shield = saveData.shield;
                ps.maxShield = saveData.maxShield;
                ps.attackPower = saveData.attackPower;
                ps.defense = saveData.defense;
                ps.attackSpeed = saveData.attackSpeed;
                ps.skillCoolTime = saveData.skillCoolTime;
                ps.skillCurTime = saveData.skillCurTime;
                ps.skillCanUse = saveData.skillCanUse;
                ps.skillDmg = saveData.skillDmg;
                ps.nonSkillCoolTime = saveData.nonSkillCoolTime;
                ps.nonSkillCurTime = saveData.nonSkillCurTime;
                ps.nonSkillCanUse = saveData.nonSkillCanUse;
                ps.skillRange = saveData.skillRange;
                ps.flashRange = saveData.flashRange;
                ps.flashReset = saveData.flashReset;
                ps.parryingTime = saveData.parryingTime;
                ps.perfectParryingTime = saveData.perfectParryingTime;
                ps.invincibilityTime = saveData.invincibilityTime;
                ps.needForPeace = saveData.needForPeace;
                ps.damageAbsortion = saveData.damageAbsortion;
                ps.spentaAbsortion = saveData.spentaAbsortion;
                ps.trapDmg = saveData.trapDmg;

                ps.ChangeHp();
                // PlayerMove
                pm.speed = saveData.speed;

                // GameManager
                GameManager.instance.pieceOfEnlightenment = saveData.pieceOfEnlightenment;
                GameManager.instance.sparkOfKnowledge = saveData.sparkOfKnowledge;
                GameManager.instance.abnormalTime = saveData.abnormalTime;
                GameManager.instance.GetItem();

                // SceneLoad
                sl.stage = saveData.stage;
                sl.stageList = saveData.stageList;
            }
        }
    }

    public void JsonSave()
    {
        // saveData에 정보들을 저장
        SaveData saveData = new SaveData();

        // WarrantSysten
        saveData.maxSpentaWarrant = ws.maxSpentaWarrant;
        saveData.nowSpentaWarrant = ws.nowSpentaWarrant;
        saveData.maxAmeshaWarrant = ws.maxAmeshaWarrant;
        saveData.nowAmeshaWarrant = ws.nowAmeshaWarrant;


        // PlayerStatue
        saveData.hp = ps.hp;
        saveData.maxHp = ps.maxHp;
        saveData.shield = ps.shield;
        saveData.maxShield = ps.maxShield;
        saveData.attackPower = ps.attackPower;
        saveData.defense = ps.defense;
        saveData.attackSpeed = ps.attackSpeed;
        saveData.skillCoolTime = ps.skillCoolTime;
        saveData.skillCurTime = ps.skillCurTime;
        saveData.skillCanUse = ps.skillCanUse;
        saveData.skillDmg = ps.skillDmg;
        saveData.nonSkillCoolTime = ps.nonSkillCoolTime;
        saveData.nonSkillCurTime = ps.nonSkillCurTime;
        saveData.nonSkillCanUse = ps.nonSkillCanUse;
        saveData.skillRange = ps.skillRange;
        saveData.flashRange = ps.flashRange;
        saveData.flashReset = ps.flashReset;
        saveData.parryingTime = ps.parryingTime;
        saveData.perfectParryingTime = ps.perfectParryingTime;
        saveData.invincibilityTime = ps.invincibilityTime;
        saveData.needForPeace = ps.needForPeace;
        saveData.damageAbsortion = ps.damageAbsortion;
        saveData.spentaAbsortion = ps.spentaAbsortion;
        saveData.trapDmg = ps.trapDmg;

        // PlayerMove
        saveData.speed = pm.speed;

        // GameManager
        saveData.pieceOfEnlightenment = GameManager.instance.pieceOfEnlightenment;
        saveData.sparkOfKnowledge = GameManager.instance.sparkOfKnowledge;
        saveData.abnormalTime = GameManager.instance.abnormalTime;

        // SceneLoad
        saveData.stage = sl.stage;
        saveData.stageList = sl.stageList;

        string json = JsonUtility.ToJson(saveData, true);

        path = Path.Combine(Application.dataPath, "database.json");
        File.WriteAllText(path, json);
    }
}
