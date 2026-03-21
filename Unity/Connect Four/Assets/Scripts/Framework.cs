using UnityEngine;

public class Framework : MonoBehaviour
{
    public Transform[,] framework = new Transform[8,5]; 
    public field[,] fields = new field[8,5];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool InputObject(int column, bool isPlayerOne, GameObject roundObject)
    {
        for(int i = 0; i < 5; i++)
        {
            if(fields[column, i] == field.empty)
            {
                if (isPlayerOne)
                {
                    fields[column, i] = field.playerOne;
                }
                else
                {
                    fields[column, i] = field.playerTwo;
                }
                Instantiate(roundObject); // position it above column and let it fall to the correct transform in framework
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