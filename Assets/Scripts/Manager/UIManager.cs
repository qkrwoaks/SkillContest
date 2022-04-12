using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text hpValueText;
    public Text painValueText;
    public Text scoreText;
    public Text waveText;
    public Text currentItemNameText;
    public Text bossNameText;
    public Text[] miniBossNameText;
    public Text bossWaveText;

    public Image currentItemImage;

    public Slider hpSlider;
    public Slider painSlider;
    public Slider bossSlider;
    public Slider[] miniBossSlider;

    public GameObject bossPanel;
    public GameObject[] stagePanel;

    public GameObject[] backgrounds;

    public static UIManager instance;


    void Start()
    {
        instance = this;
        GameManager.instance.GameInit();

        ChangeToWaveText(GameManager.instance.gameWave);
        ChangeToScoreText(GameManager.instance.score);
    }

    public void ChangeToHpSlider(float value)
    {
        hpValueText.text = Mathf.Round(value * 100).ToString() + "%";
        hpSlider.value = value;  
    }

    public void ChangeToPainSlider(float value)
    {
        painValueText.text = Mathf.Round(value * 100).ToString() + "%";
        painSlider.value = value;
    }

    public void ChangeToScoreText(int value)
    {
        scoreText.text = "Score : " + value.ToString();
    }
    
    public void ChangeToWaveText(int value)
    {
        waveText.text = "Wave  : "+ value.ToString();
    }

    public void ChangeToCurrentItem(Color itemColor, string itemName)
    {
        currentItemImage.color = itemColor;
        currentItemNameText.text = itemName;
    }

    public void BossSpawn(string bossName)
    {
        bossPanel.SetActive(true);
        bossSlider.gameObject.SetActive(true);
        bossSlider.value = 1;
        miniBossSlider[0].gameObject.SetActive(false);
        miniBossSlider[1].gameObject.SetActive(false);
        bossNameText.text = bossName;
    }

    public void MiniBossSpawn(int num, string miniBossName)
    {
        miniBossSlider[num].gameObject.SetActive(true);
        miniBossSlider[num].value = 1;
        bossSlider.gameObject.SetActive(false);
        miniBossNameText[num].text = miniBossName;
    }

    public void MiniBossDead(int num)
    {
        miniBossSlider[num].gameObject.SetActive(false);
    }

    public void AllBossDead()
    {
        bossPanel.SetActive(false);
    }

    public void ChangeToBossSlider(float maxHp, float hp)
    {
        bossSlider.value = hp / maxHp;
    }

    public void ChangeToMiniBossSlider(float maxHp, float hp, int num)
    {
        miniBossSlider[num].value = hp / maxHp;
    }

    public void ChangeLeftBossWave(int value)
    {
        bossWaveText.text = value.ToString();
    }

    public void StartStage(int num)
    {
        GameObject startStage = Instantiate(stagePanel[num - 1], transform.position, transform.rotation);
        startStage.transform.SetParent(GameObject.Find("Canvas").transform, false);
    }
}
