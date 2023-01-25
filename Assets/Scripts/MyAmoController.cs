using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自分の弾の管理スクリプト
/// </summary>
public class MyAmoController : MonoBehaviour
{
    [SerializeField, Header("弾の移動速度")]
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

    //自身を回収
    public void HideFromStage()
    {
        _objectPool.MACollect(this);
    }
}
