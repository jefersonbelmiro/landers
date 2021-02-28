using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Objective : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(HandleHint());
        StartCoroutine(HandleCamera());
    }

    IEnumerator HandleCamera()
    {
        CinemachineVirtualCamera camera = gameObject.GetComponentInChildren<CinemachineVirtualCamera>();

        float delay = LevelManager.Instance.current > 1 ? 2 : 0;
        if (camera) {
            yield return new WaitForSeconds(delay);
            camera.gameObject.SetActive(false);
        }

        GameManager.Instance.SetState(GameManager.State.Play);
    }

    IEnumerator HandleHint()
    {
        GameObject hint = transform.Find("TargetText").gameObject;
        LeanTween.alpha(hint, 0f, 0.5f).setLoopPingPong();
        yield return new WaitForSeconds(3);
        hint.SetActive(false);
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
