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

    public float shootRate = 5f; // 1����Դ���ӵ������С�ڵ���0����Ϊ������

    public float shootCoolDownTime = 0.75f; // ��ʾ����ģʽ����ȴʱ��

    public float recoilForce = 2f; // ǹ�ĺ�����

    public GameObject graphics;
}
