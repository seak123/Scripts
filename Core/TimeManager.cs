using UnityEngine;
using System.Collections;
using System;
using PowerInject;

[Insert]
public class TimeManager : MonoBehaviour
{
    private static float startTime;
    private static float nowTime;
    private static float delta;

    public Action onUpdate;
    public Action laterUpdate;

    public static float StartTime
    {
        get { return startTime; }
    }

    public static float NowTime
    {
        get { return nowTime; }
    }

    public static float Delta
    {
        get { return delta; }
    }


    private void Update()
    {
        nowTime = Time.realtimeSinceStartup - startTime;
        delta = Time.deltaTime;
        if (onUpdate != null) onUpdate();
        if (laterUpdate != null) laterUpdate();
    }
}
