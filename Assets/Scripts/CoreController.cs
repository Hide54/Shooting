using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreController : MonoBehaviour, Damageable
{
    #region
    [SerializeField, Header("�I�u�W�F�N�g�v�[���̊Ǘ��X�N���v�g��ݒ�")]
    private PoolManager _objectPool = default;
    [SerializeField, Header("�I�[�f�B�I�\�[�X��ݒ�")]
    private AudioSource _audio = default;
    [SerializeField, Header("�G���炷SE��ݒ�")]
    private AudioClip[] _clips = default;
    [SerializeField, Header("�{�X�̗̑�")]
    private int _coreHp = default;
    [SerializeField, Header("�{�X�̈ړ����x")]
    private float _coreSpeed = default;
    [SerializeField, Header("�{�X�̒e�����Ԋu")]
    private float _interval = default;
    [SerializeField,Header("�{�X�����ʂ܂ł̎���")]
    private float _waitTime = default;
    [SerializeField,Header("�e�̔��ˊp�x")]
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

    //�R�A�̃_���[�W����
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

    //�R�A�̎��S����
    public void Death()
    {
        StartCoroutine(Die());
        Debug.Log("�Q�[���N���A");
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
