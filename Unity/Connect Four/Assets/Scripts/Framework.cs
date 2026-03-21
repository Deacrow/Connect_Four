using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Framework : MonoBehaviour
{
    public GameObject ballPlayerOne;
    public GameObject ballPlayerTwo;
    public GameObject characterPlayerOne;
    public GameObject characterPlayerTwo;
    public PlayerInput _pi;
    private InputAction _shoot;
    public field[,] fields = new field[8,5];
    public int currentTurn = 0;
    public bool playerOneTurn = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pi = GetComponent<PlayerInput>();
        _shoot = _pi.actions["Shoot"];
        _shoot.started += ctx => Shoot();
        //Toss Coin who begins
        //Let that Player begin
    }

    public void Shoot() {
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
            else if(hit.point.x >= 7.5f) c = 7;
            Debug.Log(hit.point);
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
                return true;
            }
        }
        return false;
    }

    public void CheckWinAndDraw()
    {
        //Go Through all fields and check if any player has won or if it is a draw, if yes enable end screen
    }
}

public enum field
{
    empty,
    playerOne,
    playerTwo
}