﻿using System.Collections;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float thrustPower = 8.0f;

    public float rotatePower = 4.0f;

    public ParticleSystem explosion;

    public float life = 1.0f;
    public float energy = 100.0f;
    public float energyMax = 100.0f;

    public delegate void OnDie();
    public delegate void OnDamage(float damage);

    public OnDie onDie;
    public OnDamage onDamage;

    Quaternion rotation;

    public Rigidbody2D body;
    public ParticleSystem thrusterParticles;
    public ParticleSystem thrusterLaser;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        thrusterParticles = transform.Find("ThrusterParticle").GetComponent<ParticleSystem>();
        thrusterLaser = transform.Find("ThrusterLaser").GetComponent<ParticleSystem>();
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

        thrusterParticles.Emit(1);
        thrusterLaser.Emit(3);

        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        body.AddForce(transform.up * this.thrustPower);
        body.constraints = RigidbodyConstraints2D.None;
    }

    public bool Landed()
    {
        return body.velocity == Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > 1)
        {
            Damage((other.relativeVelocity.magnitude - 1) * 0.7f);
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
        ParticleSystem mainParticles = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(mainParticles.gameObject, 2.5f);

        ParticleSystem secondaryParticles = Instantiate(explosion, transform.position, Quaternion.identity);
        ParticleSystem.MainModule settings = secondaryParticles.main;
        var emission = secondaryParticles.emission;
        emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.2f, 20) });
        settings.startColor = new Color(255, 255, 255);
        Destroy(secondaryParticles.gameObject, 2.5f);

        onDie?.Invoke();
        Destroy(gameObject);
    }
}
