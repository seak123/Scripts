using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerInject;

public class GameRoot:MonoBehaviour {

    [Inject]
    BattleField battleField;

    [Inject]
    TimeManager timeManager;

    private void Awake()
    {
        Init();
        timeManager.onUpdate += battleField.Update;
    }

    private void Init()
    {
        battleField.Init();
    }
}
