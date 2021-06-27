using UnityEngine;

public class TPCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Transform _objectTransform;
    private Vector3 _linkVector;

    private void Start()
    {
        if (_camera == null)
            return;

        _linkVector = _camera.transform.position;
        _objectTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        _camera.transform.position = _objectTransform.position + _linkVector;
    }
}