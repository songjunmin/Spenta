using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // for test 1,2 : ��ȭ ȹ�� / 7,8 : ü�� ����

    public static GameManager instance;
    // GameManager.instance.~

    public GameObject Player;

    // �������� ����
    public float pieceOfEnlightenment;

    // ������ �Ҳ�
    public float sparkOfKnowledge;

    public Image hpBar;

    public Text pieceOfEnlightenmentText;
    public Text sparkOfKnowledgeText;

    public GameObject warrantPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    // �ν��Ͻ��� �� �Ҵ�

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

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (warrantPanel.activeSelf)
            {
                warrantPanel.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                warrantPanel.SetActive(true);
                gameObject.GetComponentInChildren<WarrantSystem>().SetMessage();
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
}
