using System;
using UnityEngine;

public class DestroyInTime : MonoBehaviour
{
    #region Variable

    [Header("Setting")]
    [SerializeField] private float timeDeleteSelf;
    [SerializeField] private ParticleSystem fxDestroy;

    #endregion

    #region Unity Methods

    private void Update()
    {
        Destroy(gameObject,timeDeleteSelf);
    }

    private void OnDestroy()
    {
        if (fxDestroy != null)
        {
            Instantiate(
                fxDestroy,
                transform.position,
                Quaternion.identity
            );
        }
    }

    #endregion
}
