using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmmoController : MonoBehaviour
{
    [SerializeField, Header("�e�̈ړ����x")]
    private float _speed = default;
    [SerializeField, Header("������ݒ�")]
    private GameObject _amo = default;

    private PoolManager _objectPool;

    private void Awake()
    {
        _objectPool = transform.parent.GetComponent<PoolManager>();
        gameObject.SetActive(false);
    }
    public void OnBecameInvisible()
    {
        HideFromStage();
    }

    public void ShowInStage(Vector3 _pos)
    {
        transform.position = _pos;
    }

    //���g�����
    public void HideFromStage()
    {
        _objectPool.EACollect1(this);
    }
}