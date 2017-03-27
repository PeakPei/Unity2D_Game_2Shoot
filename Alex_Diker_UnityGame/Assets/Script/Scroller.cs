/*
Alex Diker
George Brown College
100746284
COMP3064 

SECOND SCROLLER SCRIPT INCASE FIRST SCRIPT DOES NOT WORK [ NOT USING TUTORIAL METHOD ] 
First script uses a tutorial I found online, as I was unable to get my script working but this one also works
when you alter the location of the background image.

*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scroller : MonoBehaviour
{
    [SerializeField]
    private List<Transform> backgrounds;
    private float speed = 8;

    void Awake()
    {
        backgrounds = new List<Transform>();
        foreach (Transform t in gameObject.GetComponentInChildren<Transform>())
        {
            if (t != null)
                backgrounds.Add(t);
        }
    }

    void FixedUpdate()
    {
        foreach (Transform t in backgrounds)
        {
            t.Translate(Vector3.left * speed * Time.deltaTime);
            Vector3 cameraBoundsLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)); // The left point in world space of the camera.
            Vector3 objectBounds = t.GetComponent<SpriteRenderer>().bounds.extents; // The extents of the background. This is always half of the size.
            Vector3 objectBoundsRight = t.position + objectBounds; // The world position of the extents of the background.
            if (objectBoundsRight.x <= cameraBoundsLeft.x) // Check to see if the background is no longer visible by the camera. If so, move it to the other side of the 2nd background object.
            {
                t.position += new Vector3(objectBounds.x * 4, 0, 0);
            }
        }
    }
}