using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WarrantSystem : MonoBehaviour
{
    // ���ĸ� , ī��Ʈ�� , �ƻ�
    public GameObject[] warrantList = new GameObject[3];

    string[,] textString = new string[3, 4]
        {
            {"��Ÿ�� ����\n- 0.5��\n", "������ ����\n+ 0.33\n","���� ���\n+ 1\n","�� óġ��\n��Ÿ�� �ʱ�ȭ\n" },
            {"��Ÿ�� ����\n- 0.7��\n", "������ ����\n+ 0.17\n","��Ÿ� ����\n+ 250\n","����ü �ӵ�\n2��\n" },
            {"��Ÿ�� ����\n- 0.5��\n", "������ ����\n+ 0.1\n","��Ÿ� ����\n+ 200\n","����������\n�ұ���\n�ھƿ���\n" }
        };


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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMessage()
    {
        for (int i = 0; i < 3; i++)
        {
            Text basic1 = warrantList[i].gameObject.transform.GetChild(0).GetComponentInChildren<Text>();  
            Text basic2 = warrantList[i].gameObject.transform.GetChild(1).GetComponentInChildren<Text>();  
            Text intermediate = warrantList[i].gameObject.transform.GetChild(2).GetComponentInChildren<Text>();  
            Text advanced = warrantList[i].gameObject.transform.GetChild(3).GetComponentInChildren<Text>();

            basic1.text = textString[i,0] + "(" + nowSpentaWarrant[i].basic1.ToString() + "/" + maxSpentaWarrant[i].basic1.ToString() + ")";
            basic2.text = textString[i,1] + "(" + nowSpentaWarrant[i].basic2.ToString() + "/" + maxSpentaWarrant[i].basic2.ToString() + ")";
            intermediate.text = textString[i,2] + "(" + nowSpentaWarrant[i].intermediate.ToString() + "/" + maxSpentaWarrant[i].intermediate.ToString() + ")";
            advanced.text = textString[i,3] + "(" + nowSpentaWarrant[i].advanced.ToString() + "/" + maxSpentaWarrant[i].advanced.ToString() + ")";
        }
        
    }

    public void GetWarrant()
    {
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
                }
                break;

            case "basic2":
                if (nowSpentaWarrant[i].basic2 < maxSpentaWarrant[i].basic2)
                {
                    nowSpentaWarrant[i].basic2++;
                    ApplyWarrant(i, 1);
                }
                break;

            case "intermediate":
                if (nowSpentaWarrant[i].intermediate < maxSpentaWarrant[i].intermediate)
                {
                    nowSpentaWarrant[i].intermediate++;
                    ApplyWarrant(i, 2);
                }
                break;

            case "advanced":
                if (nowSpentaWarrant[i].advanced < maxSpentaWarrant[i].advanced)
                {
                    nowSpentaWarrant[i].advanced++;
                    ApplyWarrant(i, 3);
                }
                break;

        }

        SetMessage();

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

                        GameManager.instance.Player.GetComponent<PlayerStatus>().coolTime[0] -= 0.5f;
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

                        GameManager.instance.Player.GetComponent<PlayerStatus>().coolTime[1] -= 0.7f;
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

                        GameManager.instance.Player.GetComponent<PlayerStatus>().coolTime[2] -= 0.7f;
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
}
