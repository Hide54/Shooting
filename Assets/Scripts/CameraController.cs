using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("追従させたいオブジェクト")]
    public GameObject _followTarget = default;
    [Header("追従時の遅れる時間")]
    public float _smoothTime = default;
    [HideInInspector]
    public Vector3 _offset = default;

    private void Awake()
    {
        _offset = gameObject.transform.position - _followTarget.transform.position;
    }

    public void CameraFollow()
    {
        gameObject.transform.position = Vector3.Lerp(
            transform.position,
            _followTarget.transform.position + _offset,
            Time.deltaTime * _smoothTime);
    }
}
