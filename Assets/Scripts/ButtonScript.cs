using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Sprite pressedSprite;
    public Sprite unpressedSprite;

    public UnityEvent buttonPressedEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // On collision with the player, change the sprite
        if (collision.gameObject.layer == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = pressedSprite;
            buttonPressedEvent.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // On leaving collision with the player, change the sprite back
        if (collision.gameObject.layer == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = unpressedSprite;
        }
    }
}
