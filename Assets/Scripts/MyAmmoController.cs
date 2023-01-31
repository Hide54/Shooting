using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自分の弾の管理スクリプト
/// </summary>
public class MyAmmoController : MonoBehaviour
{
    [SerializeField, Header("弾の速度")]
    private float _speed = default;
    [SerializeField, Header("ダメージ")]
    private int _damage = default;

    //オブジェクトプール
    private PoolManager _objectPool = default;


    private void Awake()
    {
        _objectPool = this.transform.parent.GetComponent<PoolManager>();
    }

    private void FixedUpdate()
    {
        //弾の移動
        this.transform.position += this.transform.forward * _speed * Time.deltaTime;
    }

    // 弾の位置と向き初期化処理
    public void Init(Vector3 position, Quaternion rotation)
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
    }

    //オブジェクトに当たった処理
    public void OnTriggerEnter(Collider obj)
    {
        Damageable _damageable = obj.gameObject.GetComponent<Damageable>();

        //壁か壊せる敵の弾に当たったら何もせずに自分を回収
        if (obj.CompareTag("Wall") || obj.CompareTag("EAmo1"))
        {
            HideFromStage();
        }

        //敵に当たったら敵の体力を1減らして自分を回収
        else if (obj.CompareTag("Core"))
        {
            _damageable.Damage(_damage);
            HideFromStage();
        }
    }

    //自身を回収
    public void HideFromStage()
    {
        _objectPool.MACollect(this);
    }
}
