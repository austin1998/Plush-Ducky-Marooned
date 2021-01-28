using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoSingleton<TooltipManager>
{
    public enum TooltipTypes { Enemy , firstMap, blunderbuss, ammoChest, coinsChest, harpoonGun, cannon, buyStation};

    private Dictionary<TooltipTypes, Tooltip> tooltipsToTypes = new Dictionary<TooltipTypes, Tooltip>();

    private void Start()
    {

        Tooltip[] tooltips = Resources.FindObjectsOfTypeAll<Tooltip>();
        foreach(Tooltip tooltip in tooltips)
        {
            if(!tooltipsToTypes.ContainsKey(tooltip.tooltipType))
            {
                tooltipsToTypes.Add(tooltip.tooltipType, tooltip);
            }
        }
    }

    public void LoadTooltip(TooltipTypes tooltipType)
    {
        tooltipsToTypes[tooltipType].gameObject.SetActive(true);
        PauseMenu.Instance.PauseGame();
    }
}
