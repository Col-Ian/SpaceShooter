using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Singleton, to access use 'PlayerStats_UI.instance'
public class PlayerStats_UI : MonoBehaviour
{
   public static PlayerStats_UI instance {get; private set;} = null; //initialized during our Awake()

    [SerializeField] Image _playerHealth_fg;
    [SerializeField] TextMeshProUGUI _playerPoints_txt;

    public void DisplayPlayerHealth(float playerHealth01){
        // Make sure value is between 0-1
        playerHealth01 = Mathf.Clamp01(playerHealth01);
        _playerHealth_fg.fillAmount = playerHealth01;
    }

    public void DisplayPlayerPoints(int points){
        _playerPoints_txt.text = points.ToString();
    }

   // If there is already an instance variable populated, this may be a duplicate, so destroy.
   void Awake(){
    if(instance != null){
        DestroyImmediate(this);
        return;
    }
    // Otherwise, give permission to exist
    instance = this;
   }
}
