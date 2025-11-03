using System;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Drawing;
using UnityEditor.Experimental.GraphView;
using System.ComponentModel.Design;

public enum BlockType
{
    NONE,
    BASIC,
    ARROW,
    GRAVITY
}

public class PlayerScript : MonoBehaviour
{
    public GameObject basicPrefab;
    public GameObject arrowPrefab;
    public GameObject gravityPrefab;
    public GameObject basicBlockImage;
    public GameObject arrowBlockImage;
    public GameObject gravityBlockImage;
    public Dictionary <String, KeyCode> moveControls = new Dictionary<String, KeyCode>();
    public Boolean flipped;
    public Boolean onGround;
    public Dictionary<BlockType, GameObject> prefabDict = new Dictionary<BlockType, GameObject>();
    public float moveForce;
    private float initialMoveForce;
    public float jumpForce;
    public Rigidbody2D rb;
    private List<Vector3> placedBlocks;
    public List<BlockType> inventory;
    public BlockType selectedBlockType;
    public GameObject gameScene;
    public GameObject currentLevel;
    public int currentLevelIndex = 0;
    public Transform canvasTransform;
    public List<GameObject> blockImages;
    public Dictionary<int, List<BlockType>> inventories;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = new List<BlockType>();
        inventories = new Dictionary<int, List<BlockType>>();
        inventories[0] = new List<BlockType>() { BlockType.BASIC };
        inventories[1] = new List<BlockType>() { BlockType.ARROW };
        inventories[2] = new List<BlockType>() { BlockType.GRAVITY};
        foreach (BlockType block in inventories[0])
        {
            giveBlock(block);
        }

        placedBlocks = new List<Vector3>();
        rb = GetComponent<Rigidbody2D>();
        initialMoveForce = moveForce;

        if (inventory.Count > 0)
            selectedBlockType = inventory[0];
        else
            selectedBlockType = BlockType.NONE;

        prefabDict[BlockType.BASIC] = basicPrefab;
        prefabDict[BlockType.ARROW] = arrowPrefab;
        prefabDict[BlockType.GRAVITY] = gravityPrefab;

        moveControls["jump"] = KeyCode.UpArrow;
        moveControls["left"] = KeyCode.LeftArrow;
        moveControls["right"] = KeyCode.RightArrow;

        currentLevel = gameScene.transform.GetChild(0).gameObject;
    }

    public float getInitialMoveForce()
    {
        return initialMoveForce;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(moveControls["jump"]) && onGround)
        {
            if (!flipped)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                moveForce /= 4;
                onGround = false;
            }
            else
            {
                rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
                moveForce /= 4;
                onGround = false;
            }
        }

        if (Input.GetKey(moveControls["right"]))
        {
            rb.AddForce(Vector2.right * moveForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(moveControls["left"]))
        {
            rb.AddForce(Vector2.left * moveForce, ForceMode2D.Impulse);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            mousePos.z = 0f; // Set z to 0 for 2D

            // Snap to nearest multiple of 1
            mousePos.x = Mathf.Round(mousePos.x / 1f) * 1f;
            mousePos.y = Mathf.Round(mousePos.y / 1f) * 1f;

            placeBlock(mousePos, selectedBlockType);
        }
    }

    public void giveBlock(BlockType type)
    {
        inventory.Add(type);
        if (selectedBlockType == BlockType.NONE)
        {
            selectedBlockType = type;
        }
        updateInventoryUI();
    }

    public void flipControls()
    {
        KeyCode temp = moveControls["left"];
        moveControls["left"] = moveControls["right"];
        moveControls["right"] = temp;
    }

    public void placeBlock(Vector3 position, BlockType type)
    {
        if (placedBlocks.Contains(position) || inventory.Count == 0)
        {
            return;
        }
        else
        {
            placedBlocks.Add(position);
        }
        if (inventory != null && inventory.Contains(type))
        {
            switch (type)
            {
                case BlockType.BASIC:
                    Instantiate(basicPrefab, position, Quaternion.identity);
                    break;
                case BlockType.ARROW:
                    Instantiate(arrowPrefab, position, Quaternion.identity);
                    break;
                case BlockType.GRAVITY:
                    Instantiate(gravityPrefab, position, Quaternion.identity);
                    break;
                default:
                    break;
            }
            inventory.RemoveAt(0);
            selectedBlockType = inventory.Count > 0 ? inventory[0] : BlockType.NONE;
        }
        updateInventoryUI();
    }

    public void nextLevel()
    {
        currentLevelIndex += 1;
        this.transform.position = new Vector3((currentLevelIndex*70)-6, -3, 0);
        Destroy(currentLevel);
        currentLevel = gameScene.transform.GetChild(currentLevelIndex).gameObject;
        if (inventories.ContainsKey(currentLevelIndex))
        {
            foreach (BlockType block in inventories[currentLevelIndex])
            {
                giveBlock(block);
            }
        }
        else
        {
            inventory = null;
        }
    }

    public void updateInventoryUI()
    {
        int index = 0;
        foreach (GameObject img in blockImages)
        {
            Destroy(img);
        }
        foreach (BlockType type in inventory)
        {
            GameObject currentImage = null;
            switch (type)
            {
                case BlockType.BASIC:
                    currentImage = Instantiate(basicBlockImage, canvasTransform);
                    break;
                case BlockType.ARROW:
                    currentImage = Instantiate(arrowBlockImage, canvasTransform);
                    break;
                case BlockType.GRAVITY:
                    currentImage = Instantiate(gravityBlockImage, canvasTransform);
                    break;
                default:
                    break;
            }
            currentImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-330 + (index * 50), 120);
            blockImages.Add(currentImage);
            index += 1;
        }
    }
}
