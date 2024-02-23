using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadPlayerScript : MonoBehaviour
{
    private PlayerScript characterScript;
    private Transform characterPosition;

    // Start is called before the first frame update
    void Start()
    {
        characterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        characterPosition = GameObject.FindGameObjectWithTag("Player").transform;
        characterScript.OnDeath += MoveDeathMarker;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveDeathMarker()
    {       
        var newPosition = new Vector3(characterPosition.transform.position.x, characterPosition.transform.position.y, characterPosition.transform.position.z);

        gameObject.transform.position = newPosition;
    }
}
