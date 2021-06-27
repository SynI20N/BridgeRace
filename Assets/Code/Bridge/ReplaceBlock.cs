using DG.Tweening;
using System;
using UnityEngine;

public class ReplaceBlock : MonoBehaviour
{
    private GameObject _blockPrefab;
    private GameObject _thisBlock;

    private Color _blockColor;

    public void Init(GameObject blockPrefab)
    {
        _blockPrefab = blockPrefab;
        _thisBlock = gameObject;

        _blockColor = _thisBlock.GetComponent<MeshRenderer>().material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Backpack backpack;
        if (collision.gameObject.TryGetComponent(out backpack) && !backpack.IsEmpty())
        {
            GameObject buildObject = backpack.TakeObject();
            Color color = buildObject.GetComponent<MeshRenderer>().material.color;
            if (!color.Equals(_blockColor))
            {
                Recolor(color);
                Replace(buildObject);
            }
            else
            {
                backpack.PushObject(buildObject);
            }
        }
    }

    private void Recolor(Color color)
    {
        _blockColor = color;
    }

    private void Replace(GameObject buildObject)
    {
        GameObject block = Instantiate(_blockPrefab, _thisBlock.transform.position, Quaternion.identity);
        block.AddComponent<ReplaceBlock>();
        Destroy(_thisBlock);
        _thisBlock = block;
        _thisBlock.GetComponent<MeshRenderer>().material.color = _blockColor;

        buildObject.transform.DOKill();
        Destroy(buildObject);
    }
}
