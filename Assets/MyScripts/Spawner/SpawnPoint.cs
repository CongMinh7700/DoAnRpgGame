using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : RPGMonoBehaviour
{
    [Header("Spawn Point")]
    [SerializeField] protected List<Transform> points;
    [SerializeField]protected List<int> availableIndices = new List<int>();
    public static bool canSpawn = true;
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
    //Code cũ
    //public virtual Transform GetRandomPoint()
    //{
    //    int random = Random.Range(0, points.Count);
    //    return points[random];
    //}

    private void Update()
    {
        UpdateCanSpawn();
    }

    //Lập 1 list để lưu trữ các point chưa bị ghi đè
    //thêm vào danh sách
    public virtual Transform GetRandomPoint()
    {
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < points.Count; i++)
        {
            PointTrigger trigger = points[i].GetComponentInChildren<PointTrigger>();
            
            if (trigger != null && trigger.IsOccupied())
            {

                continue;  
            }
            availableIndices.Add(i);
        }

        if (availableIndices.Count == 0)
        {
            Debug.LogWarning("All spawn points are currently occupied.");
            return null;
        }

        int randomIndex = Random.Range(0, availableIndices.Count);
        int selectedIndex = availableIndices[randomIndex];
        return points[selectedIndex];
    }
    //Check canSpawn
    public virtual void UpdateCanSpawn()
    {
        canSpawn = false;
        foreach (Transform point in points)
        {
            PointTrigger trigger = point.GetComponentInChildren<PointTrigger>();
            if (trigger != null && !trigger.IsOccupied())
            {
                canSpawn = true;
                break;
            }
        }
    }
}

