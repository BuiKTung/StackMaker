using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] Button buttonNewGame;
    private void Start()
    {
        buttonNewGame.onClick.AddListener(NewGame);
    }

    void NewGame()
    {
        SceneManager.LoadScene("Level 1");
    }
}
