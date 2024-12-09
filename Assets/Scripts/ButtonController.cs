using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public bool On_Off = false;

    [SerializeField] private TargetMovingObjects interectivObgect;
    [SerializeField] Material matOn;
    [SerializeField] Material matOff;
    private MeshRenderer _meshRenderButton;


    void Start()
    {
        _meshRenderButton = GetComponent<MeshRenderer>();
        if (interectivObgect.status == TargetMovingObjects.STATUS.START)
        {
            On_Off = false;
            _meshRenderButton.material = matOff;
        }   
        else
        {
            On_Off = true;
            _meshRenderButton.material = matOn;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player") && interectivObgect.status == TargetMovingObjects.STATUS.START)
        {
            On_Off = true;
            interectivObgect.status = TargetMovingObjects.STATUS.MOVING_END;
            _meshRenderButton.material = matOn;
        }

        if (other.collider.CompareTag("Player") && interectivObgect.status == TargetMovingObjects.STATUS.END)
        {
            On_Off = false;
            interectivObgect.status = TargetMovingObjects.STATUS.MOVING_START;
            _meshRenderButton.material = matOff;
        }
    }
}
