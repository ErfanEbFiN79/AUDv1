using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("Drone Attack")] 
    [SerializeField] private string[] keysAccessToDroneData;
    [SerializeField] private string[] floatKeyAccess;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (string key in keysAccessToDroneData)
            {
                print($"{key} : {PlayerPrefs.GetInt(key)}");
            }
            
            foreach (string key in floatKeyAccess)
            {
                print($"{key} : {PlayerPrefs.GetFloat(key)}");
            }
        }
    }
}