using UnityEngine;
using System.Collections;
//By @JavierBullrich

public class DestroyerAsteroid : Asteroid {

    SpriteRenderer spr;
    private bool changedColor = false;
    private int life = 2;
    private Sprite normalSprite, damagedSprite;

    public void Awake()
    {
        spr = transform.Find("AsteroidSprite").GetComponent<SpriteRenderer>();
        normalSprite = spr.sprite;
    }

    public override void Update () {
        base.Update();
	}

    public void ChangeDestroyerSkin(Sprite normal, Sprite damaged)
    {
        normalSprite = normal;
        damagedSprite = damaged;
        if (life == 2)
            spr.sprite = normal;
        else
            spr.sprite = damaged;
    }

    public override void Destroy(bool sound = false)
    {
        DamageDestroyer();
        if (life == 0)
            base.Destroy(sound);
    }

    void DamageDestroyer(int damage = 1)
    {
        life -= damage;
        if(life == 1)
        {
            spr.sprite = damagedSprite;
        }
    }
    void OnEnable()
    {
        life = 2;
        if (PlanetManager.instance != null)
        {
            spr.sprite = PlanetManager.instance.getSkin().destroyerAsteroid[0];
        }
    }
}
