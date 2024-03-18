using UnityEngine;

public class Meat : Ingredient
{
    [SerializeField] private GameObject _gameObjectToSpawn;
    public override GameObject GetGameObject()
    {
        GameObject go = base.GetGameObject();
        if (CanSpawn)
        {
            GameObject _new = Instantiate(_gameObjectToSpawn);
            _new.transform.position = go.transform.position;
            Debug.Log(_new.transform.position);
        }
        return go;
    }
}
