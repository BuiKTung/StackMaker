using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameManagement : Singleton<GameManagement>
{
    public enum GameState
    {
        MainMenu,
        Playing,
        Victory
    }
    public GameState currentState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        currentState = GameState.MainMenu;
    }
    public void ChangState(GameState newState)
    {
        currentState= newState;
    }
}
