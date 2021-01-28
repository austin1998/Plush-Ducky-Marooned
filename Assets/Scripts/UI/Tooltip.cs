using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{

    public TooltipManager.TooltipTypes tooltipType;

    void Update() {
        if (Input.GetKeyDown("space"))
            CloseTooltip();
    }
    public void CloseTooltip()
    {
        gameObject.SetActive(false);
        PauseMenu.Instance.ResumeGame();
    }

}
