using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreController : MonoBehaviour, Damageable
{
    #region
    [SerializeField, Header("オブジェクトプールの管理スクリプトを設定")]
    private PoolManager _objectPool = default;
    [SerializeField, Header("オーディオソースを設定")]
    private AudioSource _audio = default;
    [SerializeField, Header("敵が鳴らすSEを設定")]
    private AudioClip[] _clips = default;
    [SerializeField, Header("ボスの体力")]
    private int _coreHp = default;
    [SerializeField, Header("ボスの移動速度")]
    private float _coreSpeed = default;
    [SerializeField, Header("ボスの弾を撃つ間隔")]
    private float _interval = default;
    [SerializeField,Header("ボスが死ぬまでの時間")]
    private float _waitTime = default;
    [SerializeField,Header("弾の発射角度")]
    private int _angle = default;

    private Vector3 _velocity = default;
    private Rigidbody _rb = default;
    #endregion

    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody>();
        StartCoroutine(Shoot());
    }

    public void Move(GameObject target)
    {
        _velocity=new Vector3(target.transform.position.x,0, target.transform.position.z);
        _rb.velocity = _velocity.normalized * _coreSpeed;
    }

    //コアのダメージ処理
    public void Damage(int value)
    {
        _audio.PlayOneShot(_clips[0]);
        _coreHp -= value;
        if (_coreHp == 0)
        {
            Death();
        }
        Debug.Log(_coreHp);
    }

    //コアの死亡処理
    public void Death()
    {
        StartCoroutine(Die());
        Debug.Log("ゲームクリア");
    }

    public IEnumerator Die()
    {
        _audio.PlayOneShot(_clips[1]);
        yield return new WaitForSeconds(_waitTime);
        this.gameObject.SetActive(false);
    }


    public IEnumerator Shoot()
    {
        int _rad1 = 0;
        Quaternion _rot = Quaternion.identity;
        while (true)
        {
            Vector3 _pos = this.transform.position;

            _rot.eulerAngles = new Vector3(0, _rad1, 0);
            _objectPool.EnemyAmmoLaunch2(_pos, _rot);
            _rad1 += _angle;
            yield return new WaitForSeconds(_interval);

            _rot.eulerAngles = new Vector3(0, _rad1, 0);
            _objectPool.EnemyAmmoLaunch1(_pos, _rot);
            _rad1 += _angle;
            yield return new WaitForSeconds(_interval);
        }
    }
}
