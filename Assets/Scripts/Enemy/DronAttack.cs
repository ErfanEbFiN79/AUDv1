using System;
using Unity.Mathematics;
using UnityEngine;

public class DroneAttack : MonoBehaviour
{
    #region Variables

    private enum StateDrone
    {
        Follow,
        MoveShot,
        Shot,
    }

    private StateDrone _stateDrone = StateDrone.Follow;
    
    
    [Header("Info")] 
    [SerializeField] private string tagPlayer;
    [SerializeField] private string tagGround;
    public GameObject[] _player;
    private GameObject _ground;
        
    [Header("Move Setting")]
    [SerializeField] private float speedMoveX;
     [SerializeField] private float speedMoveY;
    [SerializeField] private float[] distanceFromPlayer;
    [SerializeField] private float[] distanceFromGround;
    private float _distancePlayer;
    private float _distanceGround;
    private float _distanceFromPlayerData;
    private float _distanceFromGroundData;
    private GameObject _playerFollowSet;

    [Header("Time Setting")]
    [SerializeField] private float timeBtwSetRandomData;

    [Header("Stop Shot Setting")] 
    [SerializeField] private GameObject shotBullet;
    [SerializeField] private Transform pointShot;
    [SerializeField] private float timeBtwShot;
    private float _timeBtwShotSet;
    
    [Header("Move Shot Setting")] 
    [SerializeField] private GameObject moveShotBullet;
    [SerializeField] private float moveTimeBtwShot;
    private float _moveTimeBtwShotSet;

    private bool _followShot;
    
    
    #endregion

    #region Unity Methods

    
    private void Start()
    {
        InvokeRepeating("GiveRandom", 0,timeBtwSetRandomData);
        _player = GameObject.FindGameObjectsWithTag(tagPlayer);
        _ground = GameObject.FindWithTag(tagGround);
    }

    private void Update()
    {
        GetData();
        BaseState();
        ManageState();
    }
    

    #endregion

    #region Manager

    private void BaseState()
    {
        // base actions
        KeepDistance();
        See();
    }

    private void ManageState()
    {

        switch (_stateDrone)
        {
            case StateDrone.Follow:
                // just follow the player
                Follow();
                break;
            
            case StateDrone.MoveShot:
                // follow player and shot
                Follow();
                ManageShot();
                MoveShot();
                break;
            
            case StateDrone.Shot:
                // stand and shot
                StandShot();
                break;
        }
    }

    private void ManageShot()
    {
        if (_followShot)
        {
            _stateDrone = StateDrone.MoveShot;
        }

    }

    #endregion

    #region Trak

    private void GetData()
    {
        var position = transform.position;
        _distancePlayer = Vector3.Distance(position, _playerFollowSet.transform.position);
        _distanceGround = Vector3.Distance(position, _ground.transform.position);
    }

    #endregion
    

    #region Action

    private void See()
    {
        transform.LookAt(_playerFollowSet.transform);
    }

    private void Follow()
    {
        try
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _playerFollowSet.transform.position,
                speedMoveX * Time.deltaTime
            );
        }
        catch
        {
           GiveRandom();
        }

    }

    private void KeepDistance()
    {
        if (_distancePlayer < _distanceFromPlayerData)
        {
            _stateDrone = StateDrone.Shot;
        }
        else
        {
            _stateDrone = StateDrone.Follow;
        }

        if (_distanceGround < _distanceFromGroundData)
        {
            transform.Translate(0,speedMoveY * Time.deltaTime, 0);
        }
    }

    private void MoveShot()
    {
        _moveTimeBtwShotSet += Time.deltaTime;
        if (_moveTimeBtwShotSet >= moveTimeBtwShot)
        {
            Instantiate(
                moveShotBullet,
                pointShot.position,
                quaternion.identity
            );
            _moveTimeBtwShotSet = 0;
        }

    }

    private void StandShot()
    {
        _timeBtwShotSet += Time.deltaTime;
        if (_timeBtwShotSet >= timeBtwShot)
        {
            Instantiate(
                shotBullet,
                pointShot.position,
                quaternion.identity
            );
            _timeBtwShotSet = 0;
        }

    }

    #endregion

    #region Help

    private int Random(int min, int max) => UnityEngine.Random.Range(min, max);

    private void GiveRandom()
    {
        _distanceFromPlayerData = distanceFromPlayer[Random(0, distanceFromPlayer.Length)];
        _distanceFromGroundData = distanceFromGround[Random(0, distanceFromGround.Length)];
        _playerFollowSet = _player[Random(0, _player.Length)];
        var data = Random(0, 2);
        if (data == 0)
        {
            _followShot = true;
        }
        else
        {
            _followShot = false;
        }
    }
    
    #endregion

    #region Detect

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            GiveRandom();
        }
    }

    #endregion

}
