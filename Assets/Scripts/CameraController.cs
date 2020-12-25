using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public float zoomSpeed;
    public EnemySpawner enemySpawner;
    public Transform reset;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * speed, Space.World);
        transform.Translate(new Vector3(0, 0, wheel * zoomSpeed));

    }
    void SetUpPortal()
    {
        enemySpawner.SetUpPortal();
    }

    public void ResetPosition()
    {
        transform.position = reset.position;
        transform.rotation = reset.rotation;
    }
    public void StartShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }
    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition += new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
