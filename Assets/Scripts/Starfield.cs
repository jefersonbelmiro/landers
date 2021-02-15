using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Starfield : MonoBehaviour
{
    public int MaxStars = 100;
    public float StarSize = 0.1f;
    public float StarSizeRange = 0.5f;
    public float FieldWidth = 20f;
    public float FieldHeight = 25f;
    public bool Colorize = false;

    public float ParallaxFactor = 0f;

    float xOffset;
    float yOffset;

    ParticleSystem Particles;
    ParticleSystem.Particle[] Stars;

    void Awake()
    {
        Stars = new ParticleSystem.Particle[MaxStars];
        Particles = GetComponent<ParticleSystem>();

        Assert.IsNotNull(Particles, "Particle system missing from object!");

        // Offset the coordinates to distribute the spread
        xOffset = FieldWidth * 0.5f;
        // around the object's center
        yOffset = FieldHeight * 0.5f;

        for (int i = 0; i < MaxStars; i++)
        {
            // Randomize star size within parameters
            float randSize = Random.Range(StarSize, StarSizeRange);
            // If coloration is desired, color based on size
            float scaledColor = (true == Colorize) ? randSize - StarSizeRange : 1f;

            float opacity = Random.Range(0.3f, 1.0f);
            Stars[i].position = GetRandomInRectangle(FieldWidth, FieldHeight) + transform.position;
            Stars[i].startSize = randSize;
            Stars[i].startColor = new Color(1f, scaledColor, scaledColor, opacity);
        }
        // Write data to the particle system
        Particles.SetParticles(Stars, Stars.Length);
    }


    // GetRandomInRectangle
    //----------------------------------------------------------
    // Get a random value within a certain rectangle area
    //
    Vector3 GetRandomInRectangle(float width, float height)
    {
        float x = Random.Range(0, width);
        float y = Random.Range(0, height);
        return new Vector3(x - xOffset, y - yOffset, 0);
    }
}
