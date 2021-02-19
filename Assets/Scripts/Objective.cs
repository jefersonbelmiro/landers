using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Objective : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DisableCamera());
    }

    IEnumerator DisableCamera()
    {
        CinemachineVirtualCamera camera = gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
        if (camera) {
            yield return new WaitForSeconds(2);
            camera.gameObject.SetActive(false);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Ship ship = other.gameObject.GetComponent<Ship>();
            if (ship && ship.Landed())
            {
                GameManager.Instance.NextLevel();
            }
        }
    }
}
