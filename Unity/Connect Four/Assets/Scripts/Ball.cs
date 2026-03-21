using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 4f;
    public Vector3 target;

    public void SetTarget(Vector3 v)
    {
        target = v;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }
}
