using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    // ���� �������� ����
    public int stage;
    public Text stageText;
    // �������� �� ���� ��ġ (x,y)
    public float[] stageStartLoc;

    public List<int> randomList;
    public List<int> stageList;
    public int nowStage;

    public Image loadingImg;

    // ���� ������ ���� ��ġ
    public float manahLoc;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public void SetStage()
    {
        stageText.text = "1-" + stage;
    }

    public void NewGame()
    {
        GetComponent<UiManager>().ESC();
        GetComponent<SaveLoadMng>().Reset();

        SceneManager.LoadScene(8);
        nowStage = 0;

        GameManager.instance.Player.transform.position = new Vector3(-7.383516f , -7.115751f, 0);
    }

    public void GameStart()
    {
        GetComponent<SaveLoadMng>().JsonLoad();

        // ���� �ϱ�
        if (nowStage == 0)
        {
            while (randomList.Count > 0)
            {
                int randInt = Random.Range(0, randomList.Count);
                stageList.Add(randomList[randInt]);
                randomList.RemoveAt(randInt);
            };
        }
        // �̾� �ϱ�
        else
        {
            stageList.Insert(0, nowStage);
            stage--;
        }
        GameManager.instance.transform.GetChild(0).gameObject.SetActive(true);
        GameManager.instance.transform.GetChild(1).gameObject.SetActive(true);

        LoadScene();
    }

    public void LoadScene()
    {
        LoadingStart();

        // ó�� �� �� ���
        if (stage == -1)
        {
            stage++;
            SetStage();
            SceneManager.LoadScene(8);
            return;
        }

        // 3��° �� (���� ��������) �� ���
        if (stage == 2)
        {
            stage++;
            GameManager.instance.Player.transform.position = new Vector3(0, 0, 0);

            SetStage();
            SceneManager.LoadScene(9);
            return;
        }

        int nextStage = stageList[0];
        nowStage = stageList[0];
        Debug.Log(nextStage);
        stage++;
        stageList.RemoveAt(0);

        GameManager.instance.transform.GetChild(1).transform.position = new Vector3(stageStartLoc[2 * nextStage - 2], stageStartLoc[2 * nextStage - 1], 0);

        SetStage();
        SceneManager.LoadScene(nextStage);
    }

    public void LoadingStart()
    {
        loadingImg.gameObject.SetActive(true);

        Invoke("LoadingEnd", 0.5f);
    }

    public void LoadingEnd()
    {
        loadingImg.gameObject.SetActive(false);
    }
}
