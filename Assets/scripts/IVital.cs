using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVital {

    string name  { get; set; }
    Color color { get; set; }
    float max { get; set; }
    float current { get; set; }

}
