using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    #region Variables

    public static UiManager instance;
    public int codeGunH1 { get; private set; }
    public int codeGunH2 { get; private set; }

    public bool changeHappen;

    [SerializeField] private Slider bossHp;

    #endregion
    
    #region Basic Function

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        instance = this;
    }

    #endregion

    #region Buttons

    public void GunActiveH1(int code) => GunActiveActionH1(code);
    public void GunActiveH2(int code) => GunActiveActionH2(code);
    
    #endregion

    #region Word Action

    public void UpdateBossSlider(int value) => UpdateBossSliderAction(value);
    public void SetMaxBossSlider(int value) => SetMaxBossSliderAction(value);
    #endregion

    #region Action

    private void GunActiveActionH1(int code)
    {
        codeGunH1 = code;
        changeHappen = true;
    }
    
    private void GunActiveActionH2(int code)
    {
        codeGunH2 = code;
        changeHappen = true;
    }

    private void UpdateBossSliderAction(int value)
    {
        bossHp.value = value;
    }

    public void SetMaxBossSliderAction(int value)
    {
        bossHp.maxValue = value;
    }

    #endregion

    
    
}
