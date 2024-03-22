using System;
using UnityEngine;

public class HpController : MonoBehaviour
{
    
    [SerializeField] private int hp;
    [SerializeField] private GameObject effectDestroy;


    private void Update()
    {
        if (hp > 0) return;
        Destroy(gameObject);
        Instantiate(
            effectDestroy,
            transform.position,
            Quaternion.identity
        );

    }

    public void HpSet(bool down, int value) => HpDownAction(down, value);
    
    
    private void HpDownAction(bool down, int value)
    {
        if (down)
        {
            hp -= value;
        }
        else
        {
            hp += value;
        }
    }
    
    
}
