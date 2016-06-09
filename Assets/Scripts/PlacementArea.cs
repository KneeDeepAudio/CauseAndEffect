using UnityEngine;
using System.Collections;

public class PlacementArea : MonoBehaviour
{

    public bool objectPlaced = false;
    public bool blockPlaced = false;
    public bool full = false;
    public float shadeAlpha = 0.35f;
    public int maxBlocks = 4;

    private int blockCount = 0;
    private int[] blocks;
    private SpriteRenderer sprite;
    private Color col;
    private BoxCollider2D col2D;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = sprite.color;
        blocks = new int[4];
        col2D = GetComponent<BoxCollider2D>();
    }

    public bool IsFull
    {
        get
        {
            return this.full;
        }
    }

    public bool ObjectPlaced
    {
        get
        {
            return this.objectPlaced;
        }
    }

    public bool BlocksPlaced
    {
        get
        {
            return this.blockPlaced;
        }
    }

    public void PlaceBlock()
    {
        blockCount++;
        CheckFull();
        blockPlaced = true;
    }

    public void PlaceObject()
    {
        objectPlaced = true;
        CheckFull();
    }

    public void RemoveBlock()
    {
        blockCount--;
        CheckFull();
        if (blockCount == 0)
        {
            blockPlaced = false;
        }
    }

    public void RemoveObject()
    {
        objectPlaced = false;
        CheckFull();
    }

    public void CheckFull()
    {
        if (objectPlaced == true || blockCount >= maxBlocks)
        {
            full = true;
            sprite.enabled = false;
            col2D.enabled = false;
        }

        else
        {
            full = false;
            sprite.enabled = true;
            col2D.enabled = true;
        }
    }
}
