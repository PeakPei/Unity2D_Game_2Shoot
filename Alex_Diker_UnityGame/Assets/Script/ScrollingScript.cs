/*
Alex Diker
George Brown College
100746284
COMP3064 


*/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ScrollingScript : MonoBehaviour
{
    public bool isLinkedToCamera = false;
    public Vector2 speed = new Vector2(30, 30);
	public Vector2 direction = new Vector2(-1, 0);
	
	public bool isLooping = true;
	
	private List<SpriteRenderer> backgroundPart;
	private Vector2 repeatableSize;
	
	void Start()
	{
		if (isLooping)
		{

			backgroundPart = new List<SpriteRenderer>();
			
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				SpriteRenderer r = child.GetComponent<SpriteRenderer>();
				
// Only visible children are displayed
				if (r != null)
				{
					backgroundPart.Add(r);
				}
			}	
			
//Depends on the scrolling direction
			backgroundPart = backgroundPart.OrderBy(t => t.transform.position.x * (-1 * direction.x)).ThenBy(t => t.transform.position.y * (-1 * direction.y)).ToList();
			
// Get the size of the repeatable background dimentions
// so get first dimnetions and last dimnetions from the background.
			var first = backgroundPart.First();
			var last = backgroundPart.Last();
			
			repeatableSize = new Vector2(
				Mathf.Abs(last.transform.position.x - first.transform.position.x),
				Mathf.Abs(last.transform.position.y - first.transform.position.y)
				);
		}
	}
	
	void Update()
	{
// Movement of camera in the right direction

		Vector3 camMovement = new Vector3(
			speed.x + 2 * direction.x, 

// +1 to increase speed

			speed.y * direction.y,
			0);

        camMovement *= Time.deltaTime;
		transform.Translate(camMovement);
		
// Move the camera when linked
		if (isLinkedToCamera)
		{
			Camera.main.transform.Translate(camMovement);
		}		
		if (isLooping)
		{

// Check to see if the object is in the camera bounds before ++
// Camera borders specified also 

			var dist = (transform.position - Camera.main.transform.position).z;
			float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
			float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
			
			var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
			var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
			
// Start and End border using directions 

			Vector3 exitBorder = Vector3.zero;
			Vector3 entryBorder = Vector3.zero;

            if (direction.y < 0)
            {
                exitBorder.y = bottomBorder;
                entryBorder.y = topBorder;
            }
            else if (direction.y > 0)
            {
                exitBorder.y = topBorder;
                entryBorder.y = bottomBorder;
            }

            if (direction.x < 0)
			{
				exitBorder.x = leftBorder;
				entryBorder.x = rightBorder;
			}
			else if (direction.x > 0)
			{
				exitBorder.x = rightBorder;
				entryBorder.x = leftBorder;
			}
			

			
// Get the first object and display it on the background
			SpriteRenderer firstChild = backgroundPart.FirstOrDefault();
			
			if (firstChild != null)
			{
				bool checkVisible = false;
				
// Check to see if our player has passed the camera bounds (set)

				if (direction.x != 0)
				{
					if ((direction.x < 0 && (exitBorder.x > firstChild.transform.position.x))
					    || (direction.x > 0 && (firstChild.transform.position.x > exitBorder.x)))
					{
						checkVisible = true;
					}
				}
				if (direction.y != 0)
				{
					if ((direction.y < 0 && (firstChild.transform.position.y < exitBorder.y))
					    || (direction.y > 0 && (firstChild.transform.position.y > exitBorder.y)))
					{
						checkVisible = true;
					}
				}
				
// Check if the sprite is visible on the camera [ ] 
				if (checkVisible)
				{
// object is away from camera bounds therefore we recycle it.
// That means he was the first, he's now the last
// And we physically moves player to the further position possible
					
					if (firstChild.IsVisibleFrom(Camera.main) == false)
					{
// Set position in the end
						firstChild.transform.position = new Vector3(
							firstChild.transform.position.x + ((repeatableSize.x + firstChild.bounds.size.x) * -1 * direction.x),
							firstChild.transform.position.y + ((repeatableSize.y + firstChild.bounds.size.y) * -1 * direction.y),
							firstChild.transform.position.z
							);
						
// The first part become the last one
						backgroundPart.Remove(firstChild);
						backgroundPart.Add(firstChild);
					}
				}
			}
			
		}
	}
}