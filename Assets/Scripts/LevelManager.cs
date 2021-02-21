using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int current = 0;
    public Animator transition;

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
        if (UIManager.Instance)
        {
            UIManager.Instance.SetLevel(current);
        }
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
        StartCoroutine(LoadScene(nextIndex, 1f));
    }

    IEnumerator LoadScene(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(index);
    }
}
