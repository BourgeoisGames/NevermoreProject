using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTestInteraction : MonoBehaviour, Interactive {

    public float testLength = 10.0f;
    public int numberOfTests = 2;
    public Vector2 progressPosition = new Vector2(0, 0);

    private GameObject skillTestUI;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Interact()
    {
        Debug.Log(gameObject.name + " Skill Test Interact");
        GameObject canvas = GameObject.Find("Canvas");

        GameObject prefab = Resources.Load<GameObject>("ProgressBar");
        GameObject progressBar = Instantiate<GameObject>(prefab);
        progressBar.transform.SetParent(canvas.transform);
        RectTransform rect = progressBar.GetComponent<RectTransform>();
        rect.position = progressPosition;
    }
}
