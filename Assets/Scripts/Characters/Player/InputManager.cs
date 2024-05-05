using UnityEngine;

public class InputManager : MonoBehaviour, ITellDirection
{
    private const string Horizontal = nameof(Horizontal);

    private KeyCode _jumpKey = KeyCode.Space;

    public float Direction { get; private set; }
    public bool IsJump { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);
        IsJump = Input.GetKey(_jumpKey);
    }
}
