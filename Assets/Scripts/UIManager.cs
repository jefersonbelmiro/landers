using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Slider energyBar;

    private static UIManager instance;

    public static UIManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
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
