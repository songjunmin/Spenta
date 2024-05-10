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

    // �޴� �Ƹ޻� ����Ÿ ����â ���۹� ����
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
                    typeExplain.text = "�Ƹ޻��� �Ǵ�";
                    break;

                case 1:
                    stats[1].SetActive(false);
                    stats[2].SetActive(true);
                    SetSpentaWarrant();
                    typeExplain.text = "����Ÿ�� �Ǵ�";
                    break;

                case 2:
                    stats[2].SetActive(false);
                    stats[0].SetActive(true);
                    SetStatus();
                    typeExplain.text = "����";
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

        // ���� �Ǵ�
        string explain = "";
        explain += "��Ÿ�� - " + ws.nowAmeshaWarrant[0].GetPointByInt(0) + "��\n";
        explain += "��Ÿ� + " + ws.nowAmeshaWarrant[0].GetPointByInt(1) * 100 + "";

        if (ws.nowAmeshaWarrant[0].GetPointByInt(2) != 0)
        {
            explain += "\n��� �� 1�ʾȿ� �� óġ �� ��Ÿ�� �ʱ�ȭ";
        }
        ameshaWarrantTexts[0].text = explain;

        // ������ �Ǵ�
        explain = "";
        explain += "���ӽð� + " + ws.nowAmeshaWarrant[1].GetPointByInt(0) * 0.25 + "��\n";
        explain += "��Ȯ�� �и� Ÿ�̹� ����  " + ws.nowAmeshaWarrant[1].GetPointByInt(1) * 0.25 + "��";

        if (ws.nowAmeshaWarrant[1].GetPointByInt(2) != 0)
        {
            explain += "\n������ ���ӽð� 1�� ����";
        }
        ameshaWarrantTexts[1].text = explain;

        // ������ �Ǵ�
        explain = "";
        explain += "�ִ� ü�� + " + ws.nowAmeshaWarrant[2].GetPointByInt(0) * 20 + "\n";
        explain += "��ȣ�� �ִ�ġ + " + ws.nowAmeshaWarrant[2].GetPointByInt(1) * 10 + "";

        if (ws.nowAmeshaWarrant[2].GetPointByInt(2) != 0)
        {
            explain += "\n��� ���� ���Ա��� �ɸ��� �ð� 1�� ����";
        }
        ameshaWarrantTexts[2].text = explain;

        // ����� �Ǵ�
        explain = "";
        explain += "ȸ���� + " + ws.nowAmeshaWarrant[3].GetPointByInt(0) + "%\n";
        explain += "�������� �Դ� ������ - " + ws.nowAmeshaWarrant[3].GetPointByInt(1) * 30 + "%";

        if (ws.nowAmeshaWarrant[3].GetPointByInt(2) != 0)
        {
            explain += "\n����Ÿ�� �Ǵ� ���ݿ��� ���� ��� ����";
        }
        ameshaWarrantTexts[3].text = explain;

        // ������ �Ǵ�
        explain = "";
        explain += "���ݷ� + " + ws.nowAmeshaWarrant[4].GetPointByInt(0)*10 + "\n";
        explain += "���� �ӵ� + " + (2 - Mathf.Floor(100 / ps.nonSkillCoolTime[2]) / 100) + "";

        if (ws.nowAmeshaWarrant[4].GetPointByInt(2) != 0)
        {
            explain += "\n�̵��ӵ��� �����Ѵ�";
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
                spentaWarrantExplainTitleText.text = "����� ����";
                spentaWarrantExplainText.text = "�� óġ�� ��Ÿ�� �ʱ�ȭ (Max 1)\n" + ws.nowSpentaWarrant[0].advanced
                                                 + " / " + ws.maxSpentaWarrant[0].advanced;
                break;
            case 12:
                spentaWarrantExplainTitleText.text = "�ͷ�";
                spentaWarrantExplainText.text = "���� ��� ���� + 1 (Max 2)\n" + ws.nowSpentaWarrant[0].intermediate
                                                 + " / " + ws.maxSpentaWarrant[0].intermediate;
                break;
            case 13:
                spentaWarrantExplainTitleText.text = "����";
                spentaWarrantExplainText.text = "��Ÿ�� ���� - 0.5�� (Max 3)\n" + ws.nowSpentaWarrant[0].basic1
                                                 + " / " + ws.maxSpentaWarrant[0].basic1;
                break;
            case 14:
                spentaWarrantExplainTitleText.text = "����";
                spentaWarrantExplainText.text = "��� ���� + 0.33 (Max 3)\n" + ws.nowSpentaWarrant[0].basic2
                                                 + " / " + ws.maxSpentaWarrant[0].basic2;
                break;
            case 21:
                spentaWarrantExplainTitleText.text = "ȸ���� �ϰ�\n";
                spentaWarrantExplainText.text = "����ü �ӵ��� 2�谡 �ǰ� ����� 1 �����Ѵ� (Max 1)\n" + ws.nowSpentaWarrant[1].advanced
                                                 + " / " + ws.maxSpentaWarrant[1].advanced;
                break;
            case 22:
                spentaWarrantExplainTitleText.text = "ź����\n";
                spentaWarrantExplainText.text = "��Ÿ��� ���� + 250 (Max 2)\n" + ws.nowSpentaWarrant[1].intermediate
                                                 + " / " + ws.maxSpentaWarrant[1].intermediate;
                break;
            case 23:
                spentaWarrantExplainTitleText.text = "����\n";
                spentaWarrantExplainText.text = "��Ÿ�� ���� -0.7�� (Max 3)\n" + ws.nowSpentaWarrant[1].basic1
                                                 + " / " + ws.maxSpentaWarrant[1].basic1;
                break;
            case 24:
                spentaWarrantExplainTitleText.text = "�����\n";
                spentaWarrantExplainText.text = "��� ���� + 0.17 (Max 3)\n" + ws.nowSpentaWarrant[1].basic2
                                                 + " / " + ws.maxSpentaWarrant[1].basic2;
                break;
            case 31:
                spentaWarrantExplainTitleText.text = "������ �Ҳ�\n";
                spentaWarrantExplainText.text = "3�ʵ��� �� �ʸ��� �Ҳ��� �ھƿ����� (Max 1)\n" + ws.nowSpentaWarrant[2].advanced
                                                 + " / " + ws.maxSpentaWarrant[2].advanced;
                break;
            case 32:
                spentaWarrantExplainTitleText.text = "����\n";
                spentaWarrantExplainText.text = "��Ÿ��� ���� +200 (Max 2)\n" + ws.nowSpentaWarrant[2].intermediate
                                                 + " / " + ws.maxSpentaWarrant[2].intermediate;
                break;
            case 33:
                spentaWarrantExplainTitleText.text = "����\n";
                spentaWarrantExplainText.text = "��Ÿ�� ���� - 0.5�� (Max 3)\n" + ws.nowSpentaWarrant[2].basic1
                                                 + " / " + ws.maxSpentaWarrant[2].basic1;
                break;
            case 34:
                spentaWarrantExplainTitleText.text = "Ȯ��\n";
                spentaWarrantExplainText.text = "��� ���� + 0.1 (Max 3)\n" + ws.nowSpentaWarrant[2].basic2
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
        // !! ���� �ʿ�

        WarrantSystem ws = GetComponent<WarrantSystem>();
        PlayerStatus ps = GameManager.instance.Player.GetComponent<PlayerStatus>();

        // Bohuman
        string explain = "";
        explain += "��Ÿ�� - " + ws.nowSpentaWarrant[0].basic1 * 0.5 + "��\n";
        explain += "��� + " + ws.nowSpentaWarrant[0].basic2 * 0.33 + "";
        explain += "���� ��� ���� + " + ws.nowSpentaWarrant[0].intermediate + "";

        if (ws.nowSpentaWarrant[0].advanced != 0)
        {
            explain += "�� óġ �� ��Ÿ�� �ʱ�ȭ";
        }
        spentaWarrantTexts[0].text = explain;

        // Cassatra
        explain = "��Ÿ�� - " + ws.nowSpentaWarrant[1].basic1 * 0.7 + "��\n";
        explain += "��� + " + ws.nowSpentaWarrant[1].basic2 * 0.7 + "\n";
        explain += "��Ÿ� +  " + ws.nowSpentaWarrant[1].intermediate * 250 + "";

        if (ws.nowSpentaWarrant[1].advanced != 0)
        {
            explain += "\n������ ���ӽð� 1�� ����";
        }
        spentaWarrantTexts[1].text = explain;

        // Asha
        explain = "";
        explain += "�ִ� ü�� + " + ws.nowSpentaWarrant[2].GetPointByInt(0) * 20 + "\n";
        explain += "��ȣ�� �ִ�ġ + " + ws.nowSpentaWarrant[2].GetPointByInt(1) * 10 + "";

        if (ws.nowSpentaWarrant[2].GetPointByInt(2) != 0)
        {
            explain += "\n��� ���� ���Ա��� �ɸ��� �ð� 1�� ����";
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
        Debug.Log("��");
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
