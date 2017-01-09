using UnityEngine;
using System.Collections;

public class PlacementArea : MonoBehaviour
{

    public bool objectPlaced = false;
    public bool blockPlaced = false;
    public bool full = false;
    public float shadeAlpha = 0.35f;
    public int maxBlocks = 4;

    public ObjectSpawn objectArea;
    public ObjectSpawn[] blockAreas;

    private int blockCount = 0;
//    private int[] blocks;
    private SpriteRenderer sprite;
    private BoxCollider2D col2D;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
//        blocks = new int[4];
        col2D = GetComponent<BoxCollider2D>();

        RemoveHighlight();
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

    public bool BlockPlaced()
    {
        foreach (ObjectSpawn spawnArea in blockAreas)
        {
            if (spawnArea.IsFull)
                return true;
        }
        return false;
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

    public void HighlightObjectPlacement()
    {
        RemoveHighlight();
        if(!BlockPlaced() && !objectArea.IsFull)
        objectArea.Highlight();
    }

    public void HighlightBlockPlacement()
    {
        RemoveHighlight();
        foreach (ObjectSpawn spawnArea in blockAreas)
        {
            if(!spawnArea.IsFull && !objectArea.IsFull)
                spawnArea.Highlight();
        }
    }

    public void RemoveHighlight()
    {
        objectArea.RemoveHighlight();
        foreach (ObjectSpawn spawnArea in blockAreas)
        {
            spawnArea.RemoveHighlight();
        }
    }
}
