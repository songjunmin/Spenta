using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WarrantSystem : MonoBehaviour
{
    // 보후만 , 카사트라 , 아샤
    public GameObject[] warrantList = new GameObject[3];

    string[,] textString = new string[3, 4]
        {
            {"쿨타임 감소\n- 0.5초\n", "데미지 증가\n+ 0.33\n","공격 대상\n+ 1\n","적 처치시\n쿨타임 초기화\n" },
            {"쿨타임 감소\n- 0.7초\n", "데미지 증가\n+ 0.17\n","사거리 증가\n+ 250\n","투사체 속도\n2배\n" },
            {"쿨타임 감소\n- 0.5초\n", "데미지 증가\n+ 0.1\n","사거리 증가\n+ 200\n","지속적으로\n불길이\n솟아오름\n" }
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
            // 보후만
            case 0:

                switch (number)
                {
                    // 기초 강화 1
                    case 0:

                        GameManager.instance.Player.GetComponent<PlayerStatus>().coolTime[0] -= 0.5f;
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

                        GameManager.instance.Player.GetComponent<PlayerStatus>().coolTime[1] -= 0.7f;
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

                        GameManager.instance.Player.GetComponent<PlayerStatus>().coolTime[2] -= 0.7f;
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
}
