using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    [SerializeField]
    private string logStr;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(logStr);
    }

    // Update is called once per frame
    void Update()
    {
    }
}