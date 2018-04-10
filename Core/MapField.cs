using UnityEngine;
using System.Collections;
using PowerInject;
using System.Collections.Generic;

[Insert]
public class MapField 
{
    private Dictionary<MapType, List<MapUnit>> mapPool;
    private Dictionary<int,MapUnit> creatureUnits;
    private Dictionary<int, MapUnit> effectUnits;
    private List<MapUnit> addCache;
    private List<MapUnit> removeCache;
    private MapGrid[][] mapGrids;


    private bool IsGridMoved(Vector2 point, float size)
    {
        int row_s = Mathf.CeilToInt((point.y - size - MapGrid.Origin().y) / MapGrid.Size());
        int row_e = Mathf.CeilToInt((point.y + size - MapGrid.Origin().y) / MapGrid.Size());
        int col_s = Mathf.CeilToInt((point.x - size - MapGrid.Origin().x) / MapGrid.Size());
        int col_e = Mathf.CeilToInt((point.x + size - MapGrid.Origin().x) / MapGrid.Size());
        if (row_s <= 0 || row_e > MapGrid.MaxRow() || col_s <= 0 || col_e > MapGrid.MaxCol()) return false;
        bool res=true;
        for(int i = row_s; i < row_e + 1; i++)
        {
            for(int j = col_s; j < col_e + 1; j++)
            {
                res = res && mapGrids[i][j].isMoved;
            }
        }
        return res;

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
        if (mapPool.TryGetValue(unit.maptype, out pool))
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
                res = new MapUnit();
                res.maptype = MapType.Circle;
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

    public bool CheckIsMoved(CircleMapUnit unit)
    {
        Vector2 start = unit.positon;
        Vector2 end = unit.positon + unit.speed;
        // check static gird
        if (!IsGridMoved(end,unit.radius)) return false;
        foreach(var u in creatureUnits)
        {
            if (IsCollided(unit, u.Value)) return false;
        }
        return true;
    }

    private bool IsCollided(MapUnit source, MapUnit target)
    {
        switch (source.maptype)
        {
            case MapType.Circle:
                CircleMapUnit s = source as CircleMapUnit;
                if (target.maptype == MapType.Circle)
                {
                    CircleMapUnit t = target as CircleMapUnit;
                    if (Vector2.Distance(source.positon+source.speed, target.positon+target.speed) > s.radius + t.radius)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                break;
        }
        return false;
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
            switch (unit.unittype)
            {
                case UnitType.creature:
                    if (!creatureUnits.ContainsKey(unit.id))
                    {
                        creatureUnits.Add(unit.id, unit);
                    }

                    break;
                case UnitType.effect:
                    if (!effectUnits.ContainsKey(unit.id))
                    {
                        effectUnits.Add(unit.id, unit);
                    }
                    break;

            }
            
        }
        foreach(var c in creatureUnits)
        {
            if(CheckIsMoved(c.Value as CircleMapUnit))
            {
                c.Value.next = c.Value.positon + c.Value.speed;
            }
            else
            {
                c.Value.next = c.Value.positon;
            }
        }
        foreach (var e in effectUnits)
        {
            e.Value.next = e.Value.positon + e.Value.speed;
        }
        
        foreach(var e in effectUnits)
        {
            Group sg = e.Value.unit.Group;
            foreach(var c in creatureUnits)
            {
                if (c.Value.unit.Group != sg && IsCollided(e.Value, c.Value))
                {
                    EffectUnit effectunit = e.Value.unit as EffectUnit;
                    if (!effectunit.IsContainKey(c.Value.id))
                    {
                        effectunit.AddUnit(c.Value.id);
                    }
                }
            }
        }




        //view




    }
}
