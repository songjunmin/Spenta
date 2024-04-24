using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    // 현재 스테이지 정보
    public int stage;
    public Text stageText;
    // 스테이지 별 시작 위치 (x,y)
    public float[] stageStartLoc;

    public List<int> randomList;
    public List<int> stageList;
    public int nowStage;

    public Image loadingImg;

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

    public void GameStart()
    {
        GetComponent<SaveLoadMng>().JsonLoad();

        if (nowStage == 0)
        {
            while (randomList.Count > 0)
            {
                int randInt = Random.Range(0, randomList.Count);
                stageList.Add(randomList[randInt]);
                randomList.RemoveAt(randInt);
            };
        }
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

        if (stage == -1)
        {
            stage++;
            SetStage();
            SceneManager.LoadScene(8);
            return;
        }

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
