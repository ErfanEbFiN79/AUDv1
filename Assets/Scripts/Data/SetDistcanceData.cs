using System;
using UnityEngine;

public class SetDistcanceData : MonoBehaviour
{
    #region Variables

    [Header("Base")]
    
    [Tooltip("If you don't want to us self object")]
    [SerializeField] private GameObject objOne;
    [SerializeField] private string tagForObjTwo;
    [SerializeField] private string nameForSaveData;
    private float _minDistance = 100000;

    #endregion

    #region Unit Methods

    private void Start()
    {
        PlayerPrefs.SetFloat(nameForSaveData, 10000000);
        if (objOne == null)
        {
            objOne = this.gameObject;
        }
    }

    private void Update()
    {
        Setter();
    }

    #endregion

    #region Action

    private float GetDistance()
    {
        return Vector2.Distance(objOne.transform.position, GameObject.FindWithTag(tagForObjTwo).transform.position);
    }

    private void Setter()
    {
        if (GetDistance() < PlayerPrefs.GetFloat(nameForSaveData))
        {
            PlayerPrefs.SetFloat(nameForSaveData, GetDistance());
        }
    }

    #endregion
}
