using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Header("追従させたいオブジェクト")]
    private GameObject _followTarget = default;

    private Vector3 offset;

    private void Awake()
    {
        offset = gameObject.transform.position - _followTarget.transform.position;
    }


}
