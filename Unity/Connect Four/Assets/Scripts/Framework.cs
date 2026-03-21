using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Framework : MonoBehaviour
{
    //Player
    public GameObject ballPlayerOne;
    public GameObject characterPlayerOne;
    public GameObject ballPlayerTwo;
    public GameObject characterPlayerTwo;
    //UI
    public TextMeshProUGUI turnText;
    public GameObject endScreen;
    public TextMeshProUGUI endText;
    public TextMeshProUGUI fact;
    //Others
    public PlayerInput _pi;
    private InputAction _shoot;
    public field[,] fields = new field[8,5];
    public int currentTurn = 0;
    public bool playerOneTurn = true;
    public bool isOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(characterPlayerOne, new Vector3(-6.5f, 6.1f, 1f), Quaternion.identity);
        Instantiate(characterPlayerTwo, new Vector3(6.5f, 6.1f, 1f), Quaternion.Euler(0, -180, 0));

        _pi = GetComponent<PlayerInput>();
        _shoot = _pi.actions["Shoot"];
        _shoot.started += ctx => Shoot();

        int toinCoss = UnityEngine.Random.Range(0,2);
        if(toinCoss == 1) playerOneTurn = false;
    }

    public void Shoot() {
        if(isOver) return;
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            int c = 0;
            if(hit.point.x <= -6.5f) c = 0;
            else if(hit.point.x <= -4.5f) c = 1;
            else if(hit.point.x <= -2.5f) c = 2;
            else if(hit.point.x <= -0.5f) c = 3;
            else if(hit.point.x <= 1.5f) c = 4;
            else if(hit.point.x <= 3.5f) c = 5;
            else if(hit.point.x <= 5.5f) c = 6;
            else c = 7;
            //Debug.Log(hit.point);
            InputObject(c);
        }
    }

    public bool InputObject(int column)
    {
        for(int i = 0; i < 5; i++)
        {
            if(fields[column, i] == field.empty)
            {
                if (playerOneTurn)
                {
                    fields[column, i] = field.playerOne;
                }
                else
                {
                    fields[column, i] = field.playerTwo;
                }
                Vector3 spawnPoint = new Vector3(-7 + column*2, 8, 0);
                GameObject g = Instantiate((playerOneTurn == true) ? ballPlayerOne : ballPlayerTwo, spawnPoint, quaternion.identity);
                Vector3 destination = new Vector3(-7 + column*2,-4 + 2*i, 0);
                g.GetComponent<Ball>().SetTarget(destination);
                playerOneTurn = !playerOneTurn;
                currentTurn++;
                turnText.text = "Turn " + currentTurn;
                //Include short wait time before next turn and check
                CheckWinAndDraw();
                return true;
            }
        }

        //Feedback das Reihe voll
        return false;
    }

    public void CheckWinAndDraw()
    {
        for(int i = 0; i < fields.GetLength(0); i++)
        {
            for(int y = 0; y < fields.GetLength(1); y++)
            {
                if(fields[i, y] == field.empty) continue;

                if(CheckDirection(i,y,1,0, fields[i,y]) || CheckDirection(i,y,0,1, fields[i,y]) || CheckDirection(i,y,1,1, fields[i,y]) || CheckDirection(i,y,-1,1, fields[i,y]))
                {
                    string message = "Player 1 Won";
                    if(fields[i,y] == field.playerTwo) message = "Player 2 Won"; 
                    EndGame(message, "");
                    return;
                }
            }
        }

        if(currentTurn == 40)
        {
             EndGame("Draw", null);
        }
    }

    private bool CheckDirection(int widthPos, int heightPos, int widthDirection, int heightDirection, field fieldType)
    {
        for(int i = 0; i < 4; i++)
        {
            int w = widthPos + i * widthDirection;
            int h = heightPos + i * heightDirection;
            if(w < 0 || w >= fields.GetLength(0) || h < 0 || h >= fields.GetLength(1)) return false;

            if(fields[w,h] != fieldType) return false;
        }
        return true;
    }

    public void EndGame(string message,  string? factText)
    {
        isOver = true;
        endScreen.SetActive(true);
        endText.text = message;

        if(factText == null) fact.gameObject.SetActive(false);
        else fact.GetComponentInChildren<TextMeshProUGUI>().text = factText;
    }
}

public enum field
{
    empty,
    playerOne,
    playerTwo
}