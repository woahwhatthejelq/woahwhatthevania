using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;

public class SaveStibe : MonoBehaviour {

    private GameObject notActive;
    private GameObject active;
    private TextMeshProUGUI interaction;

    void Start() {
        notActive = transform.GetChild(0).gameObject;
        active = transform.GetChild(1).gameObject;
        interaction = GameObject.Find("Interaction text").GetComponent<TextMeshProUGUI>();
    }

        
}
