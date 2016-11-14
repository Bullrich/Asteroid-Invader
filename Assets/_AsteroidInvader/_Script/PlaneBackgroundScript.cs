using UnityEngine;
using System.Collections;
//By @JavierBullrich

public class PlaneBackgroundScript : MonoBehaviour {

    public float scrollSpeed;
    public float tileSizeZ;
    public bool secondTile = false;

    private Vector3 startPosition;


    void Start () {
        /*float height = Camera.main.orthographicSize * 2.0F;
        float width = height * Screen.width / Screen.height;
        transform.localScale = new Vector3(width, height, 0.1F);*/

        float height = (float)Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;
        transform.localScale = new Vector3(width / 10, 1.0f, height / 10);

        
        //tileSizeZ = tra
        if(secondTile)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - tileSizeZ, transform.position.z);
        }

        startPosition = transform.position;
    }
	
	void Update () {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.up * newPosition;
    }
}
