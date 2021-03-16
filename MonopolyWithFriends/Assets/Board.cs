using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static readonly float SIZE_Y = 1;
    public static readonly float SIZE_X = 0.7f;

    public GameObject tiles;
    public GameObject underBoard;
    public GameObject OverBoard;

    public float spacing = 0.02f;

    public Node[] board;

    private void Start()
    {
        if (board.Length % 4 != 0)
            throw new boardExeption("The board is not devidable by 4");

        CreateBoard();
    }

    private void CreateBoard()
    {
        int numberOfTile = (board.Length / 4);
        float size = SIZE_Y + ((numberOfTile-1) * (SIZE_X + spacing)) + spacing;
        underBoard.transform.localScale = new Vector3(size + SIZE_Y + (spacing*4), underBoard.transform.localScale.y, size + SIZE_Y + (spacing*4));
        OverBoard.transform.localScale = new Vector3(size - SIZE_Y - (spacing*4), OverBoard.transform.localScale.y, size - SIZE_Y - (spacing*4));
        float halfSize = size / 2f;
        Vector3 curPos = new Vector3(-halfSize,0f,halfSize);
        Vector3 dir = new Vector3(1f, 0f, 0f);
        int dirId = 0;
        float angle = 0f;

        for (int i = 0; i < board.Length; i++)
        {
            GameObject newTile = Instantiate(board[i].prefab);
            newTile.transform.name = board[i].name;
            newTile.transform.position = curPos;
            newTile.transform.parent = tiles.transform;
            newTile.transform.rotation = Quaternion.Euler(0f,angle,0f);
            

            if (i + 1 != board.Length)
            {
                if (board[i].Corner || board[i + 1].Corner)
                {
                    if (board[i].Corner)
                    {
                        switch (dirId)
                        {
                            case 0:
                                dirId = 1;
                                break;
                            case 1:
                                dirId = 2;
                                dir = new Vector3(0,0,-1);
                                angle = 90f;
                                break;
                            case 2:
                                dirId = 3;
                                dir = new Vector3(-1, 0, 0);
                                angle = 180f;
                                break;
                            case 3:
                                dirId = 4;
                                dir = new Vector3(0, 0, 1);
                                angle = -90f;
                                break;
                            default:
                                break;
                        }
                    }
                        
                    curPos += dir * (0.85f + spacing);
                }
                else
                {
                    curPos += dir * (SIZE_X + spacing);
                }
            }

        }

    }
}

public class boardExeption : System.Exception
{

    public boardExeption()
    {
        Debug.LogError("Something went wrong with the board");
    }

    public boardExeption(string msg)
    {
        Debug.LogError(msg);
    }
}


[System.Serializable]
public class Node
{
    public string name;
    public bool Corner = false;
    public GameObject prefab;
    public float price;
}
