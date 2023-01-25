using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����̒e�̊Ǘ��X�N���v�g
/// </summary>
public class MyAmoController : MonoBehaviour
{
    [SerializeField, Header("�e�̈ړ����x")]
    private float _speed = default;

    private PoolManager _objectPool;

    private void Awake()
    {
        _objectPool = transform.parent.GetComponent<PoolManager>();
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;

    }

    public void ShowInStage(Vector3 _pos)
    {
        transform.position = _pos;
    }

    //���g�����
    public void HideFromStage()
    {
        _objectPool.MACollect(this);
    }
}
