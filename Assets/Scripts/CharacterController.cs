using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField, Header("オブジェクトプールの管理スクリプトを設定")]
    private PoolManager _objectPool = default;

    [SerializeField, Header("発射の間隔を設定")]
    private float _interval = default;

    private Plane _plane = new Plane();
    private float _distance = default;

    private void Awake()
    {

    }

    /*
     * 攻撃ボタンを押してる間弾を撃つ
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

    //弾を撃つ
    private IEnumerator Shot()
    {
        _objectPool.Launch(transform.position);
        yield return new WaitForSeconds(_interval);
    }
}
