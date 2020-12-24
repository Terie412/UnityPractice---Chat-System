using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2 : MonoBehaviour
{
    private void Awake()
    {
        C1 c = this.GetComponent<C1>();
        if (c == null) Debug.LogWarning("C1还没有加载");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("C2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
