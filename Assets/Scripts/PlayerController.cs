using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, Damageable
{
    #region
    [SerializeField, Header("�I�u�W�F�N�g�v�[���̊Ǘ��X�N���v�g��ݒ�")]
    private PoolManager _objectPool = default;
    [SerializeField, Header("�Ή�����X�N���v�g��ݒ�")]
    private InputActions _input = default;
    [SerializeField, Header("�I�[�f�B�I�\�[�X��ݒ�")]
    private AudioSource _audio = default;
    [SerializeField, Header("�v���C���[���炷SE��ݒ�")]
    private AudioClip[] _clips = default;

    [SerializeField, Header("�v���C���[�̈ړ����x��ݒ�")]
    private float _speed = default;
    [SerializeField, Header("�v���C���[�̗̑͂�ݒ�")]
    private float _hp = default;

    [SerializeField, Header("�e�̔��ˊԊu��ݒ�")]
    private float _interval = default;
    [Header("�e�̑��x��ݒ�")]
    public float _ammoSpeed = default;

    //�R���g���[���[�̍��X�e�B�b�N�̓��͒l
    private Vector3 _leftInput = default;
    //�R���g���[���[�̉E�X�e�B�b�N�̓��͒l
    private Vector3 _rightInput = default;
    //L�g���K�[�̓��͒l
    private bool _leftTrigger = default;

    private float _horizontal = 0;
    private float _vertical = 0;

    //���g��Rigidbody
    private Rigidbody _rb = default;
    //�ړ����x
    private Vector3 _velocity = default;
    //�v���C���[�̌���
    private Vector3 _direction = default;

    #endregion

    private void Awake()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
        StartCoroutine(Shot());
    }

    //�v���C���[�̓��͏���
    public void MoveInput()
    {
        _leftInput = Gamepad.current.leftStick.ReadValue();
        _rightInput = Gamepad.current.rightStick.ReadValue();
        _leftTrigger = Gamepad.current.leftTrigger.isPressed;
    }

    //�v���C���[�̈ړ�����
    public void PlayerMove()
    {
        //�e�X�g�p�L�[����
        #region
        if (Keyboard.current.dKey.isPressed)
        {
            _horizontal = 1;
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            _horizontal = -1;
        }
        else
        {
            _horizontal = 0;
        }
        if (Keyboard.current.wKey.isPressed)
        {
            _vertical = 1;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            _vertical = -1;
        }
        else
        {
            _vertical = 0;
        }
        #endregion

        //_velocity = new Vector3(_leftInput.x, 0.0f, _leftInput.y);
        _velocity = new Vector3(_horizontal, 0.0f, _vertical);
        _rb.velocity = _velocity.normalized * _speed;
    }

    //�v���C���[�̕����]������
    public void PlayerRotation()
    {
        _direction = new Vector3(_rightInput.x, 0, _rightInput.y);
        this.transform.localRotation = Quaternion.LookRotation(_direction);
    }

    //�v���C���[�̃_���[�W����
    public void Damage(int value)
    {
        _audio.PlayOneShot(_clips[1]);
        _hp -= value;
    }

    //�v���C���[�̎��S����
    public void Death()
    {
        Debug.Log("�Q�[���I�[�o�[");
    }

    //�e��������
    public IEnumerator Shot()
    {
        while (true)
        {
            if (Mouse.current.leftButton.isPressed || _leftTrigger)
            {
                _audio.PlayOneShot(_clips[0]);
                Vector3 _pos = this.transform.position;
                Quaternion _rot = this.transform.rotation;
                _objectPool.MyAmmoLaunch(_pos, _rot);
            }
            yield return new WaitForSeconds(_interval);
        }
    }
}

public interface Damageable
{
    public void Damage(int value);
    public void Death();
}
