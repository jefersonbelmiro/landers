using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int current = 0;
    static LevelManager instance;

    public static LevelManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIManager.Instance.SetLevel(current);
    }

    public void RestartLevel(float delay)
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex, delay));
    }

    public void NextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex;
        if (nextIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            nextIndex += 1;
        }
        SceneManager.LoadScene(nextIndex, 0);
    }

    IEnumerator LoadScene(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);
    }
}
