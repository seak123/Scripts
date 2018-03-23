using UnityEngine;
using System.Collections;

interface IComponent
{
    void OnEnter();
    void OnUpdate();
    void OnLeave();
    void CleanUp();
}