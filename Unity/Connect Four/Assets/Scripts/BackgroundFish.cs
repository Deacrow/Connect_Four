using UnityEngine;

public class BackgroundFish : MonoBehaviour
{
    public float speed = 0.8f;
    public Vector3 target;

    void Start()
    {
        float f = Random.Range(-0.1f, 0.1f);
        transform.localScale = new Vector3(transform.localScale.x + f, transform.localScale.y + f, 0.2f);
        if(transform.position.x < 0) transform.rotation = Quaternion.Euler(0, -180, 0);
        target = new Vector3(-this.transform.position.x, this.transform.position.y);
        speed = Random.Range(speed-0.2f, speed+0.2f);
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        if(transform.position == target)
        { 
            Destroy(this.gameObject);
        }
    }
}
