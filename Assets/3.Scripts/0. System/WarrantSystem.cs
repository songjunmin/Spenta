using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WarrantSystem : MonoBehaviour
{
    // ���ĸ� , ī��Ʈ�� , �ƻ�
    public GameObject[] spentaWarrantList = new GameObject[3];

    // Test
    public GameObject[] ameshaWarrantList = new GameObject[5];

    string[,] spentaTextString = new string[3, 4]
        {
            {"��Ÿ�� ����\n- 0.5��\n", "������ ����\n+ 0.33\n","���� ���\n+ 1\n","�� óġ��\n��Ÿ�� �ʱ�ȭ\n" },
            {"��Ÿ�� ����\n- 0.7��\n", "������ ����\n+ 0.17\n","��Ÿ� ����\n+ 250\n","����ü �ӵ�\n2��\n" },
            {"��Ÿ�� ����\n- 0.5��\n", "������ ����\n+ 0.1\n","��Ÿ� ����\n+ 200\n","����������\n�ұ���\n�ھƿ���\n" }
        };

    string[,] ameshaTextString = new string[5, 4]
    {
        {"���� �Ǵ� - ����","�Ǵ� ��ü ȿ���� ��ȭ�Ѵ�\nLv�� ��Ÿ�� -1��\n","������ ��Ÿ��� �����Ѵ�\nLv�� +100\n","��ų ����� 1�� �̳���\n�� óġ �� ��Ÿ�� �ʱ�ȭ\n" },
        {"������ �Ǵ� - �и�","�Ǵ� ��ü ȿ���� ��ȭ�Ѵ�\nLv�� �и� ���ӽð� +0.25��\n","��Ȯ�� �и� Ÿ�̹� ��ȭ\nLv�� +0.25��\n","������ ���ӽð� 1�� ����\n" },
        {"������ �Ǵ�","�÷��̾� ü�� ����\nLv�� +20\n","��ȣ�� �ִ�ġ ����\nLv�� +10\n","��ȣ�� ���� �ð� 1�ʷ� ����\n��� ���� ���Ա���\n�ɸ��� �ð� 1�� ����\n" },
        {"����� �Ǵ�","�Ǵ� ��ü ȿ���� ��ȭ�Ѵ�\nLv�� ȸ���� +1%\n","�������� �Դ� ������ ����\nLv�� -30%\n","����Ÿ�� �Ǵ� ���ݿ���\n���� ��� ����\n" },
        {"������ �Ǵ�","�Ǵ� ��ü ȿ���� ��ȭ�Ѵ�\nLv�� ���ݷ� +10\n","���� �ӵ��� �����Ѵ�\nLv�� +0.2\n","�̵��ӵ��� 0.6 �����Ѵ�\n" },
    };

    string[,] ameshaBtnString = new string[5, 3]
    {
        {"��½","����","����͸�" },
        {"�˳��� �и�","��-","��-��" },
        {"����� ����","ö��","��⵿" },
        {"�Ҿ� ���","���� ��ȿ","��ȭ" },
        {"���� ����","����Ƽ��","�ż�" }
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
    // ����Ÿ�� �Ǵ� ���� ����
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

    // �Ƹ޻��� �Ǵ� ���� ����
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


    // ����Ÿ�� �Ǵ� �������� (Test)
    public void GetWarrant()
    {
        if (GameManager.instance.pieceOfEnlightenment <= 0)
        {
            Debug.Log("��ȭ ����");
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
            // ���ĸ�
            case 0:

                switch (number)
                {
                    // ���� ��ȭ 1
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillCoolTime[0] -= 0.5f;
                        break;

                    // ���� ��ȭ 2
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillDmg[0] += 0.33f;
                        break;

                    // �߱� ��ȭ
                    case 2:

                        // ���� ��� +1 
                        break;

                    case 3:

                        // �� óġ�� ��Ÿ�� �ʱ�ȭ
                        break;
                }



                break;

            // ī��Ʈ��
            case 1:

                switch (number)
                {
                    // ���� ��ȭ 1
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillCoolTime[1] -= 0.7f;
                        break;

                    // ���� ��ȭ 2
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillDmg[1] += 0.17f;
                        break;

                    // �߱� ��ȭ
                    case 2:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillRange[1].ChangeRnage(0, 250);
                        break;

                    case 3:

                        // ����ü �ӵ� 2�� (+ â �ǵ��ƿ�)
                        break;
                }
                break;
            
            // �ƻ�
            case 2:

                switch (number)
                {
                    // ���� ��ȭ 1
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillCoolTime[2] -= 0.7f;
                        break;

                    // ���� ��ȭ 2
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillDmg[2] += 0.17f;
                        break;

                    // �߱� ��ȭ
                    case 2:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().skillRange[2].ChangeRnage(100, 100);
                        break;

                    case 3:

                        // ���������� �ұ��� �ھƿ���
                        break;
                }
                break;
        }
    }

    // �Ƹ޻��� �Ǵ� ��������
    public void GetAmeshaWarrant()
    {
        if (GameManager.instance.sparkOfKnowledge <= 0)
        {
            Debug.Log("��ȭ ����");
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
                        Debug.Log("��ȭ ����");
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
                        Debug.Log("��ȭ ����");
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
                        Debug.Log("��ȭ ����");
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
            // ����
            case 0:

                switch (number)
                {
                    // �Ϲ� Ư��
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().nonSkillCoolTime[0] -= 1f;
                        break;

                    // ��� Ư��
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().flashRange += 100;
                        break;

                    // ���� Ư��
                    case 2:

                        // ��ų ��� �� 1�� �̳��� �� óġ �� ��Ÿ�� �ʱ�ȭ
                        break;

                    
                }



                break;

            // �и�
            case 1:

                switch (number)
                {
                    // �Ϲ� Ư��
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().parryingTime += 0.25f;
                        break;

                    // ��� Ư��
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().perfectParryingTime += 0.25f;
                        break;

                    // ���� Ư��
                    case 2:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().invincibilityTime += 1f;
                        break;

                    
                }
                break;

            // ������ �Ǵ�
            case 2:

                switch (number)
                {
                    // �Ϲ� Ư��
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().maxHp += 20;
                        GameManager.instance.Player.GetComponent<PlayerStatus>().hp += 20;
                        break;

                    // ��� Ư��
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().maxShield += 10;
                        break;

                    // ���� Ư��
                    case 2:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().needForPeace -= 1f;
                        break;

                }
                break;

            // ����� �Ǵ�
            case 3:

                switch (number)
                {
                    // �Ϲ� Ư��
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().damageAbsortion += 1;
                        break;

                    // ��� Ư��
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().trapDmg -= 30;
                        break;

                    // ���� Ư��
                    case 2:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().spentaAbsortion = true;
                        break;

                }
                break;

            // ������ �Ǵ�
            case 4:

                switch (number)
                {
                    // �Ϲ� Ư��
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().attackPower += 10;
                        break;

                    // ��� Ư��
                    case 1:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().attackSpeed += 0.2f;
                        break;

                    // ���� Ư��
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



        // ���� ����
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
    // ���� ����
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

        // ���� üũ
        if (j == -1)
        {
            Debug.Log("���� �߻�");
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
