using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoSingleton<HUDController>
{
    public void DestroyHUD()
    {
        foreach(var gameObjects in GetComponentsInChildren<Transform>())
        {
            Destroy(gameObjects);
        }
        Destroy(gameObject);
    }
}
