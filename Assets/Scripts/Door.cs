using UnityEngine;
using WildBall.Player;

public class Door : MonoBehaviour
{
    private PlayerController _playerController;
    private HintsController _PlayerHints;
    enum DOOR_STATUS
    {
        OPEN,
        CLOSE,
        OPENING,
        CLOSING
    }

    [Header("Door Settings")]
    [SerializeField] private DOOR_STATUS _doorStatus;
    [SerializeField] private Vector3 _minRot;
    [SerializeField] private Vector3 _maxRot;
    [SerializeField] private float _speadRotation = 1;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _PlayerHints = FindObjectOfType<HintsController>();
    }

    void Update()
    {
        if (CheckRaycastPlayer() && Input.GetKeyDown(KeyCode.F))
        {
            if (_doorStatus == DOOR_STATUS.OPEN)
                _doorStatus = DOOR_STATUS.CLOSING;
            if (_doorStatus == DOOR_STATUS.CLOSE)
                _doorStatus = DOOR_STATUS.OPENING;
        }

        if (_doorStatus == DOOR_STATUS.OPENING)
            OpenClose(1);

        if (_doorStatus == DOOR_STATUS.CLOSING)
            OpenClose(-1);
    }

    void OpenClose(int i)
    {
        Vector3 angl = transform.localRotation.eulerAngles;
        angl += new Vector3(0, i * _speadRotation * Time.deltaTime, 0);
        Vector3 clampangl = ClampVector(angl, _minRot, _maxRot);
        transform.localRotation = Quaternion.Euler(clampangl);
        if (i == 1 && clampangl.Equals(_maxRot))
            _doorStatus = DOOR_STATUS.OPEN;
        if (i == -1 && clampangl.Equals(_minRot))
            _doorStatus = DOOR_STATUS.CLOSE;
    }

    /// <summary>
    /// ќграничивает вектор в пределах минимального и максимального значений.
    /// </summary>
    private Vector3 ClampVector(Vector3 vector, Vector3 min, Vector3 max)
    {
        return new Vector3(Mathf.Clamp(vector.x, min.x, max.x),
                           Mathf.Clamp(vector.y, min.y, max.y),
                           Mathf.Clamp(vector.z, min.z, max.z));
    }

    bool CheckRaycastPlayer()
    {
        string textHint = "¬заимодействовать F";
        GameObject RaycastObj = _playerController.GetObjectRaucastForvard();
        if (gameObject == RaycastObj)
        {
            _PlayerHints.ShowHint(textHint);
            return true;
        }
        else if (_PlayerHints.GetTextHint() == textHint)
        {
            _PlayerHints.HideHint();
        }
        return false;
    }
}
