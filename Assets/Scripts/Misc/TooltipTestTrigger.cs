using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipTestTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TooltipManager.Instance.LoadTooltip(TooltipManager.TooltipTypes.Enemy);
    }
}
