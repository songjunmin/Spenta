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
        txt = txt + "공격력      : " + ps.attackPower + "\n";
        txt = txt + "공격 속도 : " + ps.attackSpeed + "\n";
        txt = txt + "방어력      : " + ps.defense ;

        txt = string.Format(txt, "HP", ps.hp, ps.maxHp, "attack power", ps.attackPower, "공격 속도", ps.attackSpeed, "방어력", ps.defense);
        statusText.text = txt;
    }

}
