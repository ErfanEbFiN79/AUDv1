using UnityEngine;

public class OnEnaiblPlayFx : MonoBehaviour
{
    [SerializeField] private ParticleSystem fx;
    
    private void OnEnable()
    {
        fx = gameObject.GetComponent<ParticleSystem>();
        fx.Play();
    }
}
