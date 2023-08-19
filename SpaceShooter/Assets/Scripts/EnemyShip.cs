using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] int _health = 3;
    [SerializeField] GameObject _explosion_sprite_PREFAB = null;
    [SerializeField] Bullet _myBulletPREFAB = null;

    private void OnTriggerEnter2D(Collider2D collision){
        var b = collision.GetComponent<Bullet>();
        if(b == null){return;}

        if(b.isPlayerBullet == false){return;}
        _health--;
        if(_health >0){
            b.EliminateSelf();
            return;
            }
        PlayerShip.instance.AddScore(1);
        StartCoroutine(ExplodeSelf());
        
    }

    IEnumerator ExplodeSelf(){
        if(_explosion_sprite_PREFAB != null){
            var exp = GameObject.Instantiate(_explosion_sprite_PREFAB);
            exp.transform.position = transform.position;
            yield return new WaitForSeconds(0.7f);
        } 
        // Destroy(this) will get rid of the script, but the invisible GameObject will hang in the space
        // We won't use Destroy(immediate) so it will finish at the end of the frame to be more smooth
        Destroy(this.gameObject);
    }

    void Update(){
        FireBullets();
    }

    float _prevShotTime = -999;
    void FireBullets(){
        float elapsed = Time.time - _prevShotTime;
        if(elapsed < 1.0f){return;}
        _prevShotTime = Time.time;
        Bullet b = GameObject.Instantiate(_myBulletPREFAB);
        b.GetComponent<Rigidbody2D>().position = transform.position;
        b.SetFlyDirection(Vector2.down);
    }
}
