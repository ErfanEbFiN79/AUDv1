using UnityEngine;

public class TimeActiveDataAdd : MonoBehaviour
{
    #region Variables

    [Header("Setting")] 
    [SerializeField] private string nameSetData;

    #endregion

    #region Unity Methods

    private void Update()
    {
        PlayerPrefs.SetFloat(nameSetData, PlayerPrefs.GetFloat(nameSetData) + Time.deltaTime);
    }

    #endregion

}