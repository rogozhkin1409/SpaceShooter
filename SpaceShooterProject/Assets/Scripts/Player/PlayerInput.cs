using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private bool _useJoystick;

    public Vector2 GetMovementInput()
    {
        if (!_useJoystick)
            _joystick.gameObject.SetActive(false);

        Vector2 joystickInput = _useJoystick ? _joystick.Direction : Vector2.zero;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 result = new Vector2(MaxAbs(x, joystickInput.x), MaxAbs(y, joystickInput.y));
        if (result.magnitude > 1)
            result.Normalize();

        return result;
    }

    private float MaxAbs(float a, float b) => Mathf.Abs(a) > Mathf.Abs(b) ? a : b;
}
