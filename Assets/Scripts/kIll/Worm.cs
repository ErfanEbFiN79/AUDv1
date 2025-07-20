using System;
using UnityEngine;

public class Worm : MonoBehaviour
{
    #region Variables

    [Header("Base")] 
    [SerializeField] private GameObject fire;
    [SerializeField] private Transform[] pointShot;
    [SerializeField] private float[] timeBtwShot;
    [SerializeField] private int[] numberOfShot;

    [Header("Time & Fx")] 
    [SerializeField] private float[] timeDeleteSelf;
    [SerializeField] private ParticleSystem fxDestroy;
    
    private enum WorkState
    {
        SetData,
        Ready,
        Attack
    }

    private WorkState _workState;
    private float _timeBtwShot;
    private float _numberOfShot;
    private Vector3 _pointShot;
    private float _timeDeleteSelf;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _timeDeleteSelf = timeDeleteSelf[Random(0, timeDeleteSelf.Length)];
    }

    private void Update()
    {
        Destroy(gameObject, _timeDeleteSelf);
        Shot();
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

    #region Action

    private void Shot()
    {
        switch (_workState)
        {
            case WorkState.SetData:
                _timeBtwShot = timeBtwShot[Random(0, timeBtwShot.Length)];
                _numberOfShot = numberOfShot[Random(0, numberOfShot.Length)];
                _workState = WorkState.Ready;
                break;
            
            case WorkState.Ready:
                _timeBtwShot -= Time.deltaTime;
                if (_timeBtwShot <= 0)
                {
                    _workState = WorkState.Attack;
                }
                break;
            
            case WorkState.Attack:
                for (int i = 0; i < _numberOfShot; i++)
                {
                    PlayerPrefs.SetInt("NumberElectCreate", PlayerPrefs.GetInt("NumberElectCreate") + 1);
                    _pointShot = pointShot[Random(0, pointShot.Length)].position;
                    Instantiate(fire, _pointShot, Quaternion.identity);
                }

                _workState = WorkState.SetData;
                break;
        }
    }

    #endregion

    #region Help

    private int Random(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    #endregion

}
