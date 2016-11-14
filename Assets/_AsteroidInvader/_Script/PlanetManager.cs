using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
//By @JavierBullrich

///<summary> -------------------------------------------------------------
///<para>*	Planet Manager class.</para>
///<para>*	@JavierBullrich | February, 2016</para>
///<para>*	This class contains the main game manager</para>
///------------------------------------------------------------- </summary>
public class PlanetManager : MonoBehaviour {
    /// <summary>The PlanetManager instance</summary>
    public static PlanetManager instance;
    private GameObject[] spawnPoints;
    private Vector3 planetPosition;
    [HideInInspector]
    public bool Playing = false;
    [HideInInspector]
    public UnityEvent GameEnded = new UnityEvent();
    [HideInInspector]
    public UnityEvent GameRestarted = new UnityEvent();

    public GlitchManager glManager;
    [HideInInspector]
    public AsteroidSounds gameSound;
    AudioDistortionFilter audDist;

    private float spawnInterval;

    CharacterObject planetSkin;

    #region Saved Variables
    public float timeSpawn = 1.3f;
    [Range(3, 13)]
    public int levelLimit;
    #endregion

    [Space(6)]

    [Header("Sprites to set")]
    public EarthBehavior _earth;
    SpriteRenderer _earthSprite;
    SpriteRenderer _moonSprite;
    SpriteRenderer _deathSprite;
    public MeshRenderer[] _backgroundPlane;
    [HideInInspector]
    public UnityEvent eventAddScore = new UnityEvent();
    public UnityEngine.UI.Text scoreText;

    #region Combo
    public ComboScript _comboScr;
    [HideInInspector]
    public int combo = 0;
    #endregion

    public bool _debugGame = false;
    public CharacterObject defaultCharObj;
    public CharacterObject glitchObj;
    int gameScore = 0;
    bool canGlitch = true;
    int glitchScore;
    public static float gameTime;

    #region Analytics variables
    [HideInInspector]
    public int destroyedAsteroid;
    [HideInInspector]
    public int destroyedCoin;
    [HideInInspector]
    public int destroyedBoomers;
    [HideInInspector]
    public int destroyedFrozen;
    #endregion

    public void Awake()
    {
        instance = this;
        GameEnded.AddListener(Die);
        _earthSprite = _earth.earthSprite;
        _moonSprite = _earth.moonSprite;
        _deathSprite = _earth.destroyedSprite;
        eventAddScore.AddListener(AddScore);
        glitchScore = PlayerPrefs.GetInt("glitch", 11);
    }

	void Start () {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawners");
        spawnInterval = timeSpawn;
        planetSkin = defaultCharObj;
        Invoke("SetDefaultSkin", 0.1f);
        gameSound = _earth.gameObject.GetComponent<AsteroidSounds>();
        audDist = _earth.gameObject.GetComponent<AudioDistortionFilter>();

    }

    void AddScore()
    {
        int score = 1;
        gameScore += score;
        if (gameScore % 6 == 0 && canGlitch)
        {
            if (UnityEngine.Random.Range(0, 3) == 2)
                glManager.HintGlithc(UnityEngine.Random.Range(0.01f, 0.1f), UnityEngine.Random.Range(0, 2));
        }
        else if ((glitchScore == gameScore ||combo == 3) && canGlitch)
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                GlitchGame();
                canGlitch = false;
            }
        scoreText.text = gameScore.ToString();
    }

    void SetDefaultSkin()
    {
        setSkin(planetSkin);
        gamePlaying = true;

        audDist.distortionLevel = 0;
    }

    public GameObject GetRandomSpawnPoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
    }
    public void setPlanetPosition(Vector3 pPosition)
    {
        planetPosition = pPosition;
    }
    public Vector3 getPlanetPosition()
    {
        return planetPosition;
    }

    private void ResetAnalyticsVariables()
    {
        destroyedAsteroid = 0;
        destroyedBoomers = 0;
        destroyedCoin = 0;
        destroyedFrozen = 0;
    }

    public void setSkin(CharacterObject skinToSet)
    {
        AsteroidPool.instance.ChangeAsteroidSkin(skinToSet.asteroid,
            skinToSet.coinAsteroid,
            skinToSet.frozenAsteroid,
            skinToSet.boomAsteroid,
            skinToSet.asteroidDebris,
            skinToSet.destroyerAsteroid);
        _earthSprite.sprite = skinToSet.planet;
        _deathSprite.sprite = skinToSet.deadPlanet;
        _moonSprite.sprite = skinToSet.moon;
        foreach (MeshRenderer bckgrSpr in _backgroundPlane)
        {
            bckgrSpr.material.mainTexture = skinToSet.background;
        }
    }

    // callbacks

    public void SpawnAsteroid()
    {
        if (AsteroidPool.instance.ActivePooledObjects() < levelLimit)
        {
            GameObject newAsteroid = AsteroidPool.instance.getNextAsteroid();
            newAsteroid.GetComponent<Asteroid>().Spawn();
        }
        NextSpawn();
    }

    void Update()
    {
        if (gamePlaying)
        gameTime = Time.deltaTime;
    }

    bool gamePlaying = true;
    void GlitchGame()
    {
        gameTime = 0;
        gamePlaying = false;
        glManager.StartTheGlitch(SetGlitch, SetDefaultSkin);
        audDist.distortionLevel = 1;
        PlayerPrefs.SetInt("glitch", glitchScore + 11);
    }

    public void SetGlitch()
    {
        setSkin(glitchObj);
    }
    
    public Vector3 getOppositeSpawnPoint(string spawnName)
    {
        string oppositeSpawn = "";
        switch (spawnName)
        {
            case "TopLeft1":
                oppositeSpawn = "BottomRight3";
                break;
            case "TopLeft2":
                oppositeSpawn = "BottomRight2";
                break;
            case "TopLeft3":
                oppositeSpawn = "BottomRight1";
                break;
            case "TopRight1":
                oppositeSpawn = "BottomLeft3";
                break;
            case "TopRight2":
                oppositeSpawn = "BottomLeft2";
                break;
            case "TopRight3":
                oppositeSpawn = "BottomLeft1";
                break;
            case "BottomLeft1":
                oppositeSpawn = "TopRight3";
                break;
            case "BottomLeft2":
                oppositeSpawn = "TopRight2";
                break;
            case "BottomLeft3":
                oppositeSpawn = "TopRight1";
                break;
            case "BottomRight1":
                oppositeSpawn = "TopLeft3";
                break;
            case "BottomRight2":
                oppositeSpawn = "TopLeft2";
                break;
            case "BottomRight3":
                oppositeSpawn = "TopLeft1";
                break;
        }
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].name == oppositeSpawn)
            {
                return spawnPoints[i].transform.position;
            }
        }
        return Vector3.zero;
    }

    public void Restart()
    {
        Playing = false;
        spawnInterval = timeSpawn;
        CancelInvoke();
        GameRestarted.Invoke();
        gameScore = 0;
        scoreText.text = "0";
    }

    public void Die()
    {
        Playing = false;
        CancelInvoke();
        AsteroidPool.instance.RestartPool();
        Invoke("GameFinished", 0.3f);
    }

    void GameFinished()
    {
        Restart();
    }

    void RestartTimer()
    {
        AsteroidPool.instance.RestartPool();
    }

    public CharacterObject getSkin()
    {
        return planetSkin;
    }

    public void StartGame()
    {
        Playing = true;
        SpawnAsteroid();
    }

    private void NextSpawn()
    {
        float spawnIntervalNow = UnityEngine.Random.Range(spawnInterval, spawnInterval + 0.2F);
        Invoke("SpawnAsteroid", spawnIntervalNow);
        if (spawnInterval > 0.4F)
            spawnInterval -= UnityEngine.Random.Range(0.01f, 0.1f);
    }

    public void reset(CharacterObject character)
    {
        setSkin(character);
        planetSkin = character;
        gamePlaying = true;
        Restart();
        ResetAnalyticsVariables();
    }

    public void play()
    {
        StartGame();
    }

    public void revive()
    {
        GameRestarted.Invoke();
        StartGame();
    }

    public void SumCombo()
    {
        combo++;
        _comboScr.showScore(combo);
    }
}
