using UnityEngine;
using System.Collections;
//By @JavierBullrich

public class AsteroidEffect : MonoBehaviour {
    public Sprite fireEffect, iceEffect;
    GameObject spriteEffect;
    SpriteRenderer spr;
    Color defaultColor, alphaColor;

    public float expansionTime = 0.3f, hideTime;
    Vector2 defaultSize, expansionSize;

    void Awake()
    {
        spriteEffect = this.gameObject.transform.GetChild(0).gameObject;
        spr = spriteEffect.GetComponent<SpriteRenderer>();
        defaultSize = spriteEffect.transform.localScale;
        defaultColor = spr.color;
        Activated();
    }

    void Activated()
    {
        alphaColor = defaultColor;
        expansionSize = defaultSize;
    }

    public void turnOn(bool isFireExplosion)
    {
        if (isFireExplosion)
            spr.sprite = fireEffect;
        else
            spr.sprite = iceEffect;
    }

    void expand()
    {
        float alphaTranslating = hideTime * Time.deltaTime;
        float expansion = expansionTime * Time.deltaTime;
        expansionSize.y += expansion;
        expansionSize.x += expansion;
        alphaColor.a -= alphaTranslating;
        spriteEffect.transform.localScale = expansionSize;
        spr.color = alphaColor;
    }

    void Deactivate()
    {
        Activated();
        gameObject.SetActive(false);
    }

    void Update()
    {
        expand();
        if (alphaColor.a <= 0)
            Deactivate();
    }
	
}
