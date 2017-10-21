using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVitalsObserver {

    void UpdateVital(string key, IVital newVital);

}
