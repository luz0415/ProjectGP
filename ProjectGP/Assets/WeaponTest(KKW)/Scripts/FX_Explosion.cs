using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("_Destroy", 3f);
    }

    void _Destroy()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
}
