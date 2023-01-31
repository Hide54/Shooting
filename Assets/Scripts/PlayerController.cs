using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, Damageable
{
    #region
    [SerializeField, Header("オブジェクトプールの管理スクリプトを設定")]
    private PoolManager _objectPool = default;
    [SerializeField, Header("対応するスクリプトを設定")]
    private InputActions _input = default;
    [SerializeField, Header("オーディオソースを設定")]
    private AudioSource _audio = default;
    [SerializeField, Header("プレイヤーが鳴らすSEを設定")]
    private AudioClip[] _clips = default;

    [SerializeField, Header("プレイヤーの移動速度を設定")]
    private float _speed = default;
    [SerializeField, Header("プレイヤーの体力を設定")]
    private float _hp = default;

    [SerializeField, Header("弾の発射間隔を設定")]
    private float _interval = default;
    [Header("弾の速度を設定")]
    public float _ammoSpeed = default;

    //コントローラーの左スティックの入力値
    private Vector3 _leftInput = default;
    //コントローラーの右スティックの入力値
    private Vector3 _rightInput = default;
    //Lトリガーの入力値
    private bool _leftTrigger = default;

    //自身のRigidbody
    private Rigidbody _rb = default;
    //移動速度
    private Vector3 _velocity = default;
    //プレイヤーの向き
    private Vector3 _direction = default;

    #endregion

    private void Awake()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
        StartCoroutine(Shot());
    }

    //プレイヤーの入力処理
    public void MoveInput()
    {
        _leftInput = Gamepad.current.leftStick.ReadValue();
        _rightInput = Gamepad.current.rightStick.ReadValue();
        _leftTrigger = Gamepad.current.leftTrigger.isPressed;
    }

    //プレイヤーの移動処理
    public void PlayerMove()
    {
        _velocity = new Vector3(_leftInput.x, 0.0f, _leftInput.y);
        _rb.velocity = _velocity.normalized * _speed;
    }

    //プレイヤーの方向転換処理
    public void PlayerRotation()
    {
        _direction = new Vector3(_rightInput.x, 0, _rightInput.y);
        this.transform.localRotation = Quaternion.LookRotation(_direction);
    }

    //プレイヤーのダメージ処理
    public void Damage(int value)
    {
        _audio.PlayOneShot(_clips[1]);
        _hp -= value;
        if (_hp == 0)
        {
            Death();
        }
        Debug.Log(_hp);
    }

    //プレイヤーの死亡処理
    public void Death()
    {
        this.gameObject.SetActive(false);
        Debug.Log("ゲームオーバー");
    }

    //弾を撃つ処理
    public IEnumerator Shot()
    {
        while (true)
        {
            if (Mouse.current.leftButton.isPressed || _leftTrigger)
            {
                //音を鳴らす
                _audio.PlayOneShot(_clips[0]);
                //弾を借りて表示
                Vector3 _pos = this.transform.position;
                Quaternion _rot = this.transform.rotation;
                _objectPool.MyAmmoLaunch(_pos, _rot);
            }
            yield return new WaitForSeconds(_interval);
        }
    }
}
