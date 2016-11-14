using UnityEngine;
using System.Collections;
//By @JavierBullrich

public class EarthBehavior : MonoBehaviour {

    public SpriteRenderer earthSprite;
    public SpriteRenderer moonSprite;
    public SpriteRenderer destroyedSprite;
    Animator anim;
    Color alphaWhite;
    bool destroyAnim;
    public AudioClip deathSound;

    void Awake () {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        PlanetManager.instance.setPlanetPosition(transform.position);
        PlanetManager.instance.GameRestarted.AddListener(Restart);
        Restart();
    }

    void Restart()
    {
        destroyedSprite.enabled = false;
        alphaWhite = new Color(1, 1, 1, 1);
        destroyAnim = false;
        earthSprite.color = alphaWhite;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Restart game
        print(other.tag);
        if (other.tag == "Asteroid" && PlanetManager.instance.Playing)
        {
            PlanetManager.instance.GameEnded.Invoke();
            DestroyAnim();
            Debug.LogError("lost with " + other.name + " " + other.transform.position);
        }
        //print("lost with " + other.name);
    }

    void DestroyAnim()
    {
        destroyAnim = true;
        GetComponent<Animator>().Play("destroyedEarth");
        InvokeEffect();
        GetComponent<AsteroidSounds>().PlaySingle(deathSound);
    }

    void InvokeEffect()
    {
        GameObject effect = AsteroidPool.instance.getEffect();
        effect.transform.localPosition = Vector2.zero;
        effect.gameObject.SetActive(true);
        effect.GetComponent<AsteroidEffect>().turnOn(true);
    }

    void Update()
    {
        if (destroyAnim)
        {
            /*destroyedSprite.enabled = true;
            float alphaTranslating = 1.8f * Time.deltaTime;
            alphaWhite.a -= alphaTranslating;
            earthSprite.color = alphaWhite;*/
        }
    }
}
