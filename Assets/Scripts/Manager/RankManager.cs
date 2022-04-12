using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    public List<RankData> rankDatas = new List<RankData>();

    public int score;
    public string userName;

    public Text[] rankScoreText;
    public Text yourRankScoreText;
    public Text playerHpScoreText;
    public Text playerPainScoreText;

    public static RankManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerHpScoreText.text = "HP    Score : " + Mathf.Round(GameManager.instance.playerHpValue * 1000);
        playerPainScoreText.text = "Pain Score : " + (10000 - Mathf.Round(GameManager.instance.playerPainValue * 10000));
        score += (int)Mathf.Round(GameManager.instance.playerHpValue * 1000) + (int)(10000 - Mathf.Round(GameManager.instance.playerPainValue * 10000));
        rankDatas = GameManager.instance.rankDatas;
        score += GameManager.instance.score;
        InitRankText();
    }

    void InitRankText()
    {
        yourRankScoreText.text = "Your Score : " + score.ToString();
    }

    public void OnRegisterButton(string userName)
    {
        ChangeRankList(userName, score);
    }

    public void ChangeRankList(string userName, int score)
    {
        rankDatas.Add(new RankData(score, userName));
        rankDatas.Sort((rankDatasA, rankDatasB) => rankDatasB.score.CompareTo(rankDatasA.score));
        GameManager.instance.stageStep = 1;
        for (int i = 0; i < 5; i++) rankScoreText[i].text = (i + 1) + "µî : " + rankDatas[i].name + " " + rankDatas[i].score;
        GameManager.instance.rankDatas = rankDatas;
    }
}
