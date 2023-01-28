using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����̒e�̊Ǘ��X�N���v�g
/// </summary>
public class MyAmmoController : MonoBehaviour
{
    [SerializeField, Header("�e�̑��x")]
    private float _speed = default;
    [SerializeField, Header("BoxCast��x���W�̔��a")]
    private float _rayX = default;
    [SerializeField, Header("BoxCast��y���W�̔��a")]
    private float _rayY = default;
    [SerializeField, Header("BoxCast��z���W�̔��a")]
    private float _rayZ = default;

    private PoolManager _objectPool = default;


    private void Awake()
    {
        _objectPool = this.transform.parent.GetComponent<PoolManager>();
    }

    private void FixedUpdate()
    {
        this.transform.position += this.transform.forward * _speed * Time.deltaTime;

        //ray�����������玩�������
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

    // �e�̈ʒu�ƌ�������������
    public void Init(Vector3 position, Quaternion rotation)
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
    }

    //BoxCast���^���I�ɕ\�����鏈��
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(_rayX * 2, _rayY * 2, _rayZ * 2));
    }

    //���g�����
    public void HideFromStage()
    {
        _objectPool.MACollect(this);
    }
}
