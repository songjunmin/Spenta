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

        // 피닉스
        string[] dialogueText = new string[9];

        dialogueText[0] = "샤오샨츠야, 나는 아그라 마이뉴에게 패배하여 힘을 잃고 여기에 갇혀있다.";
        dialogueText[1]= "너는 내 분신이자 내 힘의 편린이다.\n너는 나와 같으니, 어둠의 정령 다에바들을\n물리치고 나의 힘을 되찾아다오.\n나의 힘이 돌아올 수록 너 또한 강해진다.";
        dialogueText[2]= "붙잡힌 내 휘하의 아메샤 스펜타들을 만난다면\n그들이 풀려나 힘들 되찾도록 도와다오.";
        dialogueText[3]= "너는 나의 힘, 권능을 어느정도 다를 수 있다.";
        dialogueText[4]= "선한 생각에서 오는 강한 힘 '보후만'은 너의 힘을 순간적으로\n증폭하여 강하게 공격할 수 있다.";
        dialogueText[5]= "나의 영적인 힘 '카사트라'는 모든 물체를 관통하며 너의 적들도\n무자비하게 관통 할 수 있을 것이야.";
        dialogueText[6]= "질서와 정의에서 오는 힘 '아샤'를 통해 적들 여럿에게 불길로서\n심판 할 수도 있다.";
        dialogueText[7]= "마지막으로 궁극적 진리 '아르마이티'를 깨달아 잠시동안 신격 미트라로\n현신 할 수 있다.";
        dialogueText[8]= "이곳을 떠나기 전에 한 번 권능을 시험해보며 익숙해지고 떠나는\n것도 좋은 방법이지.";


        dialogue.SetDialogueText(dialogueText);


        string[] dialogueEndText = new string[1];
        dialogueEndText[0] = "부디 무운을 빈다…";

        dialogue.SetDialogueEndText(dialogueEndText);

        GameManager.instance.GetComponent<UiManager>().dialogueList.Add(dialogue);
    }
}
