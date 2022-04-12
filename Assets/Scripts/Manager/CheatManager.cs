using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheatManager : MonoBehaviour
{
    public GameObject gameLevelSelectPanel;
    public GameObject playerHpGaigeChangePanel;
    public GameObject painGaigeChangePanel;

    public GameObject[] helpOtherPanels;

    public GameObject pausePanel;

    GameObject panel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) SpawnStageSelectPanel(1);
        else if (Input.GetKeyDown(KeyCode.Alpha1)) SpawnStageSelectPanel(2);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) OnCheatGodMode(true);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) OnCheatGodMode(false);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) MonsterAllClear();
        else if (Input.GetKeyDown(KeyCode.Alpha5)) ChangeHpGaige();
        else if (Input.GetKeyDown(KeyCode.Alpha6)) ChangePainGaige();
        else if (Input.GetKeyDown(KeyCode.Alpha7)) SpawnCell(0);
        else if (Input.GetKeyDown(KeyCode.Alpha8)) SpawnCell(1);
        else if (Input.GetKeyDown(KeyCode.Alpha9)) OnSkipButton();
        else if (Input.GetKeyDown(KeyCode.Escape)) OnPauseButton();
    }

    public void SpawnStageSelectPanel(int num)
    {
        Time.timeScale = 0;
        GameObject canvas = Instantiate(gameLevelSelectPanel, transform.position, transform.rotation);
        canvas.transform.SetParent(GameObject.Find("Canvas").transform, false);
        GameManager.instance.stageStep = num;
    }

    public void OnCheatGodMode(bool value)
    {
        Player.instance.isCheatGodMode = value;
    }

    public void MonsterAllClear()
    {
        foreach (var monster in GameObject.FindGameObjectsWithTag("Monster")) Destroy(monster);
    }

    public void ChangeHpGaige()
    {
        Time.timeScale = 0;
        GameObject canvas = Instantiate(playerHpGaigeChangePanel, transform.position, transform.rotation);
        canvas.transform.SetParent(GameObject.Find("Canvas").transform, false);
    }

    public void ChangePainGaige()
    {
        GameObject canvas = Instantiate(painGaigeChangePanel, transform.position, transform.rotation);
        canvas.transform.SetParent(GameObject.Find("Canvas").transform, false);
    }

    public void SpawnCell(int num)
    {
        GameManager.instance.SpawnCell(num);
    }

    public void OnSkipButton()
    {
        GameManager.instance.gameWave = (GameManager.instance.stageStep * 10) + (GameManager.instance.gameLevel * 10);
        UIManager.instance.ChangeLeftBossWave((GameManager.instance.stageStep * 10) + (GameManager.instance.gameLevel * 10 - GameManager.instance.gameWave));
        UIManager.instance.ChangeToWaveText(GameManager.instance.gameWave);
    }

    public void OnPauseButton()
    {
        if (Time.timeScale == 0)
        {
            Destroy(panel);
            Time.timeScale = 1;
        }
        else
        {
            panel = Instantiate(pausePanel, transform.position, transform.rotation);
            panel.transform.SetParent(GameObject.Find("Canvas").transform, false);
            Time.timeScale = 0;
        }
    }
}
