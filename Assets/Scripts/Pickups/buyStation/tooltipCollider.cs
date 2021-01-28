using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tooltipCollider : MonoBehaviour
{
    static bool hasTouched;
    // Start is called before the first frame update
    void Start()
    {
        hasTouched = false;
    }
    void OnTriggerEnter(Collider col)
    {
        if (hasTouched == false && col.tag == "Player")
        {
            Debug.Log("touch tooltip collider");
            FindObjectOfType<TooltipManager>().LoadTooltip(TooltipManager.TooltipTypes.buyStation);
            hasTouched = true;
        }


    }
}
