using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmmo1 : EnemyAmmoController
{
    // �e�̈ʒu�ƌ�������������
    public void Init(Vector3 position, Quaternion rotation)
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
    }

    //���g�����
    public void HideFromStage()
    {
        _objectPool.EACollect1(this);
    }
}
