using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmmoController : MonoBehaviour
{
    [SerializeField, Header("弾の移動速度")]
    private float _speed = default;
    [SerializeField, Header("自分を設定")]
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

    //自身を回収
    public void HideFromStage()
    {
        _objectPool.EACollect1(this);
    }
}
