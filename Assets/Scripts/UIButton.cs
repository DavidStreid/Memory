using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour {
    // GameObject to have method invoked
    [SerializeField] private GameObject targetObject;
    // Method to invoke
    [SerializeField] private string m_targetMessage;

    public Color highlightColor = Color.cyan;

    public void OnMouseEnter()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if(sprite != null){
            sprite.color = highlightColor;
        }
    }

    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if(sprite != null){
            sprite.color = Color.white;
        }
    }

    public void OnMouseDown()
    {
        // "Pop" - Make button a bit bigger when pressing
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void OnMouseUp()
    {
        // Return button to normal scale
        transform.localScale = Vector3.one;

        if(targetObject != null){
            // Invokes method - note: costly on CPU
            targetObject.SendMessage(m_targetMessage);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
