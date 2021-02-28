using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State { Menu, Play };
    public State state = GameManager.State.Menu;
    
    static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    public void RestartLevel(float delay)
    {
        LevelManager.Instance.RestartLevel(delay);
    }

    public void NextLevel()
    {
        LevelManager.Instance.NextLevel();
    }

    public void SetState(GameManager.State value)
    {
        state = value;
    }

    public bool AllowMoviment()
    {
        return this.state == GameManager.State.Play;
    }
}

