using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // for test 1,2 : 재화 획득 / 7,8 : 체력 변동

    public static GameManager instance;
    // GameManager.instance.~

    public GameObject Player;

    // 깨달음의 조각
    public float pieceOfEnlightenment;

    // 지식의 불꽃
    public float sparkOfKnowledge;

    public Image hpBar;

    public Text pieceOfEnlightenmentText;
    public Text sparkOfKnowledgeText;

    public GameObject warrantOfSpentaPanel;
    public GameObject warrantOfAmeshaPanel;

    public float interactionRange;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    // 인스턴스에 나 할당

            DontDestroyOnLoad(gameObject);
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
    }

    void Update()
    {
        Test();
        FindInteraction();

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (warrantOfSpentaPanel.activeSelf)
            {
                warrantOfSpentaPanel.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                warrantOfSpentaPanel.SetActive(true);
                gameObject.GetComponentInChildren<WarrantSystem>().SetMessageSpenta();
                Time.timeScale = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (warrantOfAmeshaPanel.activeSelf)
            {
                warrantOfAmeshaPanel.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                warrantOfAmeshaPanel.SetActive(true);
                gameObject.GetComponentInChildren<WarrantSystem>().SetMessageAmesha();
                Time.timeScale = 0f;
            }
        }
    }

    public void GetItem()
    {
        pieceOfEnlightenmentText.text = pieceOfEnlightenment.ToString();
        sparkOfKnowledgeText.text = sparkOfKnowledge.ToString();
    }

    public void ChangeHp()
    {
        hpBar.fillAmount = Player.GetComponent<PlayerStatus>().hp / Player.GetComponent<PlayerStatus>().maxHp;
    }

    public void Test()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            pieceOfEnlightenment++;
            GetItem();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            sparkOfKnowledge++;
            GetItem();
        }
       
    }

    public void FindInteraction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("실행");
            Collider2D[] hits = Physics2D.OverlapCircleAll(Player.transform.position, interactionRange);

            foreach (Collider2D hit in hits)
            {
                Interaction inter = hit.GetComponent<Interaction>();
                if (inter != null)
                {
                    Debug.Log("찾음");
                    hit.SendMessage("SendInteraction");
                    return;
                }
            }
        }

        
    }
}
