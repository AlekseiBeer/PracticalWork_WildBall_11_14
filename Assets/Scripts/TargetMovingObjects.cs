using System;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class TargetMovingObjects : MonoBehaviour
{
    public enum STATUS
    { 
        START = 0,
        MOVING_END,
        MOVING_START,
        END
    }

    public STATUS status = STATUS.START;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _endPos;
    [SerializeField] private float timeMoving;

    private float _currentTime = 0;

    // Update is called once per frame
    void Update()
    {
        Moving(status);
    }

    void Moving(STATUS stat)
    {
        if (stat == STATUS.MOVING_END)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime < timeMoving)
            {
                transform.localPosition = LerpVector(_startPos, _endPos, _currentTime / timeMoving);
            }
            else
            {
                status = STATUS.END;
                _currentTime = 0;
            }
        }

        if (stat == STATUS.MOVING_START)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime < timeMoving)
            {
                transform.localPosition = LerpVector(_endPos, _startPos, _currentTime / timeMoving);
            }
            else
            {
                status = STATUS.START;
                _currentTime = 0;
            }
        }
    }

    private Vector3 LerpVector(Vector3 vector1, Vector3 vector2, float time)
    {
        return new Vector3(Mathf.Lerp(vector1.x, vector2.x, time),
                           Mathf.Lerp(vector1.y, vector2.y, time),
                           Mathf.Lerp(vector1.z, vector2.z, time));
    }
}
