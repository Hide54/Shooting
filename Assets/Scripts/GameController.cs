using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField, Header("�J��������X�N���v�g���t�����I�u�W�F�N�g��ݒ�")]
    private CameraController _cameraController = default;

    private void FixedUpdate()
    {
        //�J�������v���C���[�ɒǏ]������
        _cameraController.gameObject.transform.position = Vector3.Lerp(
            _cameraController.transform.position, 
            _cameraController._followTarget.transform.position + _cameraController._offset, 
            Time.deltaTime * _cameraController._smoothTime);
    }
}
