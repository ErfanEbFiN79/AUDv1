using System;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{
    #region Variables

    [Header("Damage Setting")]
    [SerializeField] private float damage;
    [SerializeField] private string tagWork;
    
    [Header("Fx Setting")] 
    [SerializeField] private GameObject fxDestroy;
    

    #endregion

    #region Action

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(tagWork))
        {
            PlayerController.instance.HpSystem(false,damage);
            Destroy(gameObject);
            if (fxDestroy != null)
            {
                Instantiate(
                    fxDestroy,
                    transform.position,
                    Quaternion.identity
                );
            }
        }
    }

    #endregion
}
