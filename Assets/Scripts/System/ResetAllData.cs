using UnityEngine;

public class ResetAllData : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }
}
