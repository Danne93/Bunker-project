using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosion : MonoBehaviour
{
    public int damage = 5;
    public float force = 1000;
    private float duration = 0.2f;
    private float destroyTimer;
    //private float radius;
    private SphereCollider sCollider;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] AudioClip explosionSoundEffect;
    AudioSource audi;
    
    // Start is called before the first frame update
    void Start()
    {
        sCollider = GetComponent<SphereCollider>();
        Instantiate(explosionEffect, transform.position, transform.rotation);
        audi = GetComponent<AudioSource>();
        AudioSource.PlayClipAtPoint(audi.clip, transform.position, 0.8f);
        destroyTimer = Time.time + duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > destroyTimer)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit != null)
        {
            unit.Damage(damage);
        }
        if (other.attachedRigidbody != null)
        {
            Vector3 forceDirection = (other.transform.position - transform.position).normalized;
            other.attachedRigidbody.AddExplosionForce(force, transform.position, sCollider.radius);
        }
    }
}
