using DG.Tweening;
using UnityEngine;

public class ReplaceBlock : MonoBehaviour
{
    private GameObject _thisBlock;
    private Color _blockColor;

    public void Init()
    {
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
                Spend(buildObject);
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

        _thisBlock.GetComponent<MeshRenderer>().material.color = _blockColor;
    }

    private void Spend(GameObject consumable)
    {
        consumable.transform.DOKill();
        Destroy(consumable);
    }
}
