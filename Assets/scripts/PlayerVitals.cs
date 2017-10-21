using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVitals : MonoBehaviour {

    private Dictionary<string, IVital> _vitals = new Dictionary<string, IVital>();
    private List<IVitalsObserver> vitalObservers = new List<IVitalsObserver>();

    public Dictionary<string, IVital> vitals
    {
        get { return _vitals; }
    }

    private static PlayerVitals _instance;
    public static PlayerVitals inst
    {
        get { return _instance; }
        set { _instance = value; }
    }

    void Awake()
    {
        _instance = this;
        _vitals.Add("Oxygen", new Vital("Oxygen", 1000));
        _vitals.Add("Health", new Vital("Health", 1000));
        _vitals.Add("Energy", new Vital("Energy", 1000));

        _vitals["Oxygen"].color = new Color(0f, 0.443f, 0.506f);
        _vitals["Health"].color = new Color(0.490f, 0.098f, 0.098f);
        _vitals["Energy"].color = new Color(0.196f, 0.196f, 0f);
    }

    private void UpdateObservers(string key)
    {
        foreach (IVitalsObserver observer in vitalObservers)
        {
            observer.UpdateVital(key, vitals[key]);
        }
    }

    public void ChangeVital(string key, float delta)
    {
        _vitals[key].current += delta;
        UpdateObservers(key);
    }

    public void SetVital(string key, float newValue)
    {
        _vitals[key].current = newValue;
        UpdateObservers(key);
    }

    public float GetVital(string key)
    {
        return _vitals[key].current;
    }

    public void Subscribe(IVitalsObserver observer)
    {
        vitalObservers.Add(observer);
    }

    public bool Unsubscribe(IVitalsObserver observer)
    {
        if (vitalObservers.Contains(observer))
        {
            vitalObservers.Remove(observer);
            return true;
        }
        return false;
    }


}
