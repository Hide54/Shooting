using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    #region
    [SerializeField,Header("オブジェクトプールの管理スクリプトを設定")]
    private PoolManager _objectPool = default;
    [SerializeField, Header("ボスの体力")]
    private int _coreHp = default;
    [SerializeField, Header("ボスの移動速度")]
    private float _coreSpeed = default;
    [SerializeField, Header("ボスの弾を撃つ間隔")]
    private float _interval = default;

    private Transform _enemyTransform = default;
    private float _damageArea = default;
    #endregion

    private void Awake()
    {
        StartCoroutine(Shoot());
    }

    //ダメージ処理
    public void EnemyDamage()
    {
        //_damageArea=_enemyTransform.localScale.x* _enemyTransform.localScale.x
    }

    public IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(_interval);
            Vector3 _pos = this.transform.position;
            Quaternion _rot = this.transform.rotation;

            _objectPool.EnemyAmmoLaunch2(_pos, _rot);
        }
    }
}
