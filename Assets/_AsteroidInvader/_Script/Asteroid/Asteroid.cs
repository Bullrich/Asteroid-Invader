using UnityEngine;
using System.Collections;
//By @JavierBullrich

public abstract class Asteroid : MonoBehaviour {
    public float speed = 5;
    [HideInInspector]
    public float movementSpeed;

    [HideInInspector]
    public Vector3 targetPos;
    [HideInInspector]
    public Vector3 startPos;
    [HideInInspector]
    public GameObject spawnPoint;
    public AudioClip destroy_sfx;

    int scoreToAdd = 1;

    private void TravelToTarget()
    {
        float step = movementSpeed * PlanetManager.gameTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }
    public void Spawn()
    {
        movementSpeed = speed;
        targetPos = PlanetManager.instance.getPlanetPosition();
        spawnPoint = PlanetManager.instance.GetRandomSpawnPoint();
        transform.position = spawnPoint.transform.position;
        if (transform.position != Vector3.zero)
            gameObject.SetActive(true);
    }
    /// <summary>Set the asteroid inactive and adds a point</summary>
    public virtual void Destroy(bool playSound = false)
    {
        gameObject.SetActive(false);
        PlanetManager.instance.eventAddScore.Invoke();
        scoreToAdd = 1;
        if (playSound)
            PlanetManager.instance.gameSound.RandomizeSfx(destroy_sfx);
    }
    /// <summary>Destroy the asteroid with a combo score</summary>
    public void ComboDestroy()
    {
        if (PlanetManager.instance.combo > 1)
            scoreToAdd = 1 * PlanetManager.instance.combo;

        Destroy(true);
    }

    public virtual void Update()
    {
        TravelToTarget();
    }
    public void ChangeSkin(Sprite newSkin)
    {
        SpriteRenderer sr = transform.Find("AsteroidSprite").GetComponent<SpriteRenderer>();
        
        sr.sprite = newSkin;
    }
    
    void OnEnable()
    {
        findOppositePoint();
    }


    private void findOppositePoint()
    {
        if (PlanetManager.instance != null)
            targetPos = PlanetManager.instance.getOppositeSpawnPoint(spawnPoint.name);
    }
}
