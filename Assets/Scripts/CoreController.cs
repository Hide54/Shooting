using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    #region
    [SerializeField,Header("�I�u�W�F�N�g�v�[���̊Ǘ��X�N���v�g��ݒ�")]
    private PoolManager _objectPool = default;
    [SerializeField, Header("�{�X�̗̑�")]
    private int _coreHp = default;
    [SerializeField, Header("�{�X�̈ړ����x")]
    private float _coreSpeed = default;
    [SerializeField, Header("�{�X�̒e�����Ԋu")]
    private float _interval = default;

    private Transform _enemyTransform = default;
    private float _damageArea = default;
    #endregion

    private void Awake()
    {
        StartCoroutine(Shoot());
    }

    //�_���[�W����
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
