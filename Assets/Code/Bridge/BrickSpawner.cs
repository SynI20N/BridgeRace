using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BrickSpawner : MonoBehaviour
{
    private const int maxBricks = 20;
    private const float spawnCooldown = 2f;

    [SerializeField] private GameObject _brickPrefab;

    private Transform _spawner;
    private Collider _collider;
    private List<GameObject> _bricks;
    private float _spawnHeight;
    private float _timer;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _bricks = new List<GameObject>(maxBricks);
        _spawner = GetComponent<Transform>();
        _spawnHeight = _spawner.position.y + (_spawner.localScale.y / 2);

        SpawnBricks(maxBricks);
    }

    private void Update()
    {
        CheckBricks();
        if (Time.time > _timer && _bricks.Count < maxBricks / 2)
        {
            _timer = Time.time + spawnCooldown;
            SpawnBricks(3);
        }
    }

    private void CheckBricks()
    {
        for (int i = 0; i < _bricks.Count; i++)
        {
            if (_bricks[i].transform.position.y != _spawnHeight)
            {
                _bricks.Remove(_bricks[i]);
                i--;
            }
        }
    }

    private void SpawnBricks(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 startPos = Extensions.RandomPointInCollider(_collider);
            startPos.y = _spawnHeight;
            _bricks.Add(Instantiate(_brickPrefab, startPos, Quaternion.identity));
        }
    }
}
