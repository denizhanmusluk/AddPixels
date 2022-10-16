using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverSystem;
public class Controller : Subject
{
    public override void SpesificStart()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Notify(NotificationType.Win);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Notify(NotificationType.Fail);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Notify(NotificationType.Start);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Notify(NotificationType.Fail);
        }
    }
}
