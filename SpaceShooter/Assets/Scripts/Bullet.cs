using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 3;
    [SerializeField] bool _isPlayerBullet;
    public bool isPlayerBullet => _isPlayerBullet;

    // Set a birth time to destroy a projectile if it has been flying around for too long.
    float _birthTime = 0;

    public void EliminateSelf(){
        Destroy(this.gameObject);
    }
    private void Start(){
        _birthTime = Time.time;
    }
    public void SetFlyDirection(Vector2 dir){
        this.GetComponent<Rigidbody2D>().velocity = dir*_speed;
    }
    void Update(){
        float existedForTime = Time.time - _birthTime;
        if(existedForTime <5){return;}
        Destroy(this.gameObject);
    }
}
