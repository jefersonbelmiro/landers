﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int level = 0;

    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    public void RestartLevel(float delay)
    {
        StartCoroutine(LoadScene(level, delay));
    }

    IEnumerator LoadScene(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(level);
    }
    
}
