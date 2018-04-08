using UnityEngine;
using System.Collections;
using PowerInject;
using System.Collections.Generic;

[Insert]
public class MapField 
{
    private Dictionary<MapType, List<MapUnit>> mapPool;
    private Dictionary<int,MapUnit> mapUnits;
    private List<MapUnit> addCache;
    private List<MapUnit> removeCache;
    private MapGrid[][] mapGrids;


    private bool IsGridMoved(Vector2 point)
    {
        int row = Mathf.CeilToInt((point.y - MapGrid.Origin().y) / MapGrid.Size());
        int col = Mathf.CeilToInt((point.x - MapGrid.Origin().x) / MapGrid.Size());
        return mapGrids[row][col].isMoved;
    }





    public MapUnit Get(MapType type)
    {
        List<MapUnit> pool;
        if (mapPool.TryGetValue(type, out pool))
        {
            MapUnit res = null;
            if (pool.Count > 0)
            {
                res = pool[0];
                pool.RemoveAt(0);
                return res;
            }
            else
            {
                res = CreateUnit(type);
                return res;
            }
        }
        else
        {
            pool = new List<MapUnit>();
            mapPool.Add(type, pool);
            MapUnit res = null;
            res = CreateUnit(type);
            return res;
        }
    }

    public void Collect(MapUnit unit)
    {
        List<MapUnit> pool;
        if (mapPool.TryGetValue(unit.type, out pool))
        {
            pool.Add(unit);
        }
        else
        {
            Debug.Log("MapField:::Fail to return component back to pool;");
        }
    }

    private MapUnit CreateUnit(MapType type)
    {
        MapUnit res = null;
        switch (type)
        {
            case MapType.Circle:
                res = new CircleMapUnit();
                break;
        }
        return res;
    }
    public void AddUnit(MapUnit unit)
    {
        addCache.Add(unit);
    }
    public void RemoveUnit(MapUnit unit)
    {
        removeCache.Add(unit);
    }

    public bool CheckIsMoved(MapUnit unit)
    {
        Vector2 start = unit.positon;
        Vector2 end = unit.positon + unit.speed;
        // check static gird
        if (!IsGridMoved(end)) return false;
        foreach(var )
    }


    public void Init()
    {

    }
    
    public void Update()
    {
        while (addCache.Count > 0)
        {
            MapUnit unit = addCache[0];
            addCache.RemoveAt(0);
            if (!mapUnits.ContainsKey(unit.id))
            {
                mapUnits.Add(unit.id, unit);
            }
        }
    }
}
