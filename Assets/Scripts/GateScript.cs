using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isOpen && transform.position.y < -1)
        {
            transform.position += new Vector3(0, 0.1f, 0);
        }
        else if (!isOpen && transform.position.y > -2.83)
        {
            transform.position -= new Vector3(0, 0.1f, 0);
        }
    }

    [ContextMenu("Open")]
    public void Open()
    {
        isOpen = true;
    }

    [ContextMenu("Close")]
    public void Close()
    {
        isOpen = false;
    }
}
