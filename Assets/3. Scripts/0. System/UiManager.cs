using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public class Dialogue
    {
        public string name;
        public int pointer;
        public string[] dialogueText;

        public int endPointer;
        public string[] dialogueEndText;


        public Dialogue(string name)
        {
            this.name = name;
            pointer = 0;
            endPointer = 0;
        }
        public void SetDialogueText(string[] text)
        {
            dialogueText = text;
        }

        public void SetDialogueEndText(string[] text)
        {
            dialogueEndText = text;
        }
    }

    // 메뉴 아메샤 스펜타 상태창 조작법 설정
    public GameObject[] UIPanels;

    public Text typeExplain;
    public Text[] statusTexts;
    public Text[] ameshaWarrantTexts;
    public Text[] spentaWarrantTexts;

    public Text spentaWarrantExplainTitleText;
    public Text spentaWarrantExplainText;
    public GameObject[] spentaWarrantTpyePanels;
    int spentaWarrantNowContents;


    public GameObject statPanel;
    public GameObject[] stats;

    public int pointer = 0;

    public GameObject dialoguePanel;
    public Text dialogueText;

    public List<Dialogue> dialogueList = new List<Dialogue>();
    Dialogue nowDialogue;

    WarrantSystem ws;

    public float interactionRange;
    private void Start()
    {
        SetDialogue();

        ws = GetComponent<WarrantSystem>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            OpenStatus();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            ESC();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeStatus();
        }

        FindInteraction();
        SetDialogue();
    }

    public void FindInteraction()
    {
        if (dialoguePanel.activeSelf)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(GameManager.instance.Player.transform.position, interactionRange);

            foreach (Collider2D hit in hits)
            {
                Interaction inter = hit.GetComponent<Interaction>();
                if (inter != null)
                {
                    Debug.Log(inter.name);
                    hit.SendMessage("SendInteraction");
                    return;
                }
            }
        }


    }


    public void ESC()
    {
        foreach(GameObject panel in UIPanels.Reverse())
        {
            if (panel.activeSelf)
            {
                panel.SetActive(false);
                Time.timeScale = 1f;
                return;
            }
        }

        Time.timeScale = 0f;
        UIPanels[0].SetActive(true);
    }

    public void OpenStatus()
    {
        if (statPanel.activeSelf)
        {
            statPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;

            GetComponent<WarrantSystem>().SetMessageAmesha();
            GetComponent<WarrantSystem>().SetMessageSpenta();
            SetStatus();

            statPanel.SetActive(true);
            pointer = 0;
            SetSpentaWarrant();
        }
        
    }

    public void ChangeStatus()
    {
        if (statPanel.activeSelf)
        {
            switch (pointer) 
            {
                case 0:
                    stats[0].SetActive(false);
                    stats[1].SetActive(true);
                    SetAmeshaWarrant();
                    typeExplain.text = "아메샤의 권능";
                    break;

                case 1:
                    stats[1].SetActive(false);
                    stats[2].SetActive(true);
                    SetSpentaWarrant();
                    typeExplain.text = "스펜타의 권능";
                    break;

                case 2:
                    stats[2].SetActive(false);
                    stats[0].SetActive(true);
                    SetStatus();
                    typeExplain.text = "스탯";
                    break;

            }
            pointer = (pointer+1) % 3;
        }
    }

    public void GetStatus()
    {
        GameObject nowClick = EventSystem.current.currentSelectedGameObject;
        int num;
        num = int.Parse(nowClick.name.Split("_")[0]);

        for (int i = 0; i < stats.Length; i++)
        {
            if (i == num)
            {
                stats[i].SetActive(true);
            }
            else
            {
                stats[i].SetActive(false);
            }
        }
    }

    public void SetStatus()
    {
        PlayerStatus ps = GameManager.instance.Player.GetComponent<PlayerStatus>();

        statusTexts[0].text = ps.hp + "/" + ps.maxHp;
        statusTexts[1].text = ps.attackPower + "";
        statusTexts[2].text = Mathf.Floor(100 / ps.nonSkillCoolTime[2]) / 100 + " / s";
        statusTexts[3].text = ps.defense + "";

    }

    public void SetAmeshaWarrant()
    {
        WarrantSystem ws = GetComponent<WarrantSystem>();
        PlayerStatus ps = GameManager.instance.Player.GetComponent<PlayerStatus>();

        // 빛의 권능
        string explain = "";
        explain += "쿨타임 - " + ws.nowAmeshaWarrant[0].GetPointByInt(0) + "초\n";
        explain += "사거리 + " + ws.nowAmeshaWarrant[0].GetPointByInt(1) * 100 + "";

        if (ws.nowAmeshaWarrant[0].GetPointByInt(2) != 0)
        {
            explain += "\n사용 후 1초안에 적 처치 시 쿨타임 초기화";
        }
        ameshaWarrantTexts[0].text = explain;

        // 진리의 권능
        explain = "";
        explain += "지속시간 + " + ws.nowAmeshaWarrant[1].GetPointByInt(0) * 0.25 + "초\n";
        explain += "정확한 패링 타이밍 증가  " + ws.nowAmeshaWarrant[1].GetPointByInt(1) * 0.25 + "초";

        if (ws.nowAmeshaWarrant[1].GetPointByInt(2) != 0)
        {
            explain += "\n무적의 지속시간 1초 증가";
        }
        ameshaWarrantTexts[1].text = explain;

        // 생명의 권능
        explain = "";
        explain += "최대 체력 + " + ws.nowAmeshaWarrant[2].GetPointByInt(0) * 20 + "\n";
        explain += "보호막 최대치 + " + ws.nowAmeshaWarrant[2].GetPointByInt(1) * 10 + "";

        if (ws.nowAmeshaWarrant[2].GetPointByInt(2) != 0)
        {
            explain += "\n평온 상태 돌입까지 걸리는 시간 1초 감소";
        }
        ameshaWarrantTexts[2].text = explain;

        // 사랑의 권능
        explain = "";
        explain += "회복량 + " + ws.nowAmeshaWarrant[3].GetPointByInt(0) + "%\n";
        explain += "함정으로 입는 데미지 - " + ws.nowAmeshaWarrant[3].GetPointByInt(1) * 30 + "%";

        if (ws.nowAmeshaWarrant[3].GetPointByInt(2) != 0)
        {
            explain += "\n스펜타의 권능 공격에도 피해 흡수 적용";
        }
        ameshaWarrantTexts[3].text = explain;

        // 정의의 권능
        explain = "";
        explain += "공격력 + " + ws.nowAmeshaWarrant[4].GetPointByInt(0)*10 + "\n";
        explain += "공격 속도 + " + (2 - Mathf.Floor(100 / ps.nonSkillCoolTime[2]) / 100) + "";

        if (ws.nowAmeshaWarrant[4].GetPointByInt(2) != 0)
        {
            explain += "\n이동속도가 증가한다";
        }
        ameshaWarrantTexts[4].text = explain;
    }

    public void ClickSpentaWarrantTypeButton(int buttonIndex)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == buttonIndex)
            {
                spentaWarrantTpyePanels[i].SetActive(true);
            }
            else
            {
                spentaWarrantTpyePanels[i].SetActive(false);
            }
        }

        spentaWarrantExplainText.text = "";
        spentaWarrantExplainTitleText.text = "";
        spentaWarrantNowContents = -1;
 
    }

    public void ClickSpentaWarrantContentsButton(int buttonIndex)
    {
        spentaWarrantNowContents = buttonIndex;

        switch (buttonIndex)
        {
            case 11:
                spentaWarrantExplainTitleText.text = "살수의 전율";
                spentaWarrantExplainText.text = "적 처치시 쿨타임 초기화 (Max 1)\n" + ws.nowSpentaWarrant[0].advanced
                                                 + " / " + ws.maxSpentaWarrant[0].advanced;
                break;
            case 12:
                spentaWarrantExplainTitleText.text = "맹렬";
                spentaWarrantExplainText.text = "공격 대상 증가 + 1 (Max 2)\n" + ws.nowSpentaWarrant[0].intermediate
                                                 + " / " + ws.maxSpentaWarrant[0].intermediate;
                break;
            case 13:
                spentaWarrantExplainTitleText.text = "수련";
                spentaWarrantExplainText.text = "쿨타임 감소 - 0.5초 (Max 3)\n" + ws.nowSpentaWarrant[0].basic1
                                                 + " / " + ws.maxSpentaWarrant[0].basic1;
                break;
            case 14:
                spentaWarrantExplainTitleText.text = "투지";
                spentaWarrantExplainText.text = "계수 증가 + 0.33 (Max 3)\n" + ws.nowSpentaWarrant[0].basic2
                                                 + " / " + ws.maxSpentaWarrant[0].basic2;
                break;
            case 21:
                spentaWarrantExplainTitleText.text = "회심의 일격\n";
                spentaWarrantExplainText.text = "투사체 속도가 2배가 되고 계수가 1 증가한다 (Max 1)\n" + ws.nowSpentaWarrant[1].advanced
                                                 + " / " + ws.maxSpentaWarrant[1].advanced;
                break;
            case 22:
                spentaWarrantExplainTitleText.text = "탄성력\n";
                spentaWarrantExplainText.text = "사거리가 증가 + 250 (Max 2)\n" + ws.nowSpentaWarrant[1].intermediate
                                                 + " / " + ws.maxSpentaWarrant[1].intermediate;
                break;
            case 23:
                spentaWarrantExplainTitleText.text = "숙달\n";
                spentaWarrantExplainText.text = "쿨타임 감소 -0.7초 (Max 3)\n" + ws.nowSpentaWarrant[1].basic1
                                                 + " / " + ws.maxSpentaWarrant[1].basic1;
                break;
            case 24:
                spentaWarrantExplainTitleText.text = "눈썰미\n";
                spentaWarrantExplainText.text = "계수 증가 + 0.17 (Max 3)\n" + ws.nowSpentaWarrant[1].basic2
                                                 + " / " + ws.maxSpentaWarrant[1].basic2;
                break;
            case 31:
                spentaWarrantExplainTitleText.text = "원한의 불꽃\n";
                spentaWarrantExplainText.text = "3초동안 매 초마다 불꽃이 솟아오른다 (Max 1)\n" + ws.nowSpentaWarrant[2].advanced
                                                 + " / " + ws.maxSpentaWarrant[2].advanced;
                break;
            case 32:
                spentaWarrantExplainTitleText.text = "의지\n";
                spentaWarrantExplainText.text = "사거리가 증가 +200 (Max 2)\n" + ws.nowSpentaWarrant[2].intermediate
                                                 + " / " + ws.maxSpentaWarrant[2].intermediate;
                break;
            case 33:
                spentaWarrantExplainTitleText.text = "집중\n";
                spentaWarrantExplainText.text = "쿨타임 감소 - 0.5초 (Max 3)\n" + ws.nowSpentaWarrant[2].basic1
                                                 + " / " + ws.maxSpentaWarrant[2].basic1;
                break;
            case 34:
                spentaWarrantExplainTitleText.text = "확신\n";
                spentaWarrantExplainText.text = "계수 증가 + 0.1 (Max 3)\n" + ws.nowSpentaWarrant[2].basic2
                                                 + " / " + ws.maxSpentaWarrant[2].basic2;
                break;

            default:
                Debug.Log("Error");
                break;
        }
    }

    public void ClickSpentaWarrantReinfoceButton()
    {
        if (spentaWarrantNowContents == -1)
        {
            return;
        }

        ws.GetWarrant(spentaWarrantNowContents);

        ClickSpentaWarrantContentsButton(spentaWarrantNowContents);

    }
    public void SetSpentaWarrant()
    {
        // !! 변경 필요

        WarrantSystem ws = GetComponent<WarrantSystem>();
        PlayerStatus ps = GameManager.instance.Player.GetComponent<PlayerStatus>();

        // Bohuman
        string explain = "";
        explain += "쿨타임 - " + ws.nowSpentaWarrant[0].basic1 * 0.5 + "초\n";
        explain += "계수 + " + ws.nowSpentaWarrant[0].basic2 * 0.33 + "";
        explain += "공격 대상 증가 + " + ws.nowSpentaWarrant[0].intermediate + "";

        if (ws.nowSpentaWarrant[0].advanced != 0)
        {
            explain += "적 처치 시 쿨타임 초기화";
        }
        spentaWarrantTexts[0].text = explain;

        // Cassatra
        explain = "쿨타임 - " + ws.nowSpentaWarrant[1].basic1 * 0.7 + "초\n";
        explain += "계수 + " + ws.nowSpentaWarrant[1].basic2 * 0.7 + "\n";
        explain += "사거리 +  " + ws.nowSpentaWarrant[1].intermediate * 250 + "";

        if (ws.nowSpentaWarrant[1].advanced != 0)
        {
            explain += "\n무적의 지속시간 1초 증가";
        }
        spentaWarrantTexts[1].text = explain;

        // Asha
        explain = "";
        explain += "최대 체력 + " + ws.nowSpentaWarrant[2].GetPointByInt(0) * 20 + "\n";
        explain += "보호막 최대치 + " + ws.nowSpentaWarrant[2].GetPointByInt(1) * 10 + "";

        if (ws.nowSpentaWarrant[2].GetPointByInt(2) != 0)
        {
            explain += "\n평온 상태 돌입까지 걸리는 시간 1초 감소";
        }
        spentaWarrantTexts[2].text = explain;

        
    }

    public void OpenSetting()
    {
        UIPanels[5].SetActive(true);

    }

    public void OpenManual()
    {
        UIPanels[4].SetActive(true);
    }

    public void StartDialogue(string name)
    {
        foreach (var item in dialogueList) 
        {
            if (item.name == name)
            {
                nowDialogue = item;

                if (nowDialogue.pointer >= nowDialogue.dialogueText.Length)
                {
                    dialogueText.text = nowDialogue.dialogueEndText[nowDialogue.endPointer];
                    nowDialogue.endPointer++;
                    nowDialogue.endPointer %= nowDialogue.dialogueEndText.Length;
                    break;
                }

                dialogueText.text = nowDialogue.dialogueText[nowDialogue.pointer];
                break;
            }
        }

        Time.timeScale = 0f;
        dialoguePanel.SetActive(true);
    }

    public void EndDialogue()
    {
        Time.timeScale = 1f;
        Debug.Log("끝");
        dialoguePanel.SetActive(false);
    }

    public void SetDialogue()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (nowDialogue.pointer == 0)
            {
                nowDialogue.pointer++;
                return;
            }

            if (dialoguePanel.activeSelf)
            {
                
                if (nowDialogue.pointer < nowDialogue.dialogueText.Length)
                {
                    dialogueText.text = nowDialogue.dialogueText[nowDialogue.pointer];
                }
                else if (nowDialogue.pointer == nowDialogue.dialogueText.Length)
                {
                    EndDialogue();
                }
                else
                {
                    nowDialogue.pointer -= 2;
                }
                nowDialogue.pointer++;
            }

        }
    }

    public void SetDialogueText(Dialogue dialogueObject)
    {
        
    }
}
