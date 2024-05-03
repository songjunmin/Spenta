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

    // 설정
    public GameObject[] UIPanels;

    public Text typeExplain;
    public Text[] statusTexts;
    public Text[] ameshaWarrantTexts;
    public Text[] spentaWarrantTexts;


    public GameObject statPanel;
    public GameObject[] stats;

    public int pointer = 0;

    public GameObject dialoguePanel;
    public Text dialogueText;

    public List<Dialogue> dialogueList = new List<Dialogue>();
    Dialogue nowDialogue;


    public float interactionRange;
    private void Start()
    {
        SetDialogue();
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
            explain += "\n스킬 사용 후 1초 이내에 적 처치 시 쿨타임 초기화";
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

    public void SetSpentaWarrant()
    {
        WarrantSystem ws = GetComponent<WarrantSystem>();
        PlayerStatus ps = GameManager.instance.Player.GetComponent<PlayerStatus>();

        // 보후만
        string explain = "";
        explain += "쿨타임 - " + ws.nowSpentaWarrant[0].basic1 * 0.5f + "초\n";
        explain += "공격 계수 + " + ws.nowSpentaWarrant[0].basic2 * .33f + "\n";
        explain += "공격 대상 + " + ws.nowSpentaWarrant[0].intermediate + "";

        if (ws.nowSpentaWarrant[0].advanced != 0)
        {
            explain += "\n적 처치시 쿨타임 초기화";
        }
        spentaWarrantTexts[0].text = explain;

        // 카사트라
        explain = "";
        explain += "쿨타임 - " + ws.nowSpentaWarrant[1].basic1 * 0.7f + "초\n";
        explain += "공격 계수 + " + ws.nowSpentaWarrant[1].basic2 * .17f + "\n";
        explain += "사정 거리 + " + ws.nowSpentaWarrant[1].intermediate * 250+ "";

        if (ws.nowSpentaWarrant[1].advanced != 0)
        {
            explain += "\n창이 2배 빠른 속도로 날아감, 창의 데미지가 2배가 됨";
        }
        spentaWarrantTexts[1].text = explain;

        // 아샤
        explain = "";
        explain += "쿨타임 - " + ws.nowSpentaWarrant[2].basic1 * 0.5f + "초\n";
        explain += "공격 계수 + " + ws.nowSpentaWarrant[2].basic2 * .1f + "\n";
        explain += "사정 거리 + " + ws.nowSpentaWarrant[2].intermediate * 200 + "";

        if (ws.nowSpentaWarrant[2].advanced != 0)
        {
            explain += "\n지속적으로 불길이 솟아오름";
        }
        spentaWarrantTexts[2].text = explain;
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
            if (dialoguePanel.activeSelf)
            {
                nowDialogue.pointer++;

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
            }

        }
    }

    public void SetDialogueText(Dialogue dialogueObject)
    {
        
    }
}
