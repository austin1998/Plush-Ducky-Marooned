using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyStationCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player") {
            Debug.Log("touch buy menu");
            BuyMenu.Instance.OpenMainBuyMenu();
        }
        

    }
}
