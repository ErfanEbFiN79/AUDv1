using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Move : MonoBehaviour
{
    #region Variables

    [Header("Setting")] 
    [SerializeField] private float[] speed;
    [SerializeField] private Vector3[] rotates;
    private float _speed;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _speed = speed[Random.Range(0, speed.Length)];
        SetRotate(rotates[Random.Range(0, rotates.Length)]);
    }

    void Update()
    {
        MoveAction();
    }

    #endregion

    #region Action

    private void SetRotate(Vector3 rotate)
    {
        gameObject.transform.eulerAngles = rotate;
    }
    
    private void MoveAction()
    {
        transform.Translate(_speed * Time.deltaTime,0,0,Space.Self);
    }
    
    

    #endregion

}
