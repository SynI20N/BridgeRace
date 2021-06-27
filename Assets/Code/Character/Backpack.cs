using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Vector3;

public class Backpack : MonoBehaviour
{
    [SerializeField] private Material _objectMat;

    private const float tweenTime = 0.7f;
    private Vector3 backpackStartPos = (back * 0.5f) + up;

    private Stack<GameObject> _objects;
    private Vector3 _nextPos;

    private void Start()
    {
        _objects = new Stack<GameObject>();
        _nextPos = backpackStartPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        Material material = other.gameObject.GetComponent<MeshRenderer>().material;
        Color color = material.color;
        if (color.Equals(_objectMat.color))
        {
            Destroy(other.gameObject.GetComponent<Collider>());
            PushObject(other.gameObject);
        }
    }

    public void PushObject(GameObject objectToPush)
    {
        objectToPush.transform.SetParent(transform);
        objectToPush.transform.DOLocalMove(_nextPos, tweenTime);
        objectToPush.transform.DOLocalRotate(zero, tweenTime);

        _nextPos += up * (objectToPush.transform.localScale.y + 0.05f);
        _objects.Push(objectToPush);
    }

    public GameObject TakeObject()
    {
        GameObject _object = _objects.Pop();
        _nextPos -= up * (_object.transform.localScale.y + 0.05f);

        return _object;
    }

    public GameObject PeekObject()
    {
        GameObject _object = _objects.Peek();

        return _object;
    }

    public bool IsEmpty()
    {
        return !Convert.ToBoolean(_objects.Count);
    }
}
