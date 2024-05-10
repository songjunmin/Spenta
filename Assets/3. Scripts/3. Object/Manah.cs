using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manah : MonoBehaviour
{
    void Start()
    {
        SetDialogueText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SendInteraction()
    {
        GameManager.instance.GetComponent<UiManager>().StartDialogue(gameObject.name);
    }


    public void SetDialogueText()
    {
        UiManager.Dialogue dialogue = new UiManager.Dialogue("Manah");

        // ������
        string[] dialogueText = new string[2];

        dialogueText[0] = "�����ַ� �Ա���! �ʿ��Լ� ����Ÿ���� ���� ��������";
        dialogueText[1] = "�����༭ ����, ���� ���� �Ҳ��� ���� �Ϻ� �����ٰ�.";


        dialogue.SetDialogueText(dialogueText);


        string[] dialogueEndText = new string[3];
        dialogueEndText[0] = "���� �̰ܳ��� ���� ����ġ�⸦��";
        dialogueEndText[1] = "����Ÿ ���� ���Ͽ�..!";
        dialogueEndText[2] = "�ٿ��ٸ� �����ϰ� ���� �����ϸ���!";

        dialogue.SetDialogueEndText(dialogueEndText);

        GameManager.instance.GetComponent<UiManager>().dialogueList.Add(dialogue);
    }
}
