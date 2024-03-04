using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WarrantSystem : MonoBehaviour
{
    // 보후만 , 카사트라 , 아샤
    public GameObject[] spentaWarrantList = new GameObject[3];

    // Test
    public GameObject[] ameshaWarrantList = new GameObject[5];

    string[,] spentaTextString = new string[3, 4]
        {
            {"쿨타임 감소\n- 0.5초\n", "데미지 증가\n+ 0.33\n","공격 대상\n+ 1\n","적 처치시\n쿨타임 초기화\n" },
            {"쿨타임 감소\n- 0.7초\n", "데미지 증가\n+ 0.17\n","사거리 증가\n+ 250\n","투사체 속도\n2배\n" },
            {"쿨타임 감소\n- 0.5초\n", "데미지 증가\n+ 0.1\n","사거리 증가\n+ 200\n","지속적으로\n불길이\n솟아오름\n" }
        };

    string[,] ameshaTextString = new string[5, 4]
    {
        {"빛의 권능 - 점멸","권능 자체 효과를 강화한다\nLv당 쿨타임 -1초\n","점멸의 사거리가 증가한다\nLv당 +100\n","스킬 사용후 1초 이내에\n적 처치 시 쿨타임 초기화\n" },
        {"진리의 권능 - 패링","권능 자체 효과를 강화한다\nLv당 패링 지속시간 +0.25초\n","정확한 패링 타이밍 완화\nLv당 +0.25초\n","무적의 지속시간 1초 증가\n" },
        {"생명의 권능","플레이어 체력 증가\nLv당 +20\n","보호막 최대치 증가\nLv당 +10\n","보호막 생성 시간 1초로 감소\n평온 상태 돌입까지\n걸리는 시간 1초 감소\n" },
        {"사랑의 권능","권능 자체 효과를 강화한다\nLv당 회복량 +1%\n","함정으로 입는 데미지 감소\nLv당 -30%\n","스펜타의 권능 공격에도\n피해 흡수 적용\n" },
        {"정의의 권능","권능 자체 효과를 강화한다\nLv당 공격력 +10\n","공격 속도가 증가한다\nLv당 +0.2\n","이동속도가 0.6 증가한다\n" },
    };

    string[,] ameshaBtnString = new string[5, 3]
    {
        {"번쩍","섬광","신출귀몰" },
        {"넉넉한 패링","팅-","무-적" },
        {"생명력 증강","철벽","재기동" },
        {"불씨 흡수","함정 무효","염화" },
        {"빠른 집행","저스티스","신속" }
    };

    int randInt;

    public enum Rarity
    {
        Normal,
        Rare,
        Legendary
    }

    [Serializable]
    public class SpentaWarrant
    {
        public int basic1;
        public int basic2;
        public int intermediate;
        public int advanced;
    }

    public SpentaWarrant[] maxSpentaWarrant = new SpentaWarrant[3];
    public SpentaWarrant[] nowSpentaWarrant = new SpentaWarrant[3];



    [Serializable]
    public class AmeshaWarrant
    {
        public int normal;
        public int rare;
        public int legendary;

        public int GetPointByInt(int x)
        {
            if (x == 0)
            {
                return normal;
            }
            else if (x == 1)
            {
                return rare;
            }
            else
            {
                return legendary;
            }
        }

        public void SetPointByint(int x, int add)
        {
            if (x == 0)
            {
                normal += add;
            }
            else if (x == 1)
            {
                rare += add;
            }
            else
            {
                legendary += add;
            }
        }
    }

    public AmeshaWarrant[] maxAmeshaWarrant = new AmeshaWarrant[5];
    public AmeshaWarrant[] nowAmeshaWarrant = new AmeshaWarrant[5];

    public GameObject ameshaPanel;

    public int rarityNum;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // 스펜타의 권능 설명 문구
    public void SetMessageSpenta()
    {
        for (int i = 0; i < 3; i++)
        {
            Text basic1 = spentaWarrantList[i].gameObject.transform.GetChild(0).GetComponentInChildren<Text>();  
            Text basic2 = spentaWarrantList[i].gameObject.transform.GetChild(1).GetComponentInChildren<Text>();  
            Text intermediate = spentaWarrantList[i].gameObject.transform.GetChild(2).GetComponentInChildren<Text>();  
            Text advanced = spentaWarrantList[i].gameObject.transform.GetChild(3).GetComponentInChildren<Text>();

            basic1.text = spentaTextString[i,0] + "(" + nowSpentaWarrant[i].basic1.ToString() + "/" + maxSpentaWarrant[i].basic1.ToString() + ")";
            basic2.text = spentaTextString[i,1] + "(" + nowSpentaWarrant[i].basic2.ToString() + "/" + maxSpentaWarrant[i].basic2.ToString() + ")";
            intermediate.text = spentaTextString[i,2] + "(" + nowSpentaWarrant[i].intermediate.ToString() + "/" + maxSpentaWarrant[i].intermediate.ToString() + ")";
            advanced.text = spentaTextString[i,3] + "(" + nowSpentaWarrant[i].advanced.ToString() + "/" + maxSpentaWarrant[i].advanced.ToString() + ")";
        }
        
    }

    // 아메샤의 권능 설명 문구
    public void SetMessageAmesha()
    {
        for (int i = 0; i < 5; i++)
        {
            Text explain = ameshaWarrantList[i].gameObject.transform.GetChild(0).GetComponent<Text>();
            Text normalB = ameshaWarrantList[i].gameObject.transform.GetChild(1).GetChild(0).GetComponent<Text>();
            Text normal = ameshaWarrantList[i].gameObject.transform.GetChild(1).GetChild(1).GetComponent<Text>();
            Text rareB = ameshaWarrantList[i].gameObject.transform.GetChild(2).GetChild(0).GetComponent<Text>();
            Text rare = ameshaWarrantList[i].gameObject.transform.GetChild(2).GetChild(1).GetComponent<Text>();
            Text legendaryB = ameshaWarrantList[i].gameObject.transform.GetChild(3).GetChild(0).GetComponent<Text>();
            Text legendary = ameshaWarrantList[i].gameObject.transform.GetChild(3).GetChild(1).GetComponent<Text>();

            explain.text = ameshaTextString[i,0];
            normalB.text = ameshaBtnString[i, 0];
            normal.text = ameshaTextString[i, 1] + "Lv " + nowAmeshaWarrant[i].normal.ToString() + "/" + maxAmeshaWarrant[i].normal.ToString();
            rareB.text = ameshaBtnString[i, 1];
            rare.text = ameshaTextString[i,2] + "Lv " + nowAmeshaWarrant[i].rare.ToString() + "/" + maxAmeshaWarrant[i].rare.ToString();
            legendaryB.text = ameshaBtnString[i, 2];
            legendary.text = ameshaTextString[i, 3] + "Lv " + nowAmeshaWarrant[i].legendary.ToString() + "/" + maxAmeshaWarrant[i].legendary.ToString();
        }
    }


    // 스펜타의 권능 눌렀을떄 (Test)
    public void GetWarrant()
    {
        if (GameManager.instance.pieceOfEnlightenment <= 0)
        {
            Debug.Log("재화 부족");
            return;
        }

        

        GameObject nowClicked = EventSystem.current.currentSelectedGameObject;
        string btnName = nowClicked.name;

        string ParentName = nowClicked.transform.parent.name;

        int i = 0;
        switch(ParentName)
        {
            case "Bohuman":
                i = 0;
                break;

            case "Cassatra":
                i = 1;
                break;

            case "Asha":
                i = 2;
                break;
        }


        switch(btnName)
        {
            case "basic1":
                if (nowSpentaWarrant[i].basic1 < maxSpentaWarrant[i].basic1)
                {
                    nowSpentaWarrant[i].basic1++;
                    ApplyWarrant(i, 0);
                    GameManager.instance.ChangePieceOfEnlightenment(-1);
                }
                break;

            case "basic2":
                if (nowSpentaWarrant[i].basic2 < maxSpentaWarrant[i].basic2)
                {
                    nowSpentaWarrant[i].basic2++;
                    ApplyWarrant(i, 1);
                    GameManager.instance.ChangePieceOfEnlightenment(-1);
                }
                break;

            case "intermediate":
                if (nowSpentaWarrant[i].intermediate < maxSpentaWarrant[i].intermediate)
                {
                    nowSpentaWarrant[i].intermediate++;
                    ApplyWarrant(i, 2);
                    GameManager.instance.ChangePieceOfEnlightenment(-1);
                }
                break;

            case "advanced":
                if (nowSpentaWarrant[i].advanced < maxSpentaWarrant[i].advanced)
                {
                    nowSpentaWarrant[i].advanced++;
                    ApplyWarrant(i, 3);
                    GameManager.instance.ChangePieceOfEnlightenment(-1);
                }
                break;

        }

        SetMessageSpenta();

    }

    void ApplyWarrant(int type, int number)
    {
        switch (type)
        {
            // 보후만
            case 0:

                switch (number)
                {
                    // 기초 강화 1
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillCoolTime[0] -= 0.5f;
                        break;

                    // 기초 강화 2
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillDmg[0] += 0.33f;
                        break;

                    // 중급 강화
                    case 2:

                        // 공격 대상 +1 
                        break;

                    case 3:

                        // 적 처치시 쿨타임 초기화
                        break;
                }



                break;

            // 카사트라
            case 1:

                switch (number)
                {
                    // 기초 강화 1
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillCoolTime[1] -= 0.7f;
                        break;

                    // 기초 강화 2
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillDmg[1] += 0.17f;
                        break;

                    // 중급 강화
                    case 2:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillRange[1].ChangeRnage(0, 250);
                        break;

                    case 3:

                        // 투사체 속도 2배 (+ 창 되돌아옴)
                        break;
                }
                break;
            
            // 아샤
            case 2:

                switch (number)
                {
                    // 기초 강화 1
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillCoolTime[2] -= 0.7f;
                        break;

                    // 기초 강화 2
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillDmg[2] += 0.17f;
                        break;

                    // 중급 강화
                    case 2:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillRange[2].ChangeRnage(100, 100);
                        break;

                    case 3:

                        // 지속적으로 불길이 솟아오름
                        break;
                }
                break;
        }
    }

    // 아메샤의 권능 눌렀을때
    public void GetAmeshaWarrant()
    {
        if (GameManager.instance.sparkOfKnowledge <= 0)
        {
            Debug.Log("재화 부족");
            return;
        }

        
        GameObject nowClicked = EventSystem.current.currentSelectedGameObject;
        string btnName = nowClicked.name;

        string ParentName = nowClicked.transform.parent.name;

        int i = 0;
        switch (ParentName)
        {
            case "Flash":
                i = 0;
                break;

            case "Parrying":
                i = 1;
                break;

            case "Life":
                i = 2;
                break;

            case "Love":
                i = 3;
                break;
                 
            case "Justice":
                i = 4;
                break;
        }

        randInt = UnityEngine.Random.Range(0, 100);

        switch (btnName)
        {
            case "Normal":
                if (nowAmeshaWarrant[i].normal < maxAmeshaWarrant[i].normal)
                {
                    GameManager.instance.ChangeSparkOfKnowledge(-1);

                    if (randInt < 75)
                    {
                        nowAmeshaWarrant[i].normal++;
                        ApplyAmeshaWarrant(i, 0);
                    }
                    else
                    {
                        Debug.Log("강화 실패");
                    }
                }
                break;

            case "Rare":
                if (nowAmeshaWarrant[i].rare < maxAmeshaWarrant[i].rare)
                {
                    GameManager.instance.ChangeSparkOfKnowledge(-1);

                    if (randInt < 25)
                    {
                        nowAmeshaWarrant[i].rare++;
                        ApplyAmeshaWarrant(i, 1);
                    }
                    else
                    {
                        Debug.Log("강화 실패");
                    }
                }
                break;

            case "Legendary":
                if (nowAmeshaWarrant[i].legendary < maxAmeshaWarrant[i].legendary)
                {
                    GameManager.instance.ChangeSparkOfKnowledge(-1);

                    if (randInt < 5)
                    {
                        nowAmeshaWarrant[i].legendary++;
                        ApplyAmeshaWarrant(i, 2);
                    }
                    else
                    {
                        Debug.Log("강화 실패");
                    }
                }
                break;
        }

        SetMessageAmesha();
    }

    void ApplyAmeshaWarrant(int type, int number)
    {
        switch (type)
        {
            // 점멸
            case 0:

                switch (number)
                {
                    // 일반 특성
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().nonSkillCoolTime[0] -= 1f;
                        break;

                    // 희귀 특성
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().flashRange += 100;
                        break;

                    // 전설 특성
                    case 2:

                        // 스킬 사용 후 1초 이내에 적 처치 시 쿨타임 초기화
                        break;

                    
                }



                break;

            // 패링
            case 1:

                switch (number)
                {
                    // 일반 특성
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().parryingTime += 0.25f;
                        break;

                    // 희귀 특성
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().perfectParryingTime += 0.25f;
                        break;

                    // 전설 특성
                    case 2:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().invincibilityTime += 1f;
                        break;

                    
                }
                break;

            // 생명의 권능
            case 2:

                switch (number)
                {
                    // 일반 특성
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().maxHp += 20;
                        GameManager.instance.Player.GetComponent<PlayerStatus>().hp += 20;
                        break;

                    // 희귀 특성
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().maxShield += 10;
                        break;

                    // 전설 특성
                    case 2:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().needForPeace -= 1f;
                        break;

                }
                break;

            // 사랑의 권능
            case 3:

                switch (number)
                {
                    // 일반 특성
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().damageAbsortion += 1;
                        break;

                    // 희귀 특성
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().trapDmg -= 30;
                        break;

                    // 전설 특성
                    case 2:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().spentaAbsortion = true;
                        break;

                }
                break;

            // 정의의 권능
            case 4:

                switch (number)
                {
                    // 일반 특성
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().attackPower += 10;
                        break;

                    // 희귀 특성
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().attackSpeed += 0.2f;
                        break;

                    // 전설 특성
                    case 2:

                        GameManager.instance.Player.GetComponent<PlayerMove>().speed += 3f;
                        break;

                }
                break;
        }
    }

    public void OpenAmeshaWarrant()
    {
        ameshaPanel.SetActive(true);
        Time.timeScale = 0f;



        // 이전 버전
        /*
        randInt = UnityEngine.Random.Range(0, 100);

        if (randInt < 70)
        {
            rarityNum = 0;
            OpenAmeshaWarrant(Rarity.Normal);
        }
        else if (randInt < 95)
        {
            rarityNum = 1;
            OpenAmeshaWarrant(Rarity.Rare);
        }
        else
        {
            rarityNum = 2;
            OpenAmeshaWarrant(Rarity.Legendary);
        }
        */
    }
    // 이전 버전
    public void OpenAmeshaWarrant(Rarity rarity)
    {
        int j = -1;

        switch (rarity)
        {
            case Rarity.Normal:
                j = 0;
                break;

            case Rarity.Rare:
                j = 1;
                break;

            case Rarity.Legendary:
                j = 2;
                break;

            default:
                break;
        }

        // 오류 체크
        if (j == -1)
        {
            Debug.Log("에러 발생");
        }

        ameshaPanel.transform.GetChild(0).GetComponent<Text>().text = "< " + rarity.ToString() + " >";

        for (int i = 0; i < 5; i++)
        {

            ameshaPanel.transform.GetChild(i + 1).GetChild(0).GetComponent<Text>().text = ameshaTextString[i, 0];
            ameshaPanel.transform.GetChild(i + 1).GetChild(1).GetChild(0).GetComponent<Text>().text = ameshaBtnString[i, j];
            ameshaPanel.transform.GetChild(i + 1).GetChild(1).GetChild(1).GetComponent<Text>().text
                = ameshaTextString[i, j] + "Lv " + nowAmeshaWarrant[i].GetPointByInt(j).ToString() + 
                   "/" + maxAmeshaWarrant[i].GetPointByInt(j).ToString();

        }
        

        ameshaPanel.SetActive(true);
        Time.timeScale = 0;
    }


    public void GetAmeshaWarrantTest()
    {
        GameObject nowClicked = EventSystem.current.currentSelectedGameObject;
        string typeName = nowClicked.transform.parent.name;

        int i = 0;
        switch (typeName)
        {
            case "Flash":
                i = 0;
                break;

            case "Parrying":
                i = 1;
                break;

            case "Life":
                i = 2;
                break;

            case "Love":
                i = 3;
                break;

            case "Justice":
                i = 4;
                break;
        }

        
        if (nowAmeshaWarrant[i].GetPointByInt(rarityNum) < maxAmeshaWarrant[i].GetPointByInt(rarityNum))
        {
            nowAmeshaWarrant[i].SetPointByint(rarityNum,1);
            ApplyAmeshaWarrant(i, rarityNum);
        }
                
        SetMessageAmesha();
        ameshaPanel.SetActive(false);
        Time.timeScale = 1;
    }

}
