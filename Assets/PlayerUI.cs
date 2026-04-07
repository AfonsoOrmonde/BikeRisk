using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    // Start is called before the first frame update

    public TouchButtons androiControls;
    void Start()
    {
        #if UNITY_ANDROID
            androiControls.Activate();
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
