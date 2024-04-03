using System;
using UnityEngine;

public class HpController : MonoBehaviour
{
    #region Variables

    [Header("Base")]
    [SerializeField] private int hp;
    [SerializeField] private GameObject effectDestroy;

    [Header("Boss")] 
    [SerializeField] private bool boss;

    #endregion

    #region Unity Methods

    private void Start()
    {
        if (boss)
        {
            UiManager.instance.SetMaxBossSlider(hp);
        }
    }

    private void Update()
    {
        if (hp < 0)
        {
            Destroy(gameObject);
            Instantiate(
                effectDestroy,
                transform.position,
                Quaternion.identity
            );
        }

        BossWork();

    }

    #endregion

    #region Work

    public void HpSet(bool down, int value) => HpDownAction(down, value);

    #endregion

    #region Action

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

    #endregion

    #region Boss Work

    private void BossWork()
    {
        if (boss)
        {
            UiManager.instance.UpdateBossSlider(hp);
        }
    }

    #endregion



    
    

    
    
}
