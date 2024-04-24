using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : RPGMonoBehaviour
{
    [SerializeField] protected List<Transform> points;

    protected override void LoadComponents()
    {
        this.LoadPoints();
    }
    protected virtual void LoadPoints()
    {
        if (this.points.Count > 0) return;
        foreach(Transform point in transform)
        {
            points.Add(point);
        }
    }
    public virtual Transform GetRandomPoint()
    {
        int random = Random.Range(0, points.Count);
        return points[random];
    }
}
