using DG.Tweening;
using UnityEngine;
using static UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlaneMovement : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody _rigidbody;
    private Plane _movementSpace;
    private Animator _animator;
    private Sequence _sequence;

    [SerializeField] private float _speed;

    private void Awake()
    {
        DOTween.Init();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();

        _movementSpace = new Plane();
        _sequence = DOTween.Sequence();
        ApplySpace(_transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = CalculateNormal(collision);
        if (Angle(_transform.up, normal) < 45f)
        {
            Transform newTransform = CalculateSpace(normal);
            ApplySpace(newTransform);
            Destroy(newTransform.gameObject);
        }
    }

    private Vector3 CalculateNormal(Collision collision)
    {
        Vector3 result = zero;
        for (int i = 0; i < collision.contactCount; i++)
        {
            result += collision.contacts[i].normal;
        }
        return result;
    }

    private Transform CalculateSpace(Vector3 normal)
    {
        Transform result = new GameObject().transform;

        result.up = normal;

        return result;
    }

    private void ApplySpace(Transform space)
    {
        _movementSpace.Forward = space.forward * _speed;
        _movementSpace.Right = space.right * _speed;
        _movementSpace.Backward = -space.forward * _speed;
        _movementSpace.Left = -space.right * _speed;
    }

    public void EvaluateInfo(TouchInfo info)
    {
        if (info.Pressed)
        {
            _animator.SetTrigger("Run");
            _animator.ResetTrigger("Stop");
            Move(info);
        }
        else
        {
            _animator.SetTrigger("Stop");
            _animator.ResetTrigger("Run");
            Stop();
        }
    }

    private void Move(TouchInfo info)
    {
        Vector3 newForward = Dot(Vector2.up, info.Direction) * _movementSpace.Forward;
        Vector3 newRight = Dot(Vector2.right, info.Direction) * _movementSpace.Right;
        float fallSpeed = _rigidbody.velocity.y;

        Vector3 sum = newForward + newRight;
        _rigidbody.velocity = new Vector3(sum.x, fallSpeed, sum.z);

        Vector3 projectedVelocity = new Vector3(sum.x, 0, sum.z);
        float moveAngle = SignedAngle(_transform.forward, projectedVelocity, _transform.up);
        Vector3 newRotation = new Vector3(0, _transform.eulerAngles.y + moveAngle, 0);
        _sequence.Append(_transform.DORotate(newRotation, 0.5f));
    }

    private void Stop()
    {
        _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
    }
}
