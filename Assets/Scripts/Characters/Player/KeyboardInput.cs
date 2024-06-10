using UnityEngine;

public class KeyboardInput : MonoBehaviour, ITellDirection
{
    private const string Horizontal = nameof(Horizontal);//зачем const?

    private KeyCode _jumpKey = KeyCode.Space;

    public float Direction => Input.GetAxis(Horizontal);
    public bool IsJump => Input.GetKey(_jumpKey);
}
