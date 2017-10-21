using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vital : IVital {

    private static readonly Color defaultColor = new Color(0.2f, 0.2f, 0.2f);

    public Vital(string name, float max)
    {
        this._name = name;
        this.max = max;
        this.current = max;
        this.color = defaultColor;
    }

    private string _name;
    public string name
    {
        get { return _name; }
        set { _name = value; }
    }

    private Color _color;
    public Color color
    {
        get {
            return _color;
        }
        set {
            _color = value;
        }
    }

    private float _max;
    public float max
    {
        get {
            return _max;
        }
        set {
            _max = value;
        }
    }
        
    private float _current;
    public float current {
        get {
            return _current;
        }
        set {
            _current = value;
        }
    }
}
