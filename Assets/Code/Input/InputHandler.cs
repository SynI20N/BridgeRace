using UnityEngine;

[RequireComponent(typeof(Input))]
public class InputHandler : MonoBehaviour
{
    private Input _input;
    [SerializeField] private PlaneMovement _movement;

    private void Start()
    {
        _input = GetComponent<Input>();
    }

    private void Update()
    {
        if (_input.Active && _movement != null)
        {
            _movement.EvaluateInfo(_input.GetInput());
        }
    }
}
