using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmmoController : MonoBehaviour
{
    [SerializeField, Header("弾の移動速度")]
    private float _speed = default;
    [Header("弾のダメージ")]
    public int _enemyDamage = default;

    [HideInInspector]
    //オブジェクトプール
    public PoolManager _objectPool = default;

    private void Awake()
    {
        _objectPool = this.transform.parent.GetComponent<PoolManager>();
    }

    private void FixedUpdate()
    {
        this.transform.position += this.transform.forward * _speed * Time.deltaTime;
    }
}
