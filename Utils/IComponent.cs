using UnityEngine;
using System.Collections;

interface IComponent
{
    void OnEnter(GameObject obj);
    void OnUpdate(float delta);
    void OnLeave();
    void CleanUp();
    void InjectVO(UnitVO vo);
}