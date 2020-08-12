using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _verticalSpeed = 5f;
    [SerializeField] private float _horizontalSpeed = 5f;
    [SerializeField] private Boundary _boundary;

    private PlayerInput _input;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!LevelController.Instance.IsPlaying)
            return;

        Vector2 movement = _input.GetMovementInput();
        movement.x *= _horizontalSpeed;
        movement.y *= _verticalSpeed;
        _rigidbody.velocity = movement;

        float xPosition = Mathf.Clamp(_rigidbody.position.x, _boundary.xMin, _boundary.xMax);
        float yPosition = Mathf.Clamp(_rigidbody.position.y, _boundary.yMin, _boundary.yMax);
        _rigidbody.position = new Vector2(xPosition, yPosition);
    }
}
