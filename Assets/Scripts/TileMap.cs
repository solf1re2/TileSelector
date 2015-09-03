using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

    //Place a gameobject with a sprite renderer attached and the grass tile in that. 
    //Create it into a prefab and place it into your "Resources" folder.
    public Transform tilePrefab;

    //Tilemap width and height
    public int mapWidth = 10;
    public int mapHeight = 10;

    //Size you want your tile in unity units
    public float tileSize = 0.32f;

    //2D array to hold all tiles, which makes it easier to reference adjacent tiles etc.
    public Transform[,] map;

    void Start()
    {
        //Load prefab "Grass" from "Resources/Prefabs/" folder.
        tilePrefab = Resources.Load<Transform>("Prefabs/GrassTile");

        //If we can't find the prefab then log a warning.
        if (!tilePrefab)
            Debug.LogWarning("Unable to find TilePrefab in your Resources folder.");

        //Initialize our 2D Transform array with the width and height
        map = new Transform[mapWidth, mapHeight];

        //Iterate over each future tile positions for x and y
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                //Instantiate tile prefab at the desired position as a Transform object
                Transform tile = Instantiate(tilePrefab, new Vector3(x * tileSize, y * tileSize, 0), Quaternion.identity) as Transform;
                //Set the tiles parent to the GameObject this script is attached to
                tile.parent = transform;
                //Set the 2D map array element to the current tile that we just created.
                map[x, y] = tile;
            }
        }

        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(mapWidth * tileSize, mapHeight * tileSize, 0.5f);
        box.center = transform.localPosition + new Vector3((mapWidth * tileSize) * 0.5f, (mapHeight * tileSize) * 0.5f, 0);
        
    }

    //Returns a tile from the map array at x and y
    public Transform GetTileAt(float fx, float fy)
    {
        int x = (int)fx;
        int y = (int)fy;
        if (x < 0 || y < 0 || x > mapWidth || y > mapHeight)
        {
            Debug.LogWarning("X or Y coordinate is out of bounds!");
            return null;
        }
        return map[x, y];
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                if (hitInfo.collider.gameObject == gameObject)
                {
                    Debug.Log("I got hit!");
                   
                }
            }
        }
    }
}
