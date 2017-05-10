using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scatterPointBehavior : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Watch")
        {
            //Toggles the data window
            if (transform.GetChild(0).gameObject.active)
            {
                transform.GetChild(0).gameObject.SetActiveRecursively(false);
            }

            else
            {
                transform.GetChild(0).gameObject.SetActiveRecursively(true);
            }
        }
    }


}
