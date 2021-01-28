using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapManager : MonoBehaviour
{
    GameObject caveCam;
    GameObject surfaceCam;

    // BRENDEN CHANGES
    [SerializeField] Camera overHeadCameraSurface;
    [SerializeField] Camera overHeadCameraCave;
    [SerializeField] GameObject surfaceBuyStations;
    [SerializeField] GameObject caveBuyStations;

    private Camera overHeadCamera;
    // END

    GameObject bigMap;
    bool isBig;
    public static bool isInCave;
    // Start is called before the first frame update
    void Start()
    {
        isBig = false;
        isInCave = false;
        bigMap = GameObject.Find("bigMap");
        bigMap.SetActive(false);
        caveCam = GameObject.Find("CaveMinimapCamera");
        surfaceCam = GameObject.Find("IslandMinimapCamera");


        // BRENDEN CHANGES
        overHeadCameraSurface.gameObject.SetActive(false);
        overHeadCameraCave.gameObject.SetActive(false);

        overHeadCamera = overHeadCameraSurface;
        // END

        setToSurface();
    }
    void Update()
    {
        if (Input.GetButton("bigMap"))
        {
            if (!isBig)
            {
                overHeadCamera.gameObject.SetActive(true);
                bigMap.SetActive(true);
            }
            isBig = true;
            return;
        }
        else if (isBig)
        {
            overHeadCamera.gameObject.SetActive(false);
            bigMap.SetActive(false);
            isBig = false;
        }
    }
    //GameObject.Find("/Managers/MinimapManager").GetComponent<minimapManager>().setToCave();
    public void setToSurface()
    {

        isInCave = false;

        //handle buyStations
        surfaceBuyStations.gameObject.SetActive(true);
        caveBuyStations.gameObject.SetActive(false);

        //Set minimap to surface
        surfaceCam.SetActive(true);
        caveCam.SetActive(false);

        //Set overHeadCamera to surface
        overHeadCamera = overHeadCameraSurface;
        overHeadCameraCave.gameObject.SetActive(false);
        overHeadCamera.gameObject.SetActive(true);

        TreasureHuntMain hunt = FindObjectOfType<TreasureHuntMain>();
        if (!hunt.IsActive())
            return;
        if (hunt.isSurfaceChest())
        {
            hunt.modifyChestIcon(true);
            hunt.modifyXMarks(false);
        }
        else
        {
            hunt.modifyChestIcon(false);
            hunt.modifyXMarks(true);
        }

    }
    public void setToCave()
    {
        isInCave = true;

        //handle buyStations
        surfaceBuyStations.gameObject.SetActive(false);
        caveBuyStations.gameObject.SetActive(true);

        //Set minimap to cave
        surfaceCam.SetActive(false);
        caveCam.SetActive(true);

        //Set overheadCamera to cave
        overHeadCamera = overHeadCameraCave;
        overHeadCameraSurface.gameObject.SetActive(false);
        overHeadCamera.gameObject.SetActive(true);

        TreasureHuntMain hunt = FindObjectOfType<TreasureHuntMain>();
        if (!hunt.IsActive())
            return;
        if (hunt.isSurfaceChest())
        {
            hunt.modifyChestIcon(false);
            hunt.modifyXMarks(true);
        }
        else
        {
            hunt.modifyXMarks(false);
            hunt.modifyChestIcon(true);
        }



    }
    public bool inCave()
    {
        return isInCave;
    }
}