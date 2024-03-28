using System.Collections.Generic;
using UnityEngine;

public class Meat : Ingredient
{
    [SerializeField] private List<InteractableData> _data;
    private int _counter = 0;
    private void Awake()
    {
        UpdateNewComponents(0);
    }
    public override GameObject GetGameObject()
    {
        if (!CanSpawn)
        {
            return null;
        }
        _counter += 1;
        GameObject obj = Instantiate(_data[Mathf.Clamp(_counter, 0, _data.Count - 1)].ObjectToSpawn);
        obj.transform.position = transform.position;
        if (_counter >= _data.Count)
        {
            Destroy(gameObject);
        }
        else
        {
            UpdateNewComponents(_counter);
            return obj;
        }
        return null;
    }
    public virtual void UpdateNewComponents(int index)
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (_data[index].Collider != null)
        {
            boxCollider.center = _data[index].Collider.center;
            boxCollider.size = _data[index].Collider.size;
        }
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (_data[index].Renderer != null)
        {
            meshRenderer.materials = _data[index].Renderer.sharedMaterials;
        }
        Rigidbody rb = GetComponent<Rigidbody>();
        if (_data[index].Rigidbody != null)
        {
            rb.mass = _data[index].Rigidbody.mass;
            rb.collisionDetectionMode = _data[index].Rigidbody.collisionDetectionMode;
        }
        Rb = rb;
        MeshFilter filter = GetComponent<MeshFilter>();
        if (_data[index].Filter != null)
        {
            filter.mesh = _data[index].Filter.sharedMesh;

        }
    }
}
