using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    public GameObject[] characters;
    private static bool isPlayerOne = true;
    public GameObject coinFlip;
    public GameObject selection;

    public void Select(int id)
    {
        GameObject c = Instantiate(characters[id]);

        if(isPlayerOne) { c.gameObject.tag = "playerOne"; isPlayerOne = false; GetComponent<Button>().interactable = false; 
            GetComponentInChildren<TextMeshProUGUI>().color = new Color(GetComponentInChildren<TextMeshProUGUI>().color.r, 
            GetComponentInChildren<TextMeshProUGUI>().color.g, GetComponentInChildren<TextMeshProUGUI>().color.b, 150f);
            selection.GetComponentInChildren<TextMeshProUGUI>().text = "Player 2 Select your Character"; }
        else { c.gameObject.tag = "playerTwo"; coinFlip.SetActive(true); coinFlip.GetComponent<CoinFlip>().StartCoinFlip(); selection.SetActive(false); isPlayerOne = true;}
    }
}
