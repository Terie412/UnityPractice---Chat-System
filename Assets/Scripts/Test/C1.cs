using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1 : MonoBehaviour
{
    private void Awake()
    {
        C2 c= this.GetComponent<C2>();
        if (c == null) Debug.LogWarning("C2还没有加载");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("C1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
