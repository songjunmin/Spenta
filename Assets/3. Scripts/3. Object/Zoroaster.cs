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

        // 피닉스
        string[] dialogueText = new string[4];

        dialogueText[0] = "허허, 자네가 바로 예언의 화신이로군.";
        dialogueText[1] = "이곳에서 심득을 얻어가면 좋겠네.";
        dialogueText[2] = "저기 있는 깨달음의 모닥불을 바라보며 한번 사색에 잠겨 보는 것도 좋겠군.";
        dialogueText[3] = "자네의 여정에 불의 가호가 있기를…";


        dialogue.SetDialogueText(dialogueText);


        string[] dialogueEndText = new string[1];
        dialogueEndText[0] = "자네의 여정에 불의 가호가 있기를…";

        dialogue.SetDialogueEndText(dialogueEndText);

        GameManager.instance.GetComponent<UiManager>().dialogueList.Add(dialogue);

    }
}
