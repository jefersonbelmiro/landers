using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject thruster;

    [SerializeField]
    float thrustPower = 8.0f;

    Rigidbody2D body;
    Quaternion rotation;

    [SerializeField]
    ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            float z = transform.rotation.eulerAngles.z;
            z -= Input.GetAxis("Horizontal") * 4.0f;
            rotation = Quaternion.Euler(0f, 0f, z);
            // transform.rotation = rotation;
            body.SetRotation(rotation);
        }

        // if (Input.GetMouseButton(0))
        if (Input.GetAxis("Vertical") > 0)
        {
            thruster.SetActive(true);
            ApplyForce();
        }
        else
        {
            thruster.SetActive(false);
        }
    }

    void ApplyForce()
    {
        // body.velocity = transform.up * this.thrustPower * Time.deltaTime;

        // body.AddForce(transform.up * this.thrustPower);
        // body.SetRotation(rotation);

        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        body.AddForce(transform.up * this.thrustPower);
        body.constraints = RigidbodyConstraints2D.None;

        // Vector3 direction = transform.position - thruster.transform.position;
        // body.AddForceAtPosition(direction.normalized * thrustPower, thruster.transform.position);

        // Vector3 direction = transform.position - thruster.transform.position;
        // body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        // body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX;

        // body.constraints = RigidbodyConstraints2D.FreezeRotation;
        // body.AddForceAtPosition(direction.normalized * thrustPower, thruster.transform.position);
        // body.constraints = RigidbodyConstraints2D.None;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collision: " + other.relativeVelocity.magnitude);
        if (other.relativeVelocity.magnitude > 2)
        {
            Die();
        }
    }

    void Die()
    {
        var explosionObj = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explosionObj.gameObject, 2.5f);

        CameraEffects.ShakeOnce();
        GameManager.Instance.RestartLevel(2.5f);

        Destroy(gameObject);
    }
}
