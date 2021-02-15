using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    float trackingSpeed = 4.2f;

    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }


    void Update()
    {
        if (target != null)
        {
            Follow();
        }
    }

    void Follow()
    {
        float z = transform.position.z;
        Vector3 position = target.transform.position;
        position.z = z;
        transform.position = position;
    }

    void FollowLerp()
    {
        float z = transform.position.z;
        Vector2 newPosition = Vector2.Lerp(
            transform.position,
            target.transform.position,
            Time.deltaTime * trackingSpeed
        );

        Vector3 targetPosition = target.transform.position;
        Vector3 min = targetPosition - new Vector3(5.0f, 5.0f, 0.0f);
        Vector3 max = targetPosition + new Vector3(5.0f, 5.0f, 0.0f);
        float x = Mathf.Clamp(newPosition.x, min.x, max.x);
        float y = Mathf.Clamp(newPosition.y, min.y, max.y);
        Vector3 position = new Vector3(x, y, transform.position.z);

        transform.position = position;
        // transform.position = Clamp(new Vector3(position.x, position.y, z));
        // transform.position = new Vector3(position.x, position.y, z);
    }
}
