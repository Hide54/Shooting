using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Header("�Ǐ]���������I�u�W�F�N�g")]
    private GameObject _followTarget = default;

    private Vector3 offset;

    private void Awake()
    {
        offset = gameObject.transform.position - _followTarget.transform.position;
    }


}
