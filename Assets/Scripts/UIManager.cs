using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Slider energyBar;
    public Text level;
    public Text fps;

    public bool showFPS = true;

    public static UIManager Instance
    {
        get { return instance; }
    }

    private static UIManager instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(DisplayFPS());
    }

    public void SetLevel(int value)
    {
        level.text = "" + value;
    }

    public void SetHealth(float value)
    {
        healthBar.value = value;
    }

    public void SetEnergy(float value)
    {
        energyBar.value = value;
    }

    private IEnumerator DisplayFPS()
    {
        while (showFPS)
        {
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(0.5f);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it
            float framesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
            fps.text = framesPerSec.ToString() + " fps";
        }
    }
}
