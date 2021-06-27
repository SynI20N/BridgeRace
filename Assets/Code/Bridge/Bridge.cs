using DG.Tweening;
using UnityEngine;
using static UnityEngine.Vector3;

public class Bridge : MonoBehaviour
{
    [SerializeField] private GameObject _blockPrefab;

    private Transform _bridgeBuilder;

    private void Start()
    {
        _bridgeBuilder = GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Backpack backpack;
        if (other.gameObject.TryGetComponent(out backpack) && !backpack.IsEmpty())
        {
            GameObject brick = backpack.TakeObject();
            BuildBlock(brick);
        }
    }

    private void BuildBlock(GameObject buildObject)
    {
        Color color = buildObject.GetComponent<MeshRenderer>().material.color;
        GameObject block = Instantiate(_blockPrefab, _bridgeBuilder.position, _bridgeBuilder.rotation);
        _bridgeBuilder.position += _bridgeBuilder.forward * _blockPrefab.transform.localScale.z + up * 0.45f;

        block.GetComponent<MeshRenderer>().material.color = color;
        block.AddComponent<ReplaceBlock>().Init(_blockPrefab);

        buildObject.transform.DOKill();
        Destroy(buildObject);
    }
}
