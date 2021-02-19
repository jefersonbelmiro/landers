using UnityEngine;

public class Player : MonoBehaviour
{
    public float life = 1.0f;
    public float energy = 100.0f;
    public float energyMax = 100.0f;

    Ship ship;

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Ship>();
        ship.life = life;
        ship.energy = energy;
        ship.energyMax = energyMax;

        ship.onDie += Die;
        ship.onDamage += Damage;

        UIManager.Instance.SetHealth(life);
        UIManager.Instance.SetEnergy(energy / energyMax);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            ship.Rotate(Input.GetAxis("Horizontal"));
        }

        // if (Input.GetMouseButton(0))
        if (Input.GetAxis("Vertical") > 0 && ship.energy > 0)
        {
            ApplyForce();
        }
    }

    void ApplyForce()
    {
        ship.ApplyForce();

        UIManager.Instance.SetEnergy(ship.energy / ship.energyMax);

        if (ship.energy <= 0)
        {
            GameManager.Instance.RestartLevel(3.5f);
        }
    }

    void Damage(float damage)
    {
        CameraEffects.ShakeOnce(0.5f, 2f);
        UIManager.Instance.SetHealth(ship.life);
    }

    void Die()
    {
        CameraEffects.ShakeOnce();
        GameManager.Instance.RestartLevel(2.5f);
    }
}
