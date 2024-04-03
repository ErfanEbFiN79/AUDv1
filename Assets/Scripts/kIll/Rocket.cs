using System;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    #region Varianles

    [Header("Move Setting")]
    [SerializeField] private float speed;

    [Header("Follow Setting")]
    [SerializeField] private string tagFollow;
    private GameObject _player;
    private Vector3 _followVector3;
    
    [Header("Time Manager")]
    [SerializeField] private float timeDeleteSelf;

    [Header("Fx Setting")]
    [SerializeField] private GameObject fxTimeDestroy;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag(tagFollow);
    }

    private void Update()
    {
        transform.LookAt(_player.transform);
        FollowCalculator();
        Move();
        Destroy(gameObject, timeDeleteSelf);
    }

    private void OnDestroy()
    {
        if (fxTimeDestroy != null)
        {
            Instantiate(
                fxTimeDestroy,
                transform.position,
                Quaternion.identity
            );
        }
    }

    #endregion

    #region Action

    private void FollowCalculator()
    {
        _followVector3 = _player.transform.position;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _followVector3,
            speed * Time.deltaTime
        );
        
        
    }
    
    #endregion
}
