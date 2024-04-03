using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private ParticleSystem effectDestroy;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Instantiate(
                effectDestroy,
                transform.position,
                Quaternion.identity
            );
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Instantiate(
                effectDestroy,
                transform.position,
                Quaternion.identity
            );
            Destroy(gameObject);
        }
    }
}
