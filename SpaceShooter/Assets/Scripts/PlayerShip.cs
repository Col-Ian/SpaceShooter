using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton
public class PlayerShip : MonoBehaviour
{
    public static PlayerShip instance {get; private set;} = null;
    [SerializeField] int _health = 3;
    [SerializeField] int _points = 0;
    [SerializeField] Camera _myCamera;
    [SerializeField] float _fireDelay = 0.5f;
    [SerializeField] Bullet _myBulletPREFAB = null; // will replace it in scene
    [SerializeField] GameObject _explosion_sprite_PREFAB = null;

    // So fire immediately as game begins
    float _prevShotTime = -999;

    bool _isDead = false;

    public void AddScore(int num){
        _points+=num;
    }
    void Update(){
        PlayerStats_UI.instance.DisplayPlayerHealth((_health/3.0f));
        PlayerStats_UI.instance.DisplayPlayerPoints(_points);
        if(_isDead){return;}
        MoveShip();
        FireBullets();
    }

    void MoveShip(){
        // Hint that it should not run a portion of code if on a computer vs phone
#if UNITY_EDITOR || UNITY_STANDALONE
        // If no mouse button down, return
        if(Input.GetMouseButton(0)==false){return;}
        Vector2 pixelPos = Input.mousePosition;
#else        
        if(Input.touchCount == 0){return;}
        // Returns a touch object
        // Position will follow the touch
        Vector2 pixelPos = Input.GetTouch(0).position;
#endif
        // Convert from pixels to world coordinates
        // ScreenToWorldPoint() takes a pixel position and then outputs a Vector3 in the world coordinates
        Vector3 worldCoord = _myCamera.ScreenToWorldPoint(pixelPos);
        transform.position = new Vector3(worldCoord.x, worldCoord.y, transform.position.z);
    }

    
    void FireBullets(){
        float elapsed = Time.time - _prevShotTime;
        if(elapsed < 0.5f){return;} // Don't shoot until 0.5 has elapsed since last shot
        _prevShotTime = Time.time;
        // recreate it in the world
        Bullet b = GameObject.Instantiate(_myBulletPREFAB);
        b.GetComponent<Rigidbody2D>().position = transform.position; // use GetComponent<Rigidbody2D>() instead of transform since the object has a rigid body 2d component
        b.SetFlyDirection(Vector2.up);
    }
    void Awake(){
    if(instance != null){
        DestroyImmediate(this.gameObject);
        return;
    }
    instance = this;
   }

   private void OnTriggerEnter2D(Collider2D collision){
        var b = collision.GetComponent<Bullet>();
        if(b == null){return;}

        if(b.isPlayerBullet){return;}
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
        this.gameObject.SetActive(false); // Conceal the object instead of Destroy so that they other things don't go wonky
        _isDead = true;
    }

}
