using UnityEngine;
using System.Collections;
//By @JavierBullrich

public class Debris : MonoBehaviour
{

    private Vector3 direction;

    public float speed;

    void Update()
    {
        transform.Translate(direction * PlanetManager.gameTime * speed);
        //print(Camera.main.WorldToViewportPoint(transform.position));
        OOC(transform.position);
    }

    public void SpawnDebris(Vector3 spawnPos, Vector3 newDir, float movementSpeed)
    {
        transform.position = spawnPos;
        direction = newDir;
        gameObject.SetActive(true);
    }
    /// <summary>Detect if the position is outside of camera (OOC)</summary>
    /// <param name="place">Position to detect</param>
    private void OOC(Vector3 place)
    {
        Vector3 cameraPos = Camera.main.WorldToViewportPoint(place);
        float posX = cameraPos.x;
        float posY = cameraPos.y;
        if (posX > 1 || posX < 0 || posY > 1 || posY < 0)
            gameObject.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Asteroid>().ComboDestroy();

        if (other.tag == "Asteroid")
        {
            float astSpeed = other.GetComponent<Asteroid>().movementSpeed;

            AsteroidPool.instance.GetDebris().GetComponent<Debris>().SpawnDebris(other.transform.position,
                debrisDirection(transform.position, other.transform.position),
                astSpeed / 2);

            PlanetManager.instance.SumCombo();
        }
        gameObject.SetActive(false);
    }

    private Vector3 debrisDirection(Vector3 pointA, Vector3 pointB)
    {
        Vector3 pos = pointA;
        Vector3 dir = (pointB - pointA).normalized;
        //Debug.DrawLine(pos, pos + dir * 10, Color.red, Mathf.Infinity);
        return dir;
    }
}
