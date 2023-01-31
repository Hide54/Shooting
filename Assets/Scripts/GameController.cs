using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField, Header("�J��������X�N���v�g���t�����I�u�W�F�N�g��ݒ�")]
    private CameraController _cameraController = default;
    [SerializeField, Header("�v���C���[����X�N���v�g���t�����I�u�W�F�N�g��ݒ�")]
    private PlayerController _chara = default;
    [SerializeField, Header("�R�A����X�N���v�g���t�����I�u�W�F�N�g��ݒ�")]
    private CoreController _boss = default;
    [SerializeField, Header("�I�[�f�B�I�\�[�X��ݒ�")]
    private AudioSource _audio = default;
    [SerializeField, Header("�Q�[���N���A�A�I�[�o�[���ɖ炷SE��ݒ�")]
    private AudioClip[] _clips = default;
    [SerializeField, Header("�Q�[���N���A����UI")]
    private GameObject _clearUI = default;
    [SerializeField,Header("�Q�[���N���A����UI")]
    private GameObject _deathUI = default;

    [Header("�[�[�[�[�[������Ȃ��[�[�[�[�[")]
    [SerializeField, Header("�v���C���[��ݒ�")]
    private GameObject _player = default;
    [SerializeField, Header("�{�X��ݒ�")]
    private GameObject _core = default;

    private float _endTime = 2f;


    private void Awake()
    {
        Time.timeScale = 1;
        _clearUI.SetActive(false);
        _deathUI.SetActive(false);
        _player = GameObject.FindGameObjectWithTag("Player");
        _core = GameObject.FindGameObjectWithTag("Core");
    }

    private void Update()
    {
        _chara.MoveInput();
        if (_player.activeSelf == false)
        {
            _audio.PlayOneShot(_clips[1]);
            _deathUI.SetActive(true);
            StartCoroutine(Death());
        }
        if (_core.activeSelf == false)
        {
            _audio.PlayOneShot(_clips[0]);
            _clearUI.SetActive(true);
            StartCoroutine(Clear());
        }
    }

    private void FixedUpdate()
    {
        //�v���C���[�̈ړ�
        _chara.PlayerMove();
        //�v���C���[�̌����̕ύX
        _chara.PlayerRotation();

        //�J�������v���C���[�ɒǏ]������
        _cameraController.CameraFollow();

        //�{�X�̈ړ�
        _boss.Move(_player);
    }

    public IEnumerator Clear()
    {
        yield return null;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(_endTime);
        SceneManager.LoadSceneAsync("Title");
    }

    public IEnumerator Death()
    {
        yield return null;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(_endTime);
        SceneManager.LoadSceneAsync("Title");
    }
}
