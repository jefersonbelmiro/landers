using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject thruster;

    [SerializeField]
    float thrustPower = 8.0f;

    [SerializeField]
    float rotatePower = 4.0f;

    [SerializeField]
    ParticleSystem explosion;

    public float life = 1.0f;
    public float energy = 100.0f;
    public float energyMax = 100.0f;

    Rigidbody2D body;
    Quaternion rotation;

    public delegate void OnDie();
    public delegate void OnDamage(float damage);

    public OnDie onDie;
    public OnDamage onDamage;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // @TODO active/deactive thruster
    }

    public void Rotate(float value)
    {
        float z = transform.rotation.eulerAngles.z;
        z -= value * rotatePower;
        rotation = Quaternion.Euler(0f, 0f, z);
        body.SetRotation(rotation);
    }

    public void ApplyForce()
    {
        energy -= 0.3f;

        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        body.AddForce(transform.up * this.thrustPower);
        body.constraints = RigidbodyConstraints2D.None;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collision: " + other.relativeVelocity.magnitude);
        if (other.relativeVelocity.magnitude > 1)
        {
            Damage((other.relativeVelocity.magnitude - 1) * 0.7f);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "LanderObjective" && body.velocity == Vector2.zero) {
            Debug.Log("SUCESSIFULY LANDING");
            GameManager.Instance.NextLevel();
        }
    }

    void Damage(float damage)
    {
        life -= damage;
        onDamage?.Invoke(damage);
        if (life <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        ParticleSystem explosionObj = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explosionObj.gameObject, 2.5f);

        onDie?.Invoke();
        Destroy(gameObject);
    }
}
