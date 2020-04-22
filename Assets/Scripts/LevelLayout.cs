using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayout : MonoBehaviour
{

    public Vector3 levelSize;
    public int defaultEntityCapacity = 32;

    private GameObject[,,] grid;
    private Dictionary<GameObject, Vector3> activeEntities;

    public static LevelLayout instance;

    public static LevelLayout GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        grid = new GameObject[(int) levelSize.x, (int) levelSize.y, (int) levelSize.z];
        activeEntities = new Dictionary<GameObject, Vector3>(defaultEntityCapacity);
        LevelLayout.instance = this;
    }

    public void AddEntity(GameObject entity)
    {
        Vector3 pos = entity.transform.position;

        activeEntities.Add(entity, pos);
        grid[(int) pos.x, (int) pos.y, (int) pos.z] = entity;
    }

    public bool IsEntityInGrid(GameObject entity)
    {
        return activeEntities.ContainsKey(entity);
    }

    public Vector3 GetEntityPosition(GameObject entity)
    {
        return activeEntities[entity];
    }

    public GameObject GetAt(int x, int y, int z)
    {
        if (x < 0 || x >= levelSize.x) return null; // new position isn't bounded on x-axis
        if (y < 0 || y >= levelSize.y) return null; // new position isn't bounded on y-axis
        if (z < 0 || z >= levelSize.z) return null; // new position isn't bounded on z-axis

        return grid?[x, y, z];
    }

    public bool MoveObject(GameObject entity, int x, int y, int z)
    {
        if (!IsEntityInGrid(entity)) return false; // entity does not exist within the game space
        if (x < 0 || x >= levelSize.x) return false; // new position isn't bounded on x-axis
        if (y < 0 || y >= levelSize.y) return false; // new position isn't bounded on y-axis
        if (z < 0 || z >= levelSize.z) return false; // new position isn't bounded on z-axis

        Vector3 oldPos = activeEntities[entity];

        grid[(int) oldPos.x, (int) oldPos.y, (int) oldPos.z] = null;
        activeEntities[entity] = new Vector3(x, y, z);
        grid[x, y, z] = entity;

        return true;
    }

}
