using UnityEngine;
using System.Collections;
//By @JavierBullrich

public class MoonBehavior : MonoBehaviour {

    GameObject planet;
    private Vector3 zAxis = new Vector3(0, 0, 1);
    private Vector3 minusZAxis = new Vector3(0, 0, -1);
    private Vector3 movementAxis;

    #region Movement Speed
    private float movementSpeed;
    public float regularSpeed = 30f;
    public float fastSpeed = 120f;
    bool clockSide = true;
    #endregion

    private Rigidbody2D rb;

    void Start () {
        planet = transform.parent.gameObject;
        movementAxis = zAxis;
        movementSpeed = regularSpeed;
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        transform.RotateAround(planet.transform.position, movementAxis, movementSpeed * PlanetManager.gameTime);
        if (PlanetManager.instance.Playing)
            playerControls();
        else
            StartGame();
    }

    void playerControls() {
        if (Input.GetMouseButtonDown(0)) {
            if (clockSide)
            {
                movementAxis = minusZAxis;
                clockSide = false;
            }
            else if (!clockSide)
            {
                movementAxis = zAxis;
                clockSide = true;
            }
        }
        if (Input.GetMouseButton(0))
        {
            movementSpeed = fastSpeed;
        } else if (Input.GetMouseButtonUp(0))
        {
            movementSpeed = regularSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Asteroid>().Destroy(true);

        if (other.tag == "Asteroid")
        {
            float astSpeed = other.GetComponent<Asteroid>().movementSpeed;

            AsteroidPool.instance.GetDebris().GetComponent<Debris>().SpawnDebris(other.transform.position,
                debrisDirection(transform.position, other.transform.position),
                astSpeed / 2);

            PlanetManager.instance.combo = 0;
        }
    }

    private Vector3 debrisDirection(Vector3 pointA, Vector3 pointB)
    {
        Vector3 pos = pointA;
        Vector3 dir = (pointB - pointA).normalized;
        return dir;
    }

    private void StartGame()
    {
        movementSpeed = regularSpeed;
        if (PlanetManager.instance._debugGame)
        {
            if (Input.GetMouseButtonDown(0))
                PlanetManager.instance.StartGame();
        }
    }
}
