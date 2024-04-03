using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float timeBtwShot;
    [SerializeField] private int damage;
    [SerializeField] private int bulletCapacity;
    [SerializeField] private int reloadBullet;
    
    public float TimeBtwShotGet { get; private set; }
    public int BulletCapacityGet { get; private set; }
    public int DamageGet { get; private set; }
    public int ReloadBulletGet { get; private set; }

    private void Awake()
    {
        TimeBtwShotGet = timeBtwShot;
        BulletCapacityGet = bulletCapacity;
        DamageGet = damage;
        ReloadBulletGet = reloadBullet;
    }
}
