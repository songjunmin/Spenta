using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoroaster : MonoBehaviour
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
        UiManager.Dialogue dialogue = new UiManager.Dialogue("Zoroaster");

        // �Ǵн�
        string[] dialogueText = new string[4];

        dialogueText[0] = "����, �ڳװ� �ٷ� ������ ȭ���̷α�.";
        dialogueText[1] = "�̰����� �ɵ��� ���� ���ڳ�.";
        dialogueText[2] = "���� �ִ� �������� ��ں��� �ٶ󺸸� �ѹ� ����� ��� ���� �͵� ���ڱ�.";
        dialogueText[3] = "�ڳ��� ������ ���� ��ȣ�� �ֱ⸦��";


        dialogue.SetDialogueText(dialogueText);


        string[] dialogueEndText = new string[1];
        dialogueEndText[0] = "�ڳ��� ������ ���� ��ȣ�� �ֱ⸦��";

        dialogue.SetDialogueEndText(dialogueEndText);

        GameManager.instance.GetComponent<UiManager>().dialogueList.Add(dialogue);

    }
}
