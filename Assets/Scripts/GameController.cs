using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField, Header("�J��������X�N���v�g���t�����I�u�W�F�N�g��ݒ�")]
    private CameraController _cameraController = default;
    [SerializeField,Header("�v���C���[����X�N���v�g���t�����I�u�W�F�N�g��ݒ�")]
    private PlayerController _chara = default;

    //private void Update()
    //{
    //    _chara.MoveInput();
    //}

    private void FixedUpdate()
    {
        //�v���C���[�̈ړ�
        _chara.PlayerMove();
        ////�v���C���[�̌����̕ύX
        //_chara.PlayerRotation();

        //�J�������v���C���[�ɒǏ]������
        _cameraController.CameraFollow();
    }
}
