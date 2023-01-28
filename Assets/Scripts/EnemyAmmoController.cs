using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmmoController : MonoBehaviour
{
    [SerializeField, Header("’e‚ÌˆÚ“®‘¬“x")]
    private float _speed = default;

    [HideInInspector]
    public PoolManager _objectPool = default;

    private void Awake()
    {
        _objectPool = this.transform.parent.GetComponent<PoolManager>();
    }

    private void FixedUpdate()
    {
        this.transform.position += this.transform.forward * _speed * Time.deltaTime;
    }
}
