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

        // 마나흐
        string[] dialogueText = new string[2];

        dialogueText[0] = "구해주러 왔구나! 너에게서 스펜타님의 힘이 느껴져…";
        dialogueText[1] = "도와줘서 고마워, 내가 가진 불꽃의 힘을 일부 나눠줄게.";


        dialogue.SetDialogueText(dialogueText);


        string[] dialogueEndText = new string[3];
        dialogueEndText[0] = "고난을 이겨내고 악을 물리치기를…";
        dialogueEndText[1] = "스펜타 님을 위하여..!";
        dialogueEndText[2] = "다에바를 도륙하고 악을 심판하리라!";

        dialogue.SetDialogueEndText(dialogueEndText);

        GameManager.instance.GetComponent<UiManager>().dialogueList.Add(dialogue);
    }
}
