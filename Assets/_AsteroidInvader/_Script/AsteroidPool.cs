using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//By @JavierBullrich

public class AsteroidPool : MonoBehaviour {

    public static AsteroidPool instance;


    #region Asteroids
    [Header("Asteroid")]
    public GameObject pooledObject;
    public int pooledAmount = 20;
    public bool willGrow = true;
    [SerializeField]
    private CoinAsteroid _prfbCoinAsteroid;
    [SerializeField]
    private FrozenAsteroid _prfbFrozenAsteroid;
    [SerializeField]
    private BoomAsteroid _prfbBoomAsteroid;
    [SerializeField]
    private AsteroidEffect _prfEffect;
    [SerializeField]
    private DestroyerAsteroid _prfbDestroyerAsteroid;
    [HideInInspector]
    public GameObject coinAsteroid;
    [HideInInspector]
    public GameObject frozenAsteroid;
    [HideInInspector]
    public GameObject boomAsteroid;
    [HideInInspector]
    public GameObject destroyerAsteroid;
    [HideInInspector]
    public List<GameObject> effect;
    #endregion

    [Header("Debris")]
    public GameObject debrisObject;
    public int debrisAmount = 4;
    public bool debrisGrow = true;
    List<GameObject> debrisPool;

    private int asteroidSpawned = 0;

    List<GameObject> pooledObjects;

	void Awake () {
        instance = this;
        CreatePool();
    }

    private void CreatePool()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
        coinAsteroid = (GameObject)Instantiate(_prfbCoinAsteroid.gameObject);
        coinAsteroid.SetActive(false);
        frozenAsteroid = (GameObject)Instantiate(_prfbFrozenAsteroid.gameObject);
        frozenAsteroid.SetActive(false);
        boomAsteroid = (GameObject)Instantiate(_prfbBoomAsteroid.gameObject);
        boomAsteroid.SetActive(false);
        destroyerAsteroid = (GameObject)Instantiate(_prfbDestroyerAsteroid.gameObject);
        destroyerAsteroid.SetActive(false);

        debrisPool = new List<GameObject>();
        for (int i = 0; i < debrisAmount; i++)
        {
            GameObject ovj = (GameObject)Instantiate(debrisObject);
            ovj.SetActive(false);
            debrisPool.Add(ovj);
        }
        effect = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            GameObject eff = (GameObject)Instantiate(_prfEffect.gameObject);
            eff.SetActive(false);
            effect.Add(eff);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }

    public GameObject getEffect()
    {
        for(int i = 0; i < effect.Count; i++)
        {
            if (!effect[i].activeInHierarchy)
                return effect[i];
        }
        {
            GameObject ovj = (GameObject)Instantiate(_prfEffect.gameObject);
            effect.Add(ovj);
            return ovj;
        }
    }

    public GameObject GetDebris()
    {
        for(int i = 0; i < debrisPool.Count; i++)
        {
            if (!debrisPool[i].activeInHierarchy)
                return debrisPool[i];
        }
        if (debrisGrow)
        {
            GameObject ovj = (GameObject)Instantiate(debrisObject);
            debrisPool.Add(ovj);
            return ovj;
        }
        return null;
    }

    public void RestartPool()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
                pooledObjects[i].SetActive(false);
        }
        if (coinAsteroid.activeInHierarchy)
            coinAsteroid.SetActive(false);
        if (frozenAsteroid.activeInHierarchy)
            frozenAsteroid.SetActive(false);
        if (destroyerAsteroid.activeInHierarchy)
            destroyerAsteroid.SetActive(false);
        if (boomAsteroid.activeInHierarchy)
            boomAsteroid.SetActive(false);

        asteroidSpawned = 0;

        for (int i = 0; i < debrisPool.Count; i++)
        {
            if (debrisPool[i].activeInHierarchy)
                debrisPool[i].SetActive(false);
        }

        #region Commented Restart Pool
        /*if(pooledObjects.Count > pooledAmount)
        {
            int amoutGrowed = pooledObjects.Count;
            for (int i = 0; i < amoutGrowed; i++)
            {
                Destroy(pooledObjects[i]);
            }
            pooledObjects.Clear();
            CreatePool();
        }*/
        #endregion
    }

    public int ActivePooledObjects()
    {
        int activeObjects = 0;
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                activeObjects++;
            }
        }
        return activeObjects;
    }

    public void slowAsteroids()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy && pooledObjects[i].GetComponent<SimpleAsteroid>() != null)
            {
                pooledObjects[i].GetComponent<SimpleAsteroid>().SlowTime();
            }
        }
    }
    public void destroyActiveAsteroids()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy && pooledObjects[i].GetComponent<SimpleAsteroid>() != null)
            {
                pooledObjects[i].GetComponent<SimpleAsteroid>().Destroy();
            }
        }
    }

    public GameObject getNextAsteroid()
    {
        asteroidSpawned++;
        if(asteroidSpawned < 3)
            return GetPooledObject();

        int j = Random.Range(0, 9);
        if (j == 6 && !coinAsteroid.activeInHierarchy)
            return coinAsteroid;
        else if (j == 5 && !frozenAsteroid.activeInHierarchy)
            return frozenAsteroid;
        else if (j == 7)
        {
            int jj = Random.Range(0, 2);
            if (jj == 1 && !boomAsteroid.activeInHierarchy)
                return boomAsteroid;
            else
                return GetPooledObject();
        }
        else if (j == 8 && !destroyerAsteroid.activeInHierarchy)
            return destroyerAsteroid;
        else
            return GetPooledObject();
    }

    public void ChangeAsteroidSkin(Sprite[] asteroidSprite, Sprite coinSprite, Sprite frozenSprite, Sprite boomSprite, Sprite debrisSprite, Sprite[] destroyerSkins)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            Sprite asSprite = asteroidSprite[Random.Range(0, asteroidSprite.Length)];
            pooledObjects[i].GetComponent<Asteroid>().ChangeSkin(asSprite);
        }
        coinAsteroid.GetComponent<Asteroid>().ChangeSkin(coinSprite);
        frozenAsteroid.GetComponent<Asteroid>().ChangeSkin(frozenSprite);
        boomAsteroid.GetComponent<Asteroid>().ChangeSkin(boomSprite);
        destroyerAsteroid.GetComponent<DestroyerAsteroid>().ChangeDestroyerSkin(destroyerSkins[0], destroyerSkins[1]);
        for (int i = 0; i < debrisPool.Count; i++)
        {
            debrisPool[i].GetComponentInChildren<SpriteRenderer>().sprite = debrisSprite;
        }
    }
}
