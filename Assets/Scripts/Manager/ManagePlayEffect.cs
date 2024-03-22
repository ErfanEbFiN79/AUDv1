using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePlayEffect : MonoBehaviour
{
    #region Variables

    [Header("Effect")] 
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private float timeBtwWork;

    #endregion

    #region Unity Methods

    private void Start()
    {
        try
        {
            effect = GetComponent<ParticleSystem>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        InvokeRepeating("Active",0,timeBtwWork);
    }

    #endregion

    #region Action

    private void Active()
    {
        effect.Play();
    }

    #endregion

}
