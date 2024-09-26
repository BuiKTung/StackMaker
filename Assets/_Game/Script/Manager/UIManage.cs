using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManage : Singleton<UIManage>
{
    [SerializeField] Button button_Setting;
    [SerializeField] Button button_Cancel;
    [SerializeField] Button button_ExitMenu;
    [SerializeField] Button button_NextLevel;
    [SerializeField] Button button_Reset, button_Reset2;
    [SerializeField] GameObject victoryMenu;
    [SerializeField] GameObject settingMenu;
    [SerializeField] TextMeshProUGUI levelName;
    bool oneChange = true;
    //[SerializeField] GameObject UIMenu;
    
    protected override void Awake()
    {
        base.Awake();
        //Instantiate(UIMenu);
    }
    void Start()
    {

        button_Reset.onClick.AddListener(ResetLevel);
        button_Reset2.onClick.AddListener(ResetLevel);
        button_ExitMenu.onClick.AddListener(ExitToMainMenu);
        button_NextLevel.onClick.AddListener(NextLevel);
        button_Setting.onClick.AddListener(ActiveSetting);
        button_Cancel.onClick.AddListener(DeActiveSetting);
        levelName.text = SceneManager.GetActiveScene().name;
       
    }
    private void Update()
    {
        if (GameManagement.Instance.currentState == GameManagement.GameState.Victory && oneChange)
        {
            StartCoroutine(WaitVictory());
            oneChange = false;
        }
    }

    void ExitToMainMenu()
    {   
        GameManagement.Instance.ChangState(GameManagement.GameState.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }
    public void ResetLevel()
    {
        victoryMenu.SetActive(false);
        GameManagement.Instance.ChangState(GameManagement.GameState.Playing);
        oneChange = true;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    void NextLevel()
    {
        victoryMenu.SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
        Debug.Log(currentScene.buildIndex);
    }
    public void DeActiveSetting()
    {
        settingMenu.SetActive(false);
    } 
    public void ActiveSetting()
    {
        settingMenu.SetActive(true);
    }
    private IEnumerator WaitVictory()
    {
        yield return new WaitForSeconds(1f);
        victoryMenu.SetActive(true);
    }

}

