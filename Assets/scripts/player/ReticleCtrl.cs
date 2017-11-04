using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleCtrl : MonoBehaviour {

    public Texture2D reticleTexture;
    public float reticleScale = 1.0f;

    private bool active = true;
    private Rect reticlePos;
    private Vector2 windowSize;

	// Use this for initialization
	void Start () {
        windowSize = new Vector2(Screen.width, Screen.height);
		UpdateReticlePosition();
	}

    private void UpdateReticlePosition()
    {
        float x = (Screen.width - (reticleScale*reticleTexture.width)) / 2.0f;
        float y = (Screen.height - (reticleScale*reticleTexture.height)) / 2.0f;
		reticlePos = new Rect(x, y, (reticleScale*reticleTexture.width), (reticleScale*reticleTexture.height));
    }

    void OnGUI()
    {
        if (active && Time.timeScale != 0)
        {
            GUI.DrawTexture(reticlePos, reticleTexture);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (windowSize.x != Screen.width || windowSize.y != Screen.height)
        {
            UpdateReticlePosition();
        }
	}
}
