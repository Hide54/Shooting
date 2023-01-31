using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����̒e�̊Ǘ��X�N���v�g
/// </summary>
public class MyAmmoController : MonoBehaviour
{
    [SerializeField, Header("�e�̑��x")]
    private float _speed = default;
    [SerializeField, Header("�_���[�W")]
    private int _damage = default;

    //�I�u�W�F�N�g�v�[��
    private PoolManager _objectPool = default;


    private void Awake()
    {
        _objectPool = this.transform.parent.GetComponent<PoolManager>();
    }

    private void FixedUpdate()
    {
        //�e�̈ړ�
        this.transform.position += this.transform.forward * _speed * Time.deltaTime;
    }

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

        //�ǂ��󂹂�G�̒e�ɓ��������牽�������Ɏ��������
        if (obj.CompareTag("Wall") || obj.CompareTag("EAmo1"))
        {
            HideFromStage();
        }

        //�G�ɓ���������G�̗̑͂�1���炵�Ď��������
        else if (obj.CompareTag("Core"))
        {
            _damageable.Damage(_damage);
            HideFromStage();
        }
    }

    //���g�����
    public void HideFromStage()
    {
        _objectPool.MACollect(this);
    }
}
