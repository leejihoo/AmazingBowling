using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public AudioSource explosionAudio;

    public float maxDamage = 100f;
    public float explosionForce = 1000f;
    public float lifeTime = 10f;
    public float explosionRadius = 20f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        explosionParticle.transform.parent = null;

        explosionParticle.Play();
        explosionAudio.Play();

        Destroy(explosionParticle.gameObject,explosionParticle.duration);
        Destroy(gameObject);

    }
}
