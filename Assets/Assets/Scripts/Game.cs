using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// comment
public class Game : MonoBehaviour
{
    public GameObject Chesspiece;
    public GameObject EndText;
    public GameObject TurnText;

    private GameObject[,] positions = new GameObject[8,8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    private string currentPlayer = "white";
    
    private bool gameOver = false;

    void Start()
    {
        playerWhite = new GameObject[]
        {
            Create("white_rook",0,0), Create("white_knight",1,0), Create("white_bishop",2,0), Create("white_queen",3,0),
            Create("white_king",4,0), Create("white_bishop",5,0), Create("white_knight",6,0), Create("white_rook",7,0),
            Create("white_pawn",0,1), Create("white_pawn",1,1), Create("white_pawn",2,1), Create("white_pawn",3,1),
            Create("white_pawn",4,1), Create("white_pawn",5,1), Create("white_pawn",6,1), Create("white_pawn",7,1), };
        playerBlack = new GameObject[]
        {
            Create("black_rook",0,7), Create("black_knight",1,7), Create("black_bishop",2,7), Create("black_queen",3,7),
            Create("black_king",4,7), Create("black_bishop",5,7), Create("black_knight",6,7), Create("black_rook",7,7),
            Create("black_pawn",0,6), Create("black_pawn",1,6), Create("black_pawn",2,6), Create("black_pawn",3,6),
            Create("black_pawn",4,6), Create("black_pawn",5,6), Create("black_pawn",6,6), Create("black_pawn",7,6),
        };


        // Set all the piece positions on the position board
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }

        Turn("white");

    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(Chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;

    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }
    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }
    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }
    public void NextTurn()
    {
        if (!gameOver)
        {
            if (currentPlayer == "white")
            {
                currentPlayer = "black";
                Turn("black");
            }
            else
            {
                currentPlayer = "white";
                Turn("white");
            }
        }
    }

    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Game");
        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;
        Turn("none");

        EndText.transform.Find("WinnerText").gameObject.SetActive(true);
        EndText.transform.Find("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";
        EndText.transform.Find("RestartText").gameObject.SetActive(true);
    }

    public void Turn(string playerTurn)
    {
        if (playerTurn == "white")
        {
            TurnText.transform.Find("White").gameObject.SetActive(true);
            TurnText.transform.Find("Black").gameObject.SetActive(false);
        }
        else if (playerTurn == "black")
        {
            TurnText.transform.Find("White").gameObject.SetActive(false);
            TurnText.transform.Find("Black").gameObject.SetActive(true);
        }
        else
        {
            TurnText.transform.Find("White").gameObject.SetActive(false);
            TurnText.transform.Find("Black").gameObject.SetActive(false);
        }
    }
}
