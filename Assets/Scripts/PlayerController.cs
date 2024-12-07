using UnityEngine;
using WildBall.Manager;

public class PlayerController : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            ScensManager.ResetLavel();
        }

        if (other.CompareTag("Finish"))
        {
            ScensManager.NextLavel();
        }
    }
}
