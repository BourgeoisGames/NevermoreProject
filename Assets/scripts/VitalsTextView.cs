using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VitalsTextView : MonoBehaviour, IVitalsObserver {

    private GameObject textPrefab;
    private Dictionary<string, Text> vitalDisplays = new Dictionary<string, Text>();

	void Start () {
        textPrefab = Resources.Load<GameObject>("PrefabText");
        ClearDisplayArea();
        SetupVitalDisplays();
	}
    
    private void ClearDisplayArea()
    {
        int count = 0;
        // purges impure children from the display area.
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
            count++;
        }
        Debug.Log("removed " + count + " unwanted child(ren) from VitalsTextView at start up");
    }

    private void SetupVitalDisplays()
    {
        PlayerVitals.inst.Subscribe(this);
        foreach (string key in PlayerVitals.inst.vitals.Keys)
        {
            Debug.Log("initializing vital '" + key + "'");
            IVital vital = PlayerVitals.inst.vitals[key];
            DisplayNewVital(vital);
        }
    }

    private void DisplayNewVital(IVital vital)
    {
        Debug.Log("displaying new vital '" + vital.name + "'");
        // creates a new text object based on the given IVital, and puts it in the display.
        GameObject newTextObj = Instantiate<GameObject>(textPrefab);
        Text txt = newTextObj.GetComponent<Text>();
        txt.text = GenerateVitalText(vital);
        txt.color = vital.color;
        vitalDisplays.Add(vital.name, txt);
        newTextObj.transform.SetParent(this.transform);
    }

    private string GenerateVitalText(IVital vital) {
        return vital.name + ": " + vital.current + " / " + vital.max;
    }


    public void UpdateVital(string key, IVital vital)
    {
        Text text = vitalDisplays[key];
        text.text = GenerateVitalText(vital);
    }
}
