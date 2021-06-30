using DG.Tweening;
using UnityEngine;
using static UnityEngine.Vector3;

public class Bridge : MonoBehaviour
{
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] [Range(0,30)] private int _blockMaxCount;

    private Transform _bridgeBuilder;
    private int _blockCount;

    private void Start()
    {
        _bridgeBuilder = GetComponent<Transform>();
        _blockCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        Backpack backpack;
        if (CanTake(other, out backpack))
        {
            GameObject brick = backpack.TakeObject();
            BuildBlock(brick);
            _blockCount++;
        }
    }

    private bool CanTake(Collider other, out Backpack backpack)
    {
        return other.gameObject.TryGetComponent(out backpack) && !backpack.IsEmpty() && _blockCount < _blockMaxCount;
    }

    private void BuildBlock(GameObject buildObject)
    {
        Color color = buildObject.GetComponent<MeshRenderer>().material.color;
        GameObject block = Instantiate(_blockPrefab, _bridgeBuilder.position, _bridgeBuilder.rotation);
        _bridgeBuilder.position += _bridgeBuilder.forward * _blockPrefab.transform.localScale.z + up * 0.45f;

        block.GetComponent<MeshRenderer>().material.color = color;
        block.AddComponent<ReplaceBlock>().Init();

        buildObject.transform.DOKill();
        Destroy(buildObject);
    }
}
