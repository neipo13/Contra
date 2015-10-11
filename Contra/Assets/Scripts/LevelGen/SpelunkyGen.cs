using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class SpelunkyGen:MonoBehaviour
{
    public int Height;
    public int Width;

    public int RoomWidth;
    public int RoomHeight;

    public TilePair[] TopLeftCornerTiles;
    public TilePair[] TopTiles;
    public TilePair[] TopRightCornerTiles;
    public TilePair[] LeftTiles;
    public TilePair[] CenterTiles;
    public TilePair[] RightTiles;
    public TilePair[] BottomLeftCornerTiles;
    public TilePair[] BottomTiles;
    public TilePair[] BottomRightCornerTiles;
    public TilePair[] CornerRoomTiles;
    public TilePair[] OuterTiles;
    public TilePair[] HorizontalHallTiles;
    public TilePair[] VerticalHallTiles;

    public int tileWidth = 32;
    public int tileHeight = 32;

    public GameObject[,] tileList;

    public Room[,] rooms;

    public TextAsset RoomDataXML;
    private List<string> LRrooms;

    public Transform parentTrans;

    public void Start()
    {
        LRrooms = new List<string>();
        PopulateRoomLists();
        rooms = new Room[Width, Height];
        tileList = new GameObject[Width * RoomWidth, Height * RoomHeight];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                rooms[i, j] = new Room(RoomWidth, RoomHeight, LRrooms);
            }
        }
        PlaceTiles();
    }

    protected void PopulateRoomLists()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(RoomDataXML.text);
        //first load LR - Left to Right
        XmlNodeList nodeList = xmlDoc.GetElementsByTagName("LR");

        foreach (XmlNode node in nodeList)
        {
            XmlNodeList dataList = node.ChildNodes;
            foreach(XmlNode r in dataList)
            {
                LRrooms.Add(r.InnerText);
            }
        }
    }

    public void PlaceTiles()
    {
        GameObject tile;
        SpriteRenderer renderer;
        Transform tileTrans;
        int counter = 0;
        for (int j = 0; j < Height; j++)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int l = 0; l < RoomHeight; l++ )
                {
                    for (int k = 0; k < RoomWidth; k++)
                    {
                        counter++;
                        //create an Empty Game Object, parented to this object
                        tile = new GameObject();
                        tile.name = "Tile" + counter.ToString();
                        tileTrans = tile.transform;
                        tileTrans.localScale = parentTrans.localScale;
                        tileTrans.position = new Vector3(
                            (i * RoomWidth) + (k),
                            (-j * RoomHeight) - (l),
                            0.0f);
                        tileTrans.parent = parentTrans;
                        if (rooms[i, j].tiles[k, l] != '0')
                        {
                            renderer = tile.AddComponent<SpriteRenderer>();
                            renderer.sprite = SelectTile(CenterTiles);
                            tile.AddComponent<BoxCollider2D>();
                        }
                        tileList[(i * k) + k, (j * l) + l] = tile;
                    }
                }

            }
        }
    }

    protected Sprite SelectTile(TilePair[] tiles)
    {
        float selection = Random.Range(0.0f, 1.0f);
        float total = 0.0f;
        for (int i = 0; i < tiles.Length; i++)
        {
            total += tiles[i].probability;
            if (selection < total)
            {
                return tiles[i].sprite;
            }
        }
        //in case of error always return the first value
        return tiles[0].sprite;
    }
}

[System.Serializable]
public class TilePair
{
    public Sprite sprite;
    [Range(0.0f, 1.0f)]
    public float probability;
}

public class Room
{
    public char[,] tiles;
    private string testString = "00000000110000000011000000001100000000110000000011000000001100000000111112222111";
    
    public Room(int width, int height, List<string> roomsList)
    {
        tiles = new char[width,height];
        for (int j = 0; j < height ; j++)
        {
            for(int i = 0; i < width; i++ )
            {
                int r = Random.Range(0, roomsList.Count);
                tiles[i, j] = roomsList[r][(i + j * width)];
            }
        }
    }
}
