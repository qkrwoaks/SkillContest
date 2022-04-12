using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public InputField userNameInputField;
    public InputField changePlayerHpInputField;
    public InputField changePainInputField;

    public GameObject inputPanel;
    public GameObject showRankPanel;
    public GameObject changeHpPanel;
    public GameObject changePainPanel;

    public GameObject[] helpPanels;

    public void OnStartButton()
    {
        EffectSoundManager.instance.PlayButtonSound();
        SceneManager.LoadScene("SelectModeScene");
    }

    public void StartGameScene(int level)
    {
        Time.timeScale = 1;
        EffectSoundManager.instance.PlayButtonSound();
        GameManager.instance.gameLevel = level;
        GameManager.instance.gameWave = 0;
        GameManager.instance.score = 0;
        SceneManager.LoadScene("GameScene");
    }


    public void OnNameRegisterButton()
    {
        EffectSoundManager.instance.PlayButtonSound();
        inputPanel.SetActive(false);
        RankManager.instance.OnRegisterButton(userNameInputField.text);
        showRankPanel.SetActive(true);
    }

    public void OnHpRegisterButton()
    {
        EffectSoundManager.instance.PlayButtonSound();
        Player.instance.ChangeHP(float.Parse(changePlayerHpInputField.text));
        changeHpPanel.SetActive(false);
    }

    public void OnPainRegisterButton()
    {
        EffectSoundManager.instance.PlayButtonSound();
        changePainPanel.SetActive(false);
        GameManager.instance.ChangePain(float.Parse(changePainInputField.text));
    }

    public void OnHelpButton(int num)
    {
        EffectSoundManager.instance.PlayButtonSound();
        helpPanels[num].SetActive(true);
    }

    public void OnQuitButton()
    {
        EffectSoundManager.instance.PlayButtonSound();
        SceneManager.LoadScene("SelectModeScene");
    }

    public void OnExitHelpButton(int num)
    {
        EffectSoundManager.instance.PlayButtonSound();
        helpPanels[num].SetActive(false);
    }

    public void OnHelpSceneButton()
    {
        EffectSoundManager.instance.PlayButtonSound();
        SceneManager.LoadScene("HelpScene");
    }
}
