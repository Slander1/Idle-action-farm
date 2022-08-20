using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay;
using UnityEngine;

public class ObjectContainer : MonoBehaviour
{
    [SerializeField] private Vector3Int _objectsCountMatrix;

    private SortedDictionary<int, MovableObject> _objects = new SortedDictionary<int, MovableObject>();
    public IReadOnlyDictionary<int, MovableObject> Objects => _objects;
    private int _nextObjectId = 0;
    public int MaxObjectsCount => _objectsCountMatrix.x * _objectsCountMatrix.y * _objectsCountMatrix.z;

    public event Action OnObjectsChanged;

    public void AddObject(MovableObject model)
    {
        var position = GetObjectPosition(Objects.Count);
        model.transform.SetParent(transform);
        model.SetLocalPosition(position, transform);
        _objects.Add(_nextObjectId++, model);
        OnObjectsChanged?.Invoke();
    }

    public MovableObject RemoveLastObject()
    {
        var lastObjectModel = Objects.Last().Value;
        RemoveObject(_objects.Last().Key);
        return lastObjectModel;
    }
    public void RemoveObject(int id)
    {
        _objects.Remove(id);
        FixObjectsPosition();
        OnObjectsChanged?.Invoke();
    }

    private void FixObjectsPosition()
    {
        var id = 0;
        foreach (var keyValuePair in _objects)
        {
            var position = GetObjectPosition(id);
            if ((keyValuePair.Value.transform.position - position).sqrMagnitude > 0.0001f)
            {
                keyValuePair.Value.SetLocalPosition(position, transform);
            }
            id++;
        }
        
    }

    private Vector3 GetObjectPosition(int id)
    {
        var matrixPosition = GetObjectPositionInMatrix(id);
        var cellSize = new Vector3(1 / (float)(_objectsCountMatrix.x),
            1 / (float)(_objectsCountMatrix.y),
            1 / (float)(_objectsCountMatrix.z));
        
        var cellPosition = new Vector3(matrixPosition.x * cellSize.x + cellSize.x * 0.5f  -0.5f,
            matrixPosition.y * cellSize.y + cellSize.y * 0.5f -0.5f,
            matrixPosition.z * cellSize.z + cellSize.z * 0.5f -0.5f);
        return cellPosition;
    }

    private Vector3Int GetObjectPositionInMatrix(int id)
    {
        var x = id % _objectsCountMatrix.x;
        var z = (id / _objectsCountMatrix.x) % _objectsCountMatrix.z;
        var y = (id / (_objectsCountMatrix.x * _objectsCountMatrix.z)) % _objectsCountMatrix.y;
        return new Vector3Int(x,y,z);
    }
}
