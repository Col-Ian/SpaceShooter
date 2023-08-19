using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Will stop explosion from infinitely spawning

    [SerializeField] float _existTime = 0.7f;
    float _birthTime = 0;

    void Start(){
        _birthTime = Time.time;
    }

    void Update(){
        if(Time.time - _birthTime < _existTime){return;}
        Destroy(this.gameObject);
    }
}
