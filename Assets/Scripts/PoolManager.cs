using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g�v�[�����Ǘ�����X�N���v�g
/// </summary>
public class PoolManager : MonoBehaviour
{
    /// <summary>
    /// �萔��ϐ�
    /// </summary>
    #region
    [SerializeField, Header("�����̒e�̃v���t�@�u��ݒ�")]
    private MyAmmoController _myAmmo = default;
    [SerializeField, Header("�󂹂�G�̒e�̃v���t�@�u��ݒ�")]
    private EnemyAmmo1 _enemyAmmo1 = default;
    [SerializeField, Header("�󂹂Ȃ��G�̒e�̃v���t�@�u��ݒ�")]
    private EnemyAmmo2 _enemyAmmo2 = default;
    [SerializeField, Header("�Ή�����X�N���v�g��ݒ�")]
    private PlayerController _chara = default;

    [SerializeField, Header("�����̒e�𐶐����鐔")]
    private int _myAmmoMaxCount = default;
    [SerializeField, Header("�G�̒e�𐶐����鐔")]
    private int _enemyAmmoMaxCount = default;

    //���������e���i�[����ꏊ
    private Queue<MyAmmoController> _myAmmoQueue;
    private Queue<EnemyAmmo1> _enemyAmmo1Queue;
    private Queue<EnemyAmmo2> _enemyAmmo2Queue;

    //���񐶐����̈ʒu
    private Vector3 _firstPos = new Vector3(100, 100, 0);
    #endregion

    /* Queue�̏�����
     * �e�̐�����Queue�ւ̒ǉ�
     */
    private void Awake()
    {
        _myAmmoQueue = new Queue<MyAmmoController>();
        _enemyAmmo1Queue = new Queue<EnemyAmmo1>();
        _enemyAmmo2Queue = new Queue<EnemyAmmo2>();

        //�v���C���[�̒e�𐶐�����Queue�ɒǉ�����
        for (int i = 0; i < _myAmmoMaxCount; i++)
        {
            MyAmmoController _tmpMyAmmo = Instantiate(_myAmmo, _firstPos, Quaternion.identity, this.transform);
            MyAmmoEnabledFalse(_tmpMyAmmo);
            _myAmmoQueue.Enqueue(_tmpMyAmmo);
        }

        //�G�̒e�𐶐�����Queue�ɒǉ�����
        for (int i = 0; i < _enemyAmmoMaxCount; i++)
        {
            //�󂹂�e
            EnemyAmmo1 _tmpEnemyAmmo1 = Instantiate(_enemyAmmo1, _firstPos, Quaternion.identity, this.transform);
            EnemyAmmo1EnabledFalse(_tmpEnemyAmmo1);
            _enemyAmmo1Queue.Enqueue(_tmpEnemyAmmo1);

            //�󂹂Ȃ��e
            EnemyAmmo2 _tmpEnemyAmmo2 = Instantiate(_enemyAmmo2, _firstPos, Quaternion.identity, this.transform);
            EnemyAmmo2EnabledFalse(_tmpEnemyAmmo2);
            _enemyAmmo2Queue.Enqueue(_tmpEnemyAmmo2);
        }
    }



    // �v���C���[�̒e�݂̑��o��
    public MyAmmoController MyAmmoLaunch(Vector3 tmpPos, Quaternion tmpRot)
    {
        //Queue�ɉ����Ȃ���΋��Ԃ�
        if (_myAmmoQueue.Count <= 0) return null;

        //Queue����e��1���o��
        MyAmmoController _tmpMyAmmo = _myAmmoQueue.Dequeue();

        //�e��\������
        MyAmmoEnabledTrue(_tmpMyAmmo);

        //�n���ꂽ���W�ɒe���ړ�����
        _tmpMyAmmo.Init(tmpPos, tmpRot);
        return _tmpMyAmmo;
    }

    //�󂹂�G�̒e�݂̑��o��
    public EnemyAmmo1 EnemyAmmoLaunch1(Vector3 tmpPos, Quaternion tmpRot)
    {
        //Queue�ɉ����Ȃ���΋��Ԃ�
        if (_enemyAmmo1Queue.Count <= 0) return null;

        //Queue����e��1���o��
        EnemyAmmo1 _tmpEnemyAmmo1 = _enemyAmmo1Queue.Dequeue();

        //�e��\������
        EnemyAmmo1EnabledTrue(_tmpEnemyAmmo1);

        //�n���ꂽ���W�ɒe���ړ�����
        _tmpEnemyAmmo1.Init(tmpPos, tmpRot);
        return _tmpEnemyAmmo1;
    }

    //�󂹂Ȃ��G�̒e�݂̑��o��
    public EnemyAmmo2 EnemyAmmoLaunch2(Vector3 tmpPos, Quaternion tmpRot)
    {
        //Queue�ɉ����Ȃ���΋��Ԃ�
        if (_enemyAmmo2Queue.Count <= 0) return null;

        //Queue����e��1���o��
        EnemyAmmo2 _tmpEnemyAmmo2 = _enemyAmmo2Queue.Dequeue();

        //�e��\������
        EnemyAmmo2EnabledTrue(_tmpEnemyAmmo2);

        //�n���ꂽ���W�ɒe���ړ�����
        _tmpEnemyAmmo2.Init(tmpPos, tmpRot);
        return _tmpEnemyAmmo2;
    }



    //�v���C���[�̒e�̉������
    public void MACollect(MyAmmoController tmpMyAmmo)
    {
        MyAmmoEnabledFalse(tmpMyAmmo);
        tmpMyAmmo.transform.position = _firstPos;
        _myAmmoQueue.Enqueue(tmpMyAmmo);
    }

    //�󂹂�G�̒e�̉������
    public void EACollect1(EnemyAmmo1 tmpEnemyAmmo1)
    {
        EnemyAmmo1EnabledFalse(tmpEnemyAmmo1);
        tmpEnemyAmmo1.transform.position = _firstPos;
        _enemyAmmo1Queue.Enqueue(tmpEnemyAmmo1);
    }

    //�󂹂Ȃ��G�̒e�̉������
    public void EACollect2(EnemyAmmo2 tmpEnemyAmmo2)
    {
        EnemyAmmo2EnabledFalse(tmpEnemyAmmo2);
        tmpEnemyAmmo2.transform.position = _firstPos;
        _enemyAmmo2Queue.Enqueue(tmpEnemyAmmo2);
    }



    //�v���C���[�̒e��\�����鏈��
    public void MyAmmoEnabledTrue(MyAmmoController myAmmo)
    {
        //���b�V���ƃX�N���v�g�̃R���|�[�l���g���擾
        MeshRenderer _mesh = myAmmo.GetComponent<MeshRenderer>();
        MyAmmoController _ammoController = myAmmo.GetComponent<MyAmmoController>();

        //�擾�����R���|�[�l���g��L����
        _mesh.enabled = true;
        _ammoController.enabled = true;
    }

    //�󂹂�G�̒e��\�����鏈��
    public void EnemyAmmo1EnabledTrue(EnemyAmmo1 enemyAmmo1)
    {
        //���b�V���ƃX�N���v�g�̃R���|�[�l���g���擾
        MeshRenderer _mesh = enemyAmmo1.GetComponent<MeshRenderer>();
        EnemyAmmo1 _ammoController = enemyAmmo1.GetComponent<EnemyAmmo1>();

        //�擾�����R���|�[�l���g��L����
        _mesh.enabled = true;
        _ammoController.enabled = true;
    }

    //�󂹂Ȃ��G�̒e��\�����鏈��
    public void EnemyAmmo2EnabledTrue(EnemyAmmo2 enemyAmmo2)
    {
        //���b�V���ƃX�N���v�g�̃R���|�[�l���g���擾
        MeshRenderer _mesh = enemyAmmo2.GetComponent<MeshRenderer>();
        EnemyAmmo2 _ammoController = enemyAmmo2.GetComponent<EnemyAmmo2>();

        //�擾�����R���|�[�l���g��L����
        _mesh.enabled = true;
        _ammoController.enabled = true;
    }



    //�v���C���[�̒e���\���ɂ��鏈��
    public void MyAmmoEnabledFalse(MyAmmoController myAmmo)
    {
        //���b�V���ƃX�N���v�g�̃R���|�[�l���g���擾
        MeshRenderer _mesh = myAmmo.GetComponent<MeshRenderer>();
        MyAmmoController _ammoController = myAmmo.GetComponent<MyAmmoController>();

        //�擾�����R���|�[�l���g�𖳌���
        _mesh.enabled = false;
        _ammoController.enabled = false;
    }

    //�󂹂�G�̒e���\���ɂ��鏈��
    public void EnemyAmmo1EnabledFalse(EnemyAmmo1 enemyAmmo1)
    {
        //���b�V���ƃX�N���v�g�̃R���|�[�l���g���擾
        MeshRenderer _mesh = enemyAmmo1.GetComponent<MeshRenderer>();
        EnemyAmmo1 _ammoController = enemyAmmo1.GetComponent<EnemyAmmo1>();

        //�擾�����R���|�[�l���g�𖳌���
        _mesh.enabled = false;
        _ammoController.enabled = false;
    }

    //�󂹂Ȃ��G�̒e���\���ɂ��鏈��
    public void EnemyAmmo2EnabledFalse(EnemyAmmo2 enemyAmmo2)
    {
        //���b�V���ƃX�N���v�g�̃R���|�[�l���g���擾
        MeshRenderer _mesh = enemyAmmo2.GetComponent<MeshRenderer>();
        EnemyAmmo2 _ammoController = enemyAmmo2.GetComponent<EnemyAmmo2>();

        //�擾�����R���|�[�l���g�𖳌���
        _mesh.enabled = false;
        _ammoController.enabled = false;
    }
}
