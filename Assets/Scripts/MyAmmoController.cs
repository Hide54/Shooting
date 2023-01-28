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
    [SerializeField, Header("BoxCastのx座標の半径")]
    private float _rayX = default;
    [SerializeField, Header("BoxCastのy座標の半径")]
    private float _rayY = default;
    [SerializeField, Header("BoxCastのz座標の半径")]
    private float _rayZ = default;

    private PoolManager _objectPool = default;


    private void Awake()
    {
        _objectPool = this.transform.parent.GetComponent<PoolManager>();
    }

    private void FixedUpdate()
    {
        this.transform.position += this.transform.forward * _speed * Time.deltaTime;

        //rayが当たったら自分を回収
        RaycastHit _hit;
        if (Physics.BoxCast(this.transform.position, new Vector3(_rayX, _rayY, _rayZ), Vector3.one * 0.1f, out _hit, Quaternion.identity, 0.1f))
        {
            if (_hit.collider.CompareTag("Core") || _hit.collider.CompareTag("Wall"))
            {
                Debug.Log("lol");
                HideFromStage();
            }
        }
    }

    // 弾の位置と向き初期化処理
    public void Init(Vector3 position, Quaternion rotation)
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
    }

    //BoxCastを疑似的に表示する処理
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(_rayX * 2, _rayY * 2, _rayZ * 2));
    }

    //自身を回収
    public void HideFromStage()
    {
        _objectPool.MACollect(this);
    }
}
