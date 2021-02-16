using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    float cameraSpeed = 4.2f;
    float pixelsPerUnit = 32.0f;

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
        float t = RoundToMultiple(cameraSpeed * Time.deltaTime);
        Vector3 newPosition = Vector3.Lerp(transform.position, target.transform.position, t);
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }

    void FollowClamp()
    {
        float t = cameraSpeed * Time.deltaTime;
        Vector2 newPosition = Vector2.Lerp(transform.position, target.transform.position, t);
        Vector2 targetPosition = target.transform.position;
        Vector2 min = targetPosition - new Vector2(2f, 2f);
        Vector2 max = targetPosition + new Vector2(2f, 2f);
        float x = Mathf.Clamp(newPosition.x, min.x, max.x);
        float y = Mathf.Clamp(newPosition.y, min.y, max.y);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    Vector2 PixelPerfectClamp(Vector2 position)
    {
        Vector2 positionInPixels = new Vector2(
                Mathf.RoundToInt(position.x * pixelsPerUnit),
                Mathf.RoundToInt(position.y * pixelsPerUnit));
        return positionInPixels / pixelsPerUnit;
    }

    float RoundToMultiple(float value)
    {
        // Calculates the minimum size of a screen pixel
        float multipleOf = 1.0f / pixelsPerUnit;
        // Using Mathf.Round at each frame is a performance killer
        return (int)((value / multipleOf) + 0.5f) * multipleOf;
    }
}
