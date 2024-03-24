using UnityEngine;

public class Boss1 : MonoBehaviour
{
    #region Variables

    [Header("Base Setting")] 
    [SerializeField] private string nameBoss;
    [SerializeField] private float hpOfBoss;
    [SerializeField] private int levelBoss;
    private GameObject _player;

    [Header("Attack 1 [Create Drone]")] 
    [SerializeField] private GameObject drone;
    [SerializeField] private Transform pointCreateDrone;
    [SerializeField] private int howMuchDrone;
    [SerializeField] private float timeCreateDrone;
    private float _timeCreateDroneSet;
    
    
    [Header("Attack 2 [Rocket]")] 
    [SerializeField] private GameObject rocket;
    [SerializeField] private Transform pointCreateRocket;
    [SerializeField] private int howMuchRocket;
    [SerializeField] private float timeCreateRocket;
    private float _timeCreateRocketSet;
    
    

    #endregion

    #region Unity Methods

    private void Start()
    {
        _timeCreateDroneSet = timeCreateDrone;
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        See();
        Attack1();
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
        _timeCreateDroneSet += 1 * Time.deltaTime;
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
        
    }

    private void Attack3()
    {
        
    }

    private void Attack4()
    {
        
    }

    private void Attack5()
    {
        
    }

    #endregion




}
