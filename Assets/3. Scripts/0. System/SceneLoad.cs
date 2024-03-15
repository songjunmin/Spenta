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

        if (stageList.Count == 0 && stage == 1)
        {
            while (randomList.Count > 0)
            {
                int randInt = Random.Range(0, randomList.Count);
                stageList.Add(randomList[randInt]);
                randomList.RemoveAt(randInt);
            }
            stage--;
        }

        LoadScene();
    }

    public void LoadScene()
    {
        int nextStage = stageList[0];
        Debug.Log(nextStage);
        stage++;
        stageList.RemoveAt(0);

        GameManager.instance.transform.GetChild(0).gameObject.SetActive(true);
        GameManager.instance.transform.GetChild(1).gameObject.SetActive(true);
        GameManager.instance.transform.GetChild(1).transform.position = new Vector3(stageStartLoc[2 * nextStage - 2], stageStartLoc[2 * nextStage - 1], 0);

        SetStage();
        SceneManager.LoadScene(nextStage);
    }
}
