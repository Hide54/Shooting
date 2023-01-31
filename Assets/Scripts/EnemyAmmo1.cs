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

    //�I�u�W�F�N�g�ɓ�����������
    public void OnTriggerEnter(Collider obj)
    {
        Damageable _damageable = obj.gameObject.GetComponent<Damageable>();

        //�ǂ��v���C���[�̒e�ɓ��������牽�������Ɏ��������
        if (obj.CompareTag("Wall") || obj.CompareTag("MAmo"))
        {
            HideFromStage();
        }
        //�v���C���[�ɓ���������v���C���[�̗̑͂�1���炵�Ď��������
        else if (obj.CompareTag("Player"))
        {
            _damageable.Damage(_enemyDamage);
            HideFromStage();
        }
    }

    //���g�����
    public void HideFromStage()
    {
        _objectPool.EACollect1(this);
    }
}
