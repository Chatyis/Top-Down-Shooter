using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    //public void SetUI(string uiType)
    //{
    //    switch (uiType)
    //    {
    //        case "health":
    //            {
    //                GameObject.Find("HealthDisplay").GetComponent<Text>().text = "Health: "+ GameObject.Find("Player").GetComponent<PlayerController>().health;
    //                break;
    //            }
    //        case "points":
    //            {
    //                GameObject.Find("ScoreDisplay").GetComponent<Text>().text = "Score: " + 0;
    //                break;
    //            }
    //        case "ammo":
    //            {
    //                GameObject.Find("AmmoDisplay").GetComponent<Text>().text = "Ammo: " + GameObject.Find("Player").GetComponent<PlayerController>().CurrentWeapon.curr_ammo + "/" + GameObject.Find("Player").GetComponent<PlayerController>().CurrentWeapon.ammo;
    //                break;
    //            }
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        GameObject.Find("HealthDisplay").GetComponent<Text>().text = "Health: " + GameObject.Find("Player").GetComponent<PlayerController>().health;
        GameObject.Find("ScoreDisplay").GetComponent<Text>().text = "Score: " + GameObject.Find("Player").GetComponent<PlayerController>().score;
        GameObject.Find("AmmoDisplay").GetComponent<Text>().text = "Ammo: " + GameObject.Find("Player").GetComponent<PlayerController>().CurrentWeapon.curr_ammo + "/" + GameObject.Find("Player").GetComponent<PlayerController>().CurrentWeapon.ammo;
    }
}
