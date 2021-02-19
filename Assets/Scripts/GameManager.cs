using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
}

