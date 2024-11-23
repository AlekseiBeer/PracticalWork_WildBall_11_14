using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animatorPoliceman;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWeight(int ActivSecondAnim)
    {
            _animatorPoliceman.SetLayerWeight(1, ActivSecondAnim == 1 ? 1 : 0);
    }
}
