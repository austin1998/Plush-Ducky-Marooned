using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

public class TreasureHuntMain : MonoBehaviour
{
    static bool activeChest; // Does the player have a map already?
    static int numberOfCompleted;
    static GameObject currentChest;

    static bool firstMap = false;
    static bool firstCoins = false;
    static bool firstAmmo = false;

    [SerializeField] int frequency; // How often to get a map 1/n
    [SerializeField] int ammoFrequency; // How often to get a ammo chest (vs coins) 1/n
    [SerializeField] int chestsToGetPW1;
    [SerializeField] int chestsToGetPW2;
    [SerializeField] int chestsToGetPW3;

    [SerializeField] AudioSource mapSound;
    [SerializeField] AudioClip mapClip;
    [SerializeField] AudioClip foundClip;
    [SerializeField] AudioClip ammoFound;
    [SerializeField] AudioClip bluderbussUnlocked;
    [SerializeField] AudioClip speargunUnlocked;
    [SerializeField] AudioClip cannonUnlocked;

    GameObject[] spawners;
    // Start is called before the first frame update
    void Start()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        activeChest = false;
        numberOfCompleted = 0;
        currentChest = null;
        spawners = GameObject.FindGameObjectsWithTag("chestSpawner");
        spawners[0].GetComponent<chestSpawner>().setSounds(speargunUnlocked, bluderbussUnlocked, cannonUnlocked, foundClip, ammoFound);
        spawners[0].GetComponent<chestSpawner>().setAudioSource(mapSound);
            for (int i = 0; i < spawners.Length; i++)
        {
            //Spawners cannot be deactivated in their start() functions. 
            spawners[i].GetComponent<chestSpawner>().setState(0);
        }
    }
    //call at start of new round
    public void roll()
    {
        
        //GameObject.GetComponent<TooltipManager>().LoadTooltip(GameObject.GetComponent<TooltipManager>().TooltipTypes.Enemy);
        if (activeChest)
        { //Don't run 2 treasure hunts concurrently
            return;
        }
        //Roll for chest
        //ints are not maximally inclusive; add 1
        int freq = Random.Range(0, frequency + 1);
        if (freq == 1 && frequency != 1)
            return;
        if (firstMap == false) {
            StartCoroutine(delayedMapTooltip());
            firstMap = true;
        }
        if (mapSound != null)
        {
            mapSound.PlayOneShot(mapClip, 2f);
        }
        activeChest = true;
        //Make the chest
        if (numberOfCompleted == chestsToGetPW1 || numberOfCompleted == chestsToGetPW2 || numberOfCompleted == chestsToGetPW3)
        {
            GameObject chestSpawners = GameObject.Find("ChestSpawners");
            if (numberOfCompleted == chestsToGetPW1)
            {
                chestSpawners.transform.Find("speargunSpawner").GetComponent<chestSpawner>().setState(1);
                currentChest = chestSpawners.transform.Find("speargunSpawner").gameObject;
                Debug.Log("You got a power weapon 1 map\n");
            }
            else if (numberOfCompleted == chestsToGetPW2)
            {
                chestSpawners.transform.Find("blunderbussSpawner").GetComponent<chestSpawner>().setState(2);
                currentChest = chestSpawners.transform.Find("blunderbussSpawner").gameObject;
                Debug.Log("You got a power weapon 2 map\n");
            }
            else if (numberOfCompleted == chestsToGetPW3)
            {
                chestSpawners.transform.Find("handCannonSpawner").GetComponent<chestSpawner>().setState(5);
                currentChest = chestSpawners.transform.Find("handCannonSpawner").gameObject;
                Debug.Log("You got a power weapon 3  map\n");
            }
        }
        else
        {
            int type = (Random.Range(0, ammoFrequency + 1) == 1 ? 4 : 3); 
            //ints are not maximally inclusive
            int no = Random.Range(0, spawners.Length);
            currentChest = spawners[no]; //grab a random chest spawner
            currentChest.GetComponent<chestSpawner>().setState(type);
            Debug.Log("You got a points or ammo map\n");
        }
        //select a chest to show
        //show notification on screen
    }
    public bool isSurfaceChest() {
        if (currentChest.name.Contains("cave"))
            return false;
        return true;
    }
    public void modifyXMarks(bool showOrHide) {
        Transform x = GameObject.Find("xMarks").transform;
        foreach (Transform child in x)
        {
            child.gameObject.SetActive(showOrHide);
        }
    }
    public void modifyChestIcon(bool showOrHide) {
        currentChest.transform.Find("mapIcon").gameObject.SetActive(showOrHide);
    }
    public bool IsActive() {
        if (currentChest == null)
            return false;
        return true;
    }
    public void chestFound(int chestType)
    {
        if (firstAmmo == false && chestType == 4)
        {
            FindObjectOfType<TooltipManager>().LoadTooltip(TooltipManager.TooltipTypes.ammoChest);
            firstAmmo = true;
        }
        if (firstCoins == false && chestType == 3)
        {
            FindObjectOfType<TooltipManager>().LoadTooltip(TooltipManager.TooltipTypes.coinsChest);
            firstCoins = true;
        }
        else if (chestType == 5)
        {
            FindObjectOfType<TooltipManager>().LoadTooltip(TooltipManager.TooltipTypes.cannon);
        }
        else if (chestType == 2)
        {
            FindObjectOfType<TooltipManager>().LoadTooltip(TooltipManager.TooltipTypes.blunderbuss);
        }
        else if (chestType == 1) {
            FindObjectOfType<TooltipManager>().LoadTooltip(TooltipManager.TooltipTypes.harpoonGun);
        }

        numberOfCompleted++;
        currentChest = null;
        activeChest = false;
    }
    IEnumerator delayedMapTooltip() {
        yield return new WaitForSeconds(2);
        FindObjectOfType<TooltipManager>().LoadTooltip(TooltipManager.TooltipTypes.firstMap);
    }
}