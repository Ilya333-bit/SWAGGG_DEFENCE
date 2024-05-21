using System.Collections.Generic;
using UnityEngine;

public class PathPoints: MonoBehaviour
{
    private List<Vector3> _pathPoints = new List<Vector3>();
    [SerializeField] private string _pathPointName = "path end";
    [SerializeField] private Transform _vector3MainTower;

    private void Start()
    {
        _vector3MainTower = FindObjectOfType<MainTower>().GetComponent<Transform>();
        for (int i = 0; i < 25; i++)
        {   
            _pathPoints.Add(transform.GetChild(i).position);

            if (transform.GetChild(i).name == _pathPointName) break;
        }
    }

    public Vector3 GetPathPoint(int index)
    {
        if (index >= _pathPoints.Count) return _vector3MainTower.position;
        return _pathPoints[index];
    }

    public int GetLength()
    {
        return _pathPoints.Count;
    }
}