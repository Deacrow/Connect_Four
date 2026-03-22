using System.Collections;
using TMPro;
using UnityEngine;

public class CoinFlip : MonoBehaviour
{
    SpriteRenderer sr;
    private Sprite[] sides = new Sprite[2];
    public int flipCount = 1;
    public GameObject framework;
    private float dur = 0.003f;
    private float startSize = 1.5f;
    public TextMeshProUGUI startText;

    public void StartCoinFlip()
    {
        sides[0] = GameObject.FindGameObjectWithTag("playerOne").GetComponent<Character>().charBall.GetComponent<SpriteRenderer>().sprite;
        sides[1] = GameObject.FindGameObjectWithTag("playerTwo").GetComponent<Character>().charBall.GetComponent<SpriteRenderer>().sprite;

        StartCoroutine(Flipping(dur, startSize, Random.Range(5, 10)));
        SoundManager.PlaySound(Sounds.CoinToss);
    }

    void Awake() 
    {
        sr = GetComponent<SpriteRenderer>();
        startSize = transform.localScale.x;
    }

    IEnumerator ShowResult(float duration)
    {
        yield return new WaitForSeconds(duration);
        framework.SetActive(true);
        startText.gameObject.SetActive(false);
        if(flipCount % 2 == 0)
        {
            framework.GetComponent<Framework>().playerOneTurn = false;
        }
        gameObject.SetActive(false);
    }

    IEnumerator Flipping(float duration, float size, int flipCounts)
    {
        while(flipCount < flipCounts){
            while(size > 0.1)
            {
                size = size - 0.07f;
                transform.localScale = new Vector3(startSize, size, startSize);
                yield return new WaitForSeconds(duration);
            }
            sr.sprite = sides[flipCount % 2];
            while( size < startSize-0.01f)
            {
                size = size + 0.07f;
                transform.localScale = new Vector3(startSize, size, size);
                yield return new WaitForSeconds(duration);
            }
            flipCount++;
        }
        StartCoroutine(ShowResult(1.5f));
        SoundManager.PlaySound(Sounds.CoinRes);
        startText.gameObject.SetActive(true);
        if(flipCount % 2 == 0) {startText.color = GameObject.FindGameObjectWithTag("playerTwo").GetComponent<Character>().charColor; startText.text = GameObject.FindGameObjectWithTag("playerTwo").GetComponent<Character>().charName + " Starts!"; }
        else {startText.color = GameObject.FindGameObjectWithTag("playerOne").GetComponent<Character>().charColor; startText.text = GameObject.FindGameObjectWithTag("playerOne").GetComponent<Character>().charName + " Starts!"; }
    }
}
