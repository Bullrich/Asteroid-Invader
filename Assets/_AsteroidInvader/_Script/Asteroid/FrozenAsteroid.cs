using UnityEngine;
using System.Collections;
//By @JavierBullrich

public class FrozenAsteroid : Asteroid {

    private float distanceFromEndPoint;

    public override void Update()
    {
        base.Update();

        if (CalculateDistanceToEndPoint() < 0.5F)
            gameObject.SetActive(false);
    }

    private float CalculateDistanceToEndPoint()
    {
        return Vector3.Distance(transform.position, targetPos);
    }

    private void TravelToTarget()
    {
        float step = speed * PlanetManager.gameTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }
    public override void Destroy(bool sound = false)
    {
        base.Destroy(sound);
        InvokeEffect();
        AsteroidPool.instance.slowAsteroids();
        PlanetManager.instance.destroyedFrozen++;
    }

    void InvokeEffect()
    {
        GameObject effect = AsteroidPool.instance.getEffect();
        effect.transform.localPosition = transform.position;
        effect.gameObject.SetActive(true);
        effect.GetComponent<AsteroidEffect>().turnOn(false);
    }
}
