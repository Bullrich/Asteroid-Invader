using UnityEngine;
using System.Collections;
//By @JavierBullrich

public class SimpleAsteroid : Asteroid
{
    public float fastSpeed = 8;
    SpriteRenderer spr;
    private bool changedColor = false;
    GameObject frozenSprite;
    [SerializeField]
    private Color frozenColor;

    public void Start()
    {
        spr = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        frozenSprite = this.gameObject.transform.GetChild(1).gameObject;
        frozenSprite.SetActive(false);
    }

    public override void Update()
    {
        base.Update();
    }
    private void DecideSpeed()
    {
        int rand = Random.Range(1, 5);
        if (rand < 4)
            movementSpeed = speed;
        else
            movementSpeed = fastSpeed;
    }
    void OnEnable()
    {
        DecideSpeed();
    }
    public override void Destroy(bool sound = false)
    {
        PlanetManager.instance.destroyedAsteroid++;
        base.Destroy(sound);
    }

    public void SlowTime()
    {
        movementSpeed /= 2;
        spr.color = frozenColor;
        changedColor = true;
        frozenSprite.SetActive(true);
    }

    void OnDisable()
    {
        if (changedColor)
        {
            spr.color = new Color(1, 1, 1, 1);
            changedColor = false;
            frozenSprite.SetActive(false);
        }
    }

    static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
    {
        return 2.0f * Vector2.Dot(inDirection, inNormal) * inNormal - inDirection;
    }
}
