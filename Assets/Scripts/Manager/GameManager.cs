using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float pain;
    public int score;
    public int gameLevel;
    public int stageStep;
    public int gameWave;
    public int doubleScore = 1;

    public float playerHpValue;
    public float playerPainValue;

    public bool[] isBossClear = new bool[2] { false, false };

    public string[] baseRankDataName = new string[] { "AAA", "BBB", "CCC", "DDD", "EEE" };
    public int[] baseRankScore = new int[] { 10000, 1000, 100, 10, 1 };

    public GameObject playerPrefab;
    public GameObject[] monsterPrefabs;
    public GameObject[] cellPrefabs;
    public GameObject[] bossPrefabs;

    public List<RankData> rankDatas = new List<RankData>();

    public static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        InitRank();
    }

    public void InitRank()
    {
        for (int i = 0; i < 5; i++) rankDatas.Add(new RankData(baseRankScore[i], baseRankDataName[i]));
        rankDatas.Sort((rankDatasA, rankDatasB) => rankDatasB.score.CompareTo(rankDatasA.score));
    }

    public void GameInit()
    {
        UIManager.instance.StartStage(stageStep);
        BackgroundSoundManager.instance.PlayFightBackgroundSound();
        StopAllCoroutines();
        StartCoroutine(SpawnWave());
        if (stageStep == 1)
        {
            score = 0;
            gameWave = 0;
        }
        Instantiate(playerPrefab, new Vector2(0, -4), transform.rotation);
        isBossClear[0] = false;
        isBossClear[1] = false;
        pain = 0;
        doubleScore = 1;
        AppendToPain(stageStep == 1 ? 0.1f : 0.3f);
        UIManager.instance.ChangeToPainSlider(pain);
        UIManager.instance.ChangeToScoreText(score);
        UIManager.instance.ChangeToWaveText(gameWave);
        Player.instance.Init();
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(3f);
        UIManager.instance.ChangeLeftBossWave((stageStep * 10) + (gameLevel * 10));
        gameWave++;
        for (; gameWave <= (stageStep * 10) + (gameLevel * 10); gameWave++)
        {
            UIManager.instance.ChangeLeftBossWave((stageStep * 10) + (gameLevel * 10 - gameWave));
            UIManager.instance.ChangeToWaveText(gameWave);
            for (int i = -4; i <= 4; i += 2) Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Length)], new Vector2(i, 6), transform.rotation);
            yield return new WaitForSeconds(0.5f);
            SpawnCell(Random.Range(0, cellPrefabs.Length));
            yield return new WaitForSeconds(3f);
        }
        Instantiate(bossPrefabs[stageStep - 1], new Vector2(0, 6), transform.rotation);
    }

    public void SpawnCell(int number)
    {
        Instantiate(cellPrefabs[number], new Vector2(Random.Range(-4, 4), 6), transform.rotation);
    }

    public void AppendToScore(int score)
    {
        this.score += (score * doubleScore);
        UIManager.instance.ChangeToScoreText(this.score);
    }

    public void AppendToPain(float painValue)
    {
        pain += painValue;
        if (pain >= 1) StartCoroutine(GameOver());
        UIManager.instance.ChangeToPainSlider(pain);
    }

    public void PainRecovery()
    {
        if (pain < 0.1)
        {
            pain = 0;
            return;
        }
        pain -= 0.1f;
    }

    public void ChangePain(float value)
    {
        pain = value;
        if (pain >= 1) StartCoroutine(GameOver());
        UIManager.instance.ChangeToPainSlider(pain);
    }

    public void OnDoubleScore()
    {
        StopCoroutine(ChangeToDoubleScore());
        StartCoroutine(ChangeToDoubleScore());
    }

    public IEnumerator ChangeToDoubleScore()
    {
        doubleScore = 2;
        yield return new WaitForSeconds(5f);
        doubleScore = 1;
    }

    public void BossClear(int value)
    {
        isBossClear[value] = true;
        if (isBossClear[0] && isBossClear[1]) StartCoroutine(StageClear());
    }

    public IEnumerator StageClear()
    {
        EffectSoundManager.instance.PlayOnStageClearClip();
        UIManager.instance.AllBossDead();
        yield return new WaitForSeconds(3f);
        Player.instance.hp = Player.instance.maxHp;
        if (stageStep == 2) GameClear();
        else
        {
            stageStep = 2;
            GameInit();
            Player.instance.Init();
        }
    }

    public void GameClear()
    {
        playerHpValue = Player.instance.hp;
        playerPainValue = pain;
        EffectSoundManager.instance.PlayOnGameClearClip();
        StopCoroutine(SpawnWave());
        foreach (var monster in GameObject.FindGameObjectsWithTag("Monster")) Destroy(monster);
        foreach (var bullet in GameObject.FindGameObjectsWithTag("MonsterBullet")) Destroy(bullet);
        SceneManager.LoadScene("RankScene");
    }

    public IEnumerator GameOver()
    {
        playerHpValue = Player.instance.hp;
        playerPainValue = pain;
        SceneManager.LoadScene("GameOverScene");
        EffectSoundManager.instance.PlayOnDeadSound();
        StopCoroutine(SpawnWave());
        foreach (var bullet in GameObject.FindGameObjectsWithTag("MonsterBullet")) Destroy(bullet);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("RankScene");
    }
}
