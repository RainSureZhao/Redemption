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

    public float shootCoolDownTime = 0.75f; // 表示单发模式的冷却时间

    public float recoilForce = 2f; // 枪的后坐力

    public GameObject graphics;
}
