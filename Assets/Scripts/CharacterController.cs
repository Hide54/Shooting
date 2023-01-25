using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField, Header("�I�u�W�F�N�g�v�[���̊Ǘ��X�N���v�g��ݒ�")]
    private PoolManager _objectPool = default;

    [SerializeField, Header("���˂̊Ԋu��ݒ�")]
    private float _interval = default;

    private Plane _plane = new Plane();
    private float _distance = default;

    private void Awake()
    {

    }

    /*
     * �U���{�^���������Ă�Ԓe������
     */
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if(_plane.Raycast(ray,out _distance))
        {
            Vector3 lookPoint = ray.GetPoint(_distance);
            transform.LookAt(lookPoint);
        }

        if (Input.GetButton("Fire1"))
        {
            StartCoroutine(Shot());
        }
    }

    //�e������
    private IEnumerator Shot()
    {
        _objectPool.Launch(transform.position);
        yield return new WaitForSeconds(_interval);
    }
}
