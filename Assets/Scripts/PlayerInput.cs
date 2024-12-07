using UnityEngine;

namespace WildBall.Player.Inputs
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerInput : MonoBehaviour
    {
        [HideInInspector] public float vertical;
        [HideInInspector] public float horizontal;
        [HideInInspector] public float angl;
        [HideInInspector] public bool jump;
        [HideInInspector] public bool leap;

        void Update()
        {
            horizontal = -Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIS);
            vertical = Input.GetAxis(GlobalStringVars.VERTICAL_AXIS);
            angl = 0;
            if (Input.GetKey(KeyCode.Q))
                angl--;
            if (Input.GetKey(KeyCode.E))
                angl++;
            if (Input.GetButtonDown(GlobalStringVars.JUMP_BUTTON))
                jump = true;
            if (Input.GetButtonDown(GlobalStringVars.SHIFT_BUTTON))
                leap = true;

        }
    }
}



