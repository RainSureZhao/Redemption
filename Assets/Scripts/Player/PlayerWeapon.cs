using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerWeapon
{
    public string Name = "AWM";
    public int Damage = 10;
    public float Range = 100f;

    public float shootRate = 5f; // 1秒可以打的子弹，如果小于等于0，则为单发。

    public GameObject graphics;
}
