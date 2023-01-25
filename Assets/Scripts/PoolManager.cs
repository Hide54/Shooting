using System.Collections;
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
    private MyAmoController _myAmo = default;
    [SerializeField, Header("�󂹂�G�̒e�̃v���t�@�u��ݒ�")]
    private EnemyAmoController _enemyAmo1 = default;
    [SerializeField, Header("�󂹂Ȃ��G�̒e�̃v���t�@�u��ݒ�")]
    private EnemyAmoController _enemyAmo2 = default;

    [SerializeField, Header("�e�𐶐����鐔")]
    private int _amoMaxCount = default;

    //���������e���i�[����ꏊ
    private Queue<MyAmoController> _myAmoQueue;
    private Queue<EnemyAmoController> _enemyAmo1Queue;
    private Queue<EnemyAmoController> _enemyAmo2Queue;

    //���񐶐����̈ʒu
    private Vector3 _firstPos = new Vector3(100, 100, 0);
    #endregion

    /* Queue�̏�����
     * �G�̐�����Queue�ւ̒ǉ�
     */
    private void Awake()
    {
        _myAmoQueue = new Queue<MyAmoController>();
        _enemyAmo1Queue = new Queue<EnemyAmoController>();
        _enemyAmo2Queue = new Queue<EnemyAmoController>();

        for (int i = 0; i < _amoMaxCount; i++)
        {
            MyAmoController _tmpMyAmo = Instantiate(_myAmo, _firstPos, Quaternion.identity, transform);
            _myAmoQueue.Enqueue(_tmpMyAmo);
            EnemyAmoController _tmpEnemyAmo1 = Instantiate(_enemyAmo1, _firstPos, Quaternion.identity, transform);
            _enemyAmo1Queue.Enqueue(_tmpEnemyAmo1);
            EnemyAmoController _tmpEnemyAmo2 = Instantiate(_enemyAmo2, _firstPos, Quaternion.identity, transform);
            _enemyAmo2Queue.Enqueue(_tmpEnemyAmo2);
        }
    }

    /* �G�݂̑��o��
     * Queue����G��1���o��
     * �G��\��
     * �n���ꂽ���W�ɓG���ړ�����
     * �Ăяo�����ɓn��
     */
    public MyAmoController Launch(Vector3 _pos)
    {
        if (_myAmoQueue.Count <= 0) return null;

        MyAmoController _tmpMyAmo = _myAmoQueue.Dequeue();
        _tmpMyAmo.gameObject.SetActive(true);
        _tmpMyAmo.ShowInStage(_pos);
        return _tmpMyAmo;
    }

    /* �G�̉��
     * �G���\��
     * Queue�Ɋi�[
     */
    public void MACollect(MyAmoController myAmo)
    {
        myAmo.gameObject.SetActive(false);
        _myAmoQueue.Enqueue(myAmo);
    }
    public void EACollect1(EnemyAmoController enemyAmo)
    {
        enemyAmo.gameObject.SetActive(false);
        _enemyAmo1Queue.Enqueue(enemyAmo);
    }
    public void EACollect2(EnemyAmoController enemyAmo)
    {
        enemyAmo.gameObject.SetActive(false);
        _enemyAmo2Queue.Enqueue(enemyAmo);
    }
}
