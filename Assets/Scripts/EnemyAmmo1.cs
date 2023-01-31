using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmmo1 : EnemyAmmoController
{
    // 弾の位置と向き初期化処理
    public void Init(Vector3 position, Quaternion rotation)
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
    }

    //オブジェクトに当たった処理
    public void OnTriggerEnter(Collider obj)
    {
        Damageable _damageable = obj.gameObject.GetComponent<Damageable>();

        //壁かプレイヤーの弾に当たったら何もせずに自分を回収
        if (obj.CompareTag("Wall") || obj.CompareTag("MAmo"))
        {
            HideFromStage();
        }
        //プレイヤーに当たったらプレイヤーの体力を1減らして自分を回収
        else if (obj.CompareTag("Player"))
        {
            _damageable.Damage(_enemyDamage);
            HideFromStage();
        }
    }

    //自身を回収
    public void HideFromStage()
    {
        _objectPool.EACollect1(this);
    }
}
