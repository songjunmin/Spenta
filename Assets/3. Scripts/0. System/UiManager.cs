using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject statPanel;
    public GameObject[] stats;
    public Text statusText;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            OpenStatus();
        }
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
        string txt = "";
       
        txt = txt + "HP            : " + ps.hp + "/" + ps.maxHp + "\n";
        txt = txt + "���ݷ�      : " + ps.attackPower + "\n";
        txt = txt + "���� �ӵ� : " + ps.attackSpeed + "\n";
        txt = txt + "����      : " + ps.defense ;

        txt = string.Format(txt, "HP", ps.hp, ps.maxHp, "attack power", ps.attackPower, "���� �ӵ�", ps.attackSpeed, "����", ps.defense);
        statusText.text = txt;
    }

}
