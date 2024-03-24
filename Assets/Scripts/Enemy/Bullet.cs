using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables

    [Header("Base Setting")] 
    [SerializeField] private GameObject player;

    [Header("Move Setting")]
    [SerializeField] private float speedMove;
    [SerializeField] private Vector3 setTarget;

    #endregion

    #region Unity Methods
    
    private void Start()
    {
        player = GameObject.Find("Player");
        setTarget = player.transform.position;
    }

    private void Update()
    {
        Move();
    }

    #endregion

    #region Action

    private void Move()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            setTarget,
            speedMove * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, setTarget) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    #endregion

}

