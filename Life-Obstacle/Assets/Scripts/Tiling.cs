using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour
{
    public int offsetY = 2;         // the offset so that clones load before getting into cam view

    // used to check if we need to instantiate clones
    public bool hasAUpClone = false;

    public bool hasALowerClone = false;

    public bool reverseScale = false;   // used if the object is not tilable

    public float moveSpeed;

    private SpriteRenderer sr;
    private Sprite ogSprite;
    private float spriteWidth = 0f;
    private Camera cam;
    private Transform myTransform;

    private void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ogSprite = sr.sprite;
        spriteWidth = this.transform.parent.localScale.x * sr.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        MoveClone();

        // calculates the cameras extent (half the width) of what the camera can see in world coordinates
        float camHorizontalExtent = cam.orthographicSize * Screen.width / Screen.height;

        // calculate the y position where tha camera can seee the edge of the sprite
        float edgePositionUp = (myTransform.position.y + spriteWidth / 2) - camHorizontalExtent;
        float edgePositionDown = (myTransform.position.y - spriteWidth / 2) + camHorizontalExtent;

        // does it still need clones? If not do nothing
        if (!hasALowerClone || !hasAUpClone)
        {

            // checking if edge of sprite is visible and instantiate new clone if possible
            if (cam.transform.position.y >= edgePositionUp - offsetY && !hasAUpClone)
            {
                MakeNewClone(1);
                hasAUpClone = true;
            }
            else if (cam.transform.position.y <= edgePositionDown + offsetY && !hasALowerClone)
            {
                MakeNewClone(-1);
                hasALowerClone = true;
            }

        }

        // check if clone is 'offscreen' (below) and delete
        if (edgePositionUp + spriteWidth / 2 <= cam.transform.position.y - offsetY)
        {
            hasALowerClone = false;
            Destroy(this.gameObject);
        }

    }

    // function that instantiates a new clone up or down required
    void MakeNewClone(int direction)
    {
        // calculating the position of the clone
        Vector3 newPosition =
            new Vector3(myTransform.position.x, myTransform.position.y + spriteWidth * direction, myTransform.position.z);

        // instantiating new clone and storing it in a variable
        Transform newClone = (Transform)Instantiate(myTransform, newPosition, myTransform.rotation, myTransform.parent);



        // if not tilable reverse the y size of object to smooth out the seam
        if (reverseScale)
        {
            newClone.localScale = new Vector3(newClone.localScale.x, newClone.localScale.y * -1, newClone.localScale.z);
        }

        //newClone.parent = myTransform.parent;

        if (direction > 0)
        {
            // if clone is up, tell the clone it has a instance below
            // (to prevent instantiates over existing clones)
            newClone.GetComponent<Tiling>().hasALowerClone = true;
        }
        else
        {
            newClone.GetComponent<Tiling>().hasAUpClone = true;
        }
    }

    // move clone below
    private void MoveClone()
    {
        this.transform.Translate(0, -moveSpeed, 0);
    }

    private bool InCenter()
    {
        return this.transform.position.y < cam.transform.position.y;

    }
}
