using System;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    #region Variables
    
    private GameObject _player;

    [Header("Attack 1 [Create Drone]")] 
    [SerializeField] private GameObject drone;
    [SerializeField] private Transform pointCreateDrone;
    [SerializeField] private int howMuchDrone;
    [SerializeField] private float timeCreateDrone;
    private float _timeCreateDroneSet;
    
    
    [Header("Attack 2 [Rocket]")] 
    [SerializeField] private GameObject rocket;
    [SerializeField] private Transform[] pointCreateRocket;
    [SerializeField] private float timeCreateRocket;
    private float _timeCreateRocketSet;

    [Header("Attack 3 [Wall of fire]")]
    [SerializeField] private ParticleSystem[] fires;
    [SerializeField] private float distanceActiveFire;
    [SerializeField] private float[] timesActiveFire;
    [SerializeField] private float[] timesStayActive;
    private float _timeActiveF;
    private float _timeStayA;
    private enum StateAttack3
    {
        SetData,
        GetReady,
        Attack,
        DeActive,
    }

    private StateAttack3 _stateAttack3;
    

    public float _nowDistance;

    [Header("Attack 4 [Bomb Attack]")] 
    [SerializeField] private GameObject bombs;
    [SerializeField] private float[] forceJumpBombPower;
    [SerializeField] private Vector3[] pointJump;
    [SerializeField] private float[] timeBtwBombAttack;
    [SerializeField] private int[] numberCreateBomb;
    private float _timeBtwBombAttack;
    
    private enum StateBombAttack
    {
        SetData,
        GetReady,
        Attack,
        DeActive
    }

    private StateBombAttack _stateBombAttack;
    private Vector3 _pointJump;
    private float _forcedJumpPower;
    private int _numberCreateBombSet;

    [Header("Attack 5 [Worm Attack]")]
    [SerializeField] private GameObject worm;
    [SerializeField] private float[] timeBtwCreateWorm;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;
    [SerializeField] private float yCreate;
    [SerializeField] private int[] numberCreateWorm;

    private enum StateWormAttack
    {
        SetData,
        GetReady,
        Attack,
    }
    
    
    private StateWormAttack _stateWormAttack;
    private float _timeCreateWorm;
    private int _numberCreateWorm;
    private Vector3 _pointCreateWorm;
    
    #endregion

    #region Unity Methods

    private void Start()
    {
        foreach (ParticleSystem fx in fires)
        {
            fx.gameObject.SetActive(false);
        }
        _timeCreateDroneSet = timeCreateDrone;
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        See();
        Attack1();
        Attack2();
        Attack3();
        Attack4();
        Attack5();
    }

    #endregion

    #region Action

    private void See()
    {
        transform.LookAt(_player.transform);
    }

    #endregion

    private void SelfHeal()
    {
        
    }

    #region Attack  

    private void Attack1()
    {
        _timeCreateDroneSet += Time.deltaTime;
        if (_timeCreateDroneSet >= timeCreateDrone)
        {
            for (int i = 0; i < howMuchDrone; i++)
            {
                Instantiate(
                    drone,
                    pointCreateDrone.position,
                    Quaternion.identity
                );
            }

            _timeCreateDroneSet = 0;
        }
    }

    private void Attack2()
    {
        _timeCreateRocketSet += Time.deltaTime;
        if (_timeCreateRocketSet >= timeCreateRocket)
        {
            Instantiate(
                rocket,
                pointCreateRocket[Random(0, pointCreateRocket.Length)].position,
                rocket.transform.rotation
            );
            _timeCreateRocketSet = 0;
        }
    }

    private void Attack3()
    {
        Attack3Helper();
        
        switch (_stateAttack3)
        {
            case StateAttack3.SetData:
                foreach (ParticleSystem fx in fires)
                {
                    fx.gameObject.SetActive(false);
                }
                _timeActiveF = timesActiveFire[Random(0, timesActiveFire.Length)];
                _timeStayA = timesStayActive[Random(0, timesStayActive.Length)];
                if (_nowDistance < distanceActiveFire)
                {
                    _stateAttack3 = StateAttack3.GetReady;
                }
                break;
                        
            case StateAttack3.GetReady:
                _timeActiveF -= Time.deltaTime;
                if (_timeActiveF <= 0)
                {
                    _stateAttack3 = StateAttack3.Attack;
                }
        
                if (_nowDistance > distanceActiveFire + 1)
                { 
                    _stateAttack3 = StateAttack3.DeActive;
                }
                            
                break;
                        
            case StateAttack3.Attack:
                foreach (ParticleSystem fx in fires)
                {
                    fx.gameObject.SetActive(true);
                }
        
                _timeStayA -= Time.deltaTime;

                if (_timeStayA <= 0)
                {
                    _stateAttack3 = StateAttack3.DeActive;
                }
                break;
            
            case StateAttack3.DeActive:
                foreach (ParticleSystem fx in fires)
                {
                    fx.gameObject.SetActive(false);
                }
                _timeActiveF = 0;
                _timeStayA = 0;
                _nowDistance = 0;
                _stateAttack3 = StateAttack3.SetData;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
  
    }

    private void Attack4()
    {
        switch (_stateBombAttack)
        {
            case StateBombAttack.SetData:
                _timeBtwBombAttack = timeBtwBombAttack[Random(0, timeBtwBombAttack.Length)];
                _forcedJumpPower = forceJumpBombPower[Random(0, forceJumpBombPower.Length)];
                _pointJump = pointJump[Random(0, pointJump.Length)];
                _numberCreateBombSet = numberCreateBomb[Random(0, numberCreateBomb.Length)];
                _stateBombAttack = StateBombAttack.GetReady;
                break;
            case StateBombAttack.GetReady:
                _timeBtwBombAttack -= 1 * Time.deltaTime;
                if (_timeBtwBombAttack <= 0)
                {
                    _stateBombAttack = StateBombAttack.Attack;
                }
                break;
            case StateBombAttack.Attack:
                for (int i = 0; i < _numberCreateBombSet; i++)
                {
                    GameObject bomb = Instantiate(
                        bombs,
                        transform.position,
                        Quaternion.identity
                    );
                    Vector3 point = _player.transform.position + _pointJump;
                    bomb.GetComponent<Rigidbody>().AddForce(point * _forcedJumpPower);
                    _pointJump = pointJump[Random(0, pointJump.Length)];
                }

                _stateBombAttack = StateBombAttack.SetData;
                
                break;
            case StateBombAttack.DeActive:
                
                break;
        }
    }

    private void Attack5()
    {
        switch (_stateWormAttack)
        {
            case StateWormAttack.SetData:

                _timeCreateWorm = timeBtwCreateWorm[Random(0, timeBtwCreateWorm.Length)];
                _numberCreateWorm = numberCreateWorm[Random(0, numberCreateWorm.Length)];
                _stateWormAttack = StateWormAttack.GetReady;
                break;
            
            case StateWormAttack.GetReady:
                _timeCreateWorm -= Time.deltaTime;
                if (_timeCreateWorm <= 0)
                {
                    _stateWormAttack = StateWormAttack.Attack;
                }
                break;
            
            case StateWormAttack.Attack:

                for (int i = 0; i < _numberCreateWorm; i++)
                {
                    _pointCreateWorm = new Vector3(
                        RandomFloat(minX, maxX),
                        yCreate,
                        RandomFloat(minZ, maxZ)
                    );
                    Instantiate(
                        worm,
                        _pointCreateWorm,
                        worm.transform.rotation
                    );
                    
                }

                _stateWormAttack = StateWormAttack.SetData;
                
                break;
        }
    }

    #endregion

    #region Help

    private int Random(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    private float RandomFloat(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    private void Attack3Helper()
    {
        _nowDistance = Vector3.Distance(transform.position, _player.transform.position);
    }
    
    #endregion

    



}
