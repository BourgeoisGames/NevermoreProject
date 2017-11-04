using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGravity : MonoBehaviour {

    public int[] layersIgnored;
    private static List<int> _layersIgnored;

    public KeyCode swapKey;
    public float gravitySwapCooldown = 0.5f;
    private static float _cooldown = 0f;

    private static GlobalGravity _inst;
    public static GlobalGravity inst
    {
        get { return _inst; }
    }

    private static bool _gravityActive = true;
    public static bool gravityActive
    {
        get
        {
            return _gravityActive;
        }
        set
        {
            _gravityActive = value;
            CascadeGravitySetting(value);
        }
    }

    void Awake()
    {
        _inst = this;
        _layersIgnored = new List<int>();
        for (int i = 0; i < layersIgnored.Length; i++) {
            _layersIgnored.Add(layersIgnored[i]);
        }
        gravityActive = true;
    }

    void Update()
    {
        if (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
            return;
        }
        if (Input.GetKeyDown(swapKey))
        {
            Debug.Log("toggling gravity active");
            gravityActive = !gravityActive;
        }
    }

    private static void CascadeGravitySetting(bool useGravity)
    {
        Rigidbody[] rbs = (Rigidbody[]) GameObject.FindObjectsOfType(typeof(Rigidbody));
        for (int i = 0; i < rbs.Length; i++)
        {
            if (_layersIgnored.Contains(rbs[i].gameObject.layer)) {
                continue;
            }
            rbs[i].useGravity = useGravity;
        }
    }
}
