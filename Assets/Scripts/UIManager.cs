using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Slider energyBar;
    public Text level;

    private static UIManager instance;

    public static UIManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
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

}
