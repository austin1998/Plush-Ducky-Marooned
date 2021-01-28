using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

    private int i = 0;

    // Start is called before the first frame update
    void Start() {
        Debug.Log("I have been instantiated");
    }

    // Update is called once per frame
    void Update() {
        PrintUpdate();
        i++;
    }

    private void PrintUpdate() {
        Debug.Log("Update on frame " + i);
    }

}
