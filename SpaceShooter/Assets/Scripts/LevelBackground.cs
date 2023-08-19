using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBackground : MonoBehaviour
{
    // Variable for holding our backgrounds/foregrounds.
    [SerializeField] List<SpriteRenderer> _backgrounds;
    [SerializeField] List<SpriteRenderer> _bg_joints; // Hide seams between backgrounds. It could be added as a child of the background element, but we are doing it through code.
    [SerializeField] List<SpriteRenderer> _foregrounds;
    // Making the speeds variables helps us to have more control. You can even do temporary faster speeds in these to simulate a boost.
    [SerializeField] float _bg_speed = 0.8f;
    [SerializeField] float _fg_speed = 1.5f;
    [Space(10)]
    [SerializeField] float _thresholdHeight = 15;
    [SerializeField] float _thresholdLoop= -15;

    // Update is called once per frame
    void Update()
    {
       MoveBackgrounds(_backgrounds,_bg_speed);
       MoveBackgrounds(_bg_joints,_bg_speed);
       MoveBackgrounds(_foregrounds,_fg_speed);
       LoopBackgroundsAround(_backgrounds);
       LoopBackgroundsAround(_bg_joints);
       LoopBackgroundsAround(_foregrounds);
    }

    // Make it it's own function so we don't have multiple for loops for each movement to be less clunky
    void MoveBackgrounds(List<SpriteRenderer> tiles, float speed){
         // look through backgrounds and push down
        for(int i=0; i<tiles.Count; ++i){
            // Time.deltaTime to make it framerate independant
            // _speed to give us some more slight control
            tiles[i].transform.position -= Vector3.up*Time.deltaTime*speed;
        }
    }

    // When the tile reaches the -15 position, it will loop back to the top to allow continuous parallax
    void LoopBackgroundsAround(List<SpriteRenderer> tiles){
        for(int i=0; i<tiles.Count; ++i){
            Transform t = tiles[i].transform;
            if(t.position.y > _thresholdLoop){
                continue;
                } else {
                    // x and z stay the same
                    t.position = new Vector3(t.position.x, _thresholdHeight, t.position.z);
                }
        }
    }
}
