using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phoenix : MonoBehaviour
{
    // Start is called before the first frame update
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
        UiManager.Dialogue dialogue = new UiManager.Dialogue("Phoenix");

        // �Ǵн�
        string[] dialogueText = new string[9];

        dialogueText[0] = "����������, ���� �Ʊ׶� ���̴����� �й��Ͽ� ���� �Ұ� ���⿡ �����ִ�.";
        dialogueText[1]= "�ʴ� �� �н����� �� ���� ���̴�.\n�ʴ� ���� ������, ����� ���� �ٿ��ٵ���\n����ġ�� ���� ���� ��ã�ƴٿ�.\n���� ���� ���ƿ� ���� �� ���� ��������.";
        dialogueText[2]= "������ �� ������ �Ƹ޻� ����Ÿ���� �����ٸ�\n�׵��� Ǯ���� ���� ��ã���� ���ʹٿ�.";
        dialogueText[3]= "�ʴ� ���� ��, �Ǵ��� ������� �ٸ� �� �ִ�.";
        dialogueText[4]= "���� �������� ���� ���� �� '���ĸ�'�� ���� ���� ����������\n�����Ͽ� ���ϰ� ������ �� �ִ�.";
        dialogueText[5]= "���� ������ �� 'ī��Ʈ��'�� ��� ��ü�� �����ϸ� ���� ���鵵\n���ں��ϰ� ���� �� �� ���� ���̾�.";
        dialogueText[6]= "������ ���ǿ��� ���� �� '�ƻ�'�� ���� ���� �������� �ұ�μ�\n���� �� ���� �ִ�.";
        dialogueText[7]= "���������� �ñ��� ���� '�Ƹ�����Ƽ'�� ���޾� ��õ��� �Ű� ��Ʈ���\n���� �� �� �ִ�.";
        dialogueText[8]= "�̰��� ������ ���� �� �� �Ǵ��� �����غ��� �ͼ������� ������\n�͵� ���� �������.";


        dialogue.SetDialogueText(dialogueText);


        string[] dialogueEndText = new string[1];
        dialogueEndText[0] = "�ε� ������ ��١�";

        dialogue.SetDialogueEndText(dialogueEndText);

        GameManager.instance.GetComponent<UiManager>().dialogueList.Add(dialogue);
    }
}
