using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Ship ship;
    public GameObject startFieldTop;
    public GameObject startFieldBottom;

    float startFieldHeight = 30f;

    // Start is called before the first frame update
    void Start()
    {
        float y = ship.transform.position.y;

        startFieldBottom.transform.position = new Vector3(0, y, 0);
        startFieldTop.transform.position = new Vector3(0, y + startFieldHeight, 0);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    void FixedUpdate()
    {
        ship.transform.position += Vector3.up * Time.deltaTime * 4;

        float y = startFieldBottom.transform.position.y;
        if (ship.transform.position.y >= y + startFieldHeight)
        {
            UpdateStarfield();
        }
    }

    void UpdateStarfield()
    {
        startFieldBottom.transform.position += new Vector3(0, startFieldHeight * 2, 0);
        // swap references
        GameObject top = startFieldTop;
        startFieldTop = startFieldBottom;
        startFieldBottom = top;
    }
}
