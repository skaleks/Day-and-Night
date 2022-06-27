using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    private float speed;

    void Update()
    {
        speed = Random.Range(2f, 10f);

        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
