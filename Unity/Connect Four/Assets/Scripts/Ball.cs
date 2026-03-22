using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 6f;
    public Vector3 target;
    public GameObject charParticle;
    private bool isDone = false;

    public void SetTarget(Vector3 v)
    {
        target = v;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        if(transform.position == target && !isDone)
        { 
            GameObject g = Instantiate(charParticle, transform.position, quaternion.identity);
            g.transform.parent = this.transform;
            StartCoroutine(DestroyParticleSystem(0.3f));
            SoundManager.PlaySound(Sounds.BallInPlace);
            charParticle = g;
            isDone = true;
        }
    }

    IEnumerator DestroyParticleSystem(float duration)
    {
        yield return new WaitForSeconds(duration);
        charParticle.GetComponent<ParticleSystem>().Stop();
        //Destroy(charParticle);
    }
}
