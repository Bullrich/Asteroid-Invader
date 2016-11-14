using UnityEngine;
using System.Collections;
//By @JavierBullrich

public class SpawnerScript : MonoBehaviour
{

    public enum Position
    {
        TopLeft1,
        TopLeft2,
        TopLeft3,
        TopRight1,
        TopRight2,
        TopRight3,
        BottomLeft1,
        BottomLeft2,
        BottomLeft3,
        BottomRight1,
        BottomRight2,
        BottomRight3
    }
    public Position startPosition;

    void Awake()
    {
        StartPosicion();
    }

    private void StartPosicion()
    {
        switch (startPosition)
        {
            case Position.TopLeft1:
                var TopLeft1 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1.1f, 0));
                transform.position = new Vector3(TopLeft1.x - 0.7f, TopLeft1.y, 0);
                break;
            case Position.TopLeft2:
                var TopLeft2 = Camera.main.ViewportToWorldPoint(new Vector3(0.16f, 1.1f, 0));
                transform.position = new Vector3(TopLeft2.x, TopLeft2.y, 0);
                break;
            case Position.TopLeft3:
                var TopLeft3 = Camera.main.ViewportToWorldPoint(new Vector3(0.4f, 1.1f, 0));
                transform.position = new Vector3(TopLeft3.x, TopLeft3.y, 0);
                break;
            case Position.TopRight1:
                var TopRight1 = Camera.main.ViewportToWorldPoint(new Vector3(0.6f, 1.1f, 0));
                transform.position = new Vector3(TopRight1.x, TopRight1.y, 0);
                break;
            case Position.TopRight2:
                var TopRight2 = Camera.main.ViewportToWorldPoint(new Vector3(0.86f, 1.1f, 0));
                transform.position = new Vector3(TopRight2.x, TopRight2.y, 0);
                break;
            case Position.TopRight3:
                var TopRight3 = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1.1f, 0));
                transform.position = new Vector3(TopRight3.x + 0.7f, TopRight3.y, 0);
                break;
            case Position.BottomLeft1:
                var BottomLeft1 = Camera.main.ViewportToWorldPoint(new Vector3(0f, -0.1f, 0));
                transform.position = new Vector3(BottomLeft1.x - 0.7f, BottomLeft1.y, 0);
                break;
            case Position.BottomLeft2:
                var BottomLeft2 = Camera.main.ViewportToWorldPoint(new Vector3(0.16f, -0.1f, 0));
                transform.position = new Vector3(BottomLeft2.x, BottomLeft2.y, 0);
                break;
            case Position.BottomLeft3:
                var BottomLeft3 = Camera.main.ViewportToWorldPoint(new Vector3(0.4f, -0.1f, 0));
                transform.position = new Vector3(BottomLeft3.x, BottomLeft3.y, 0);
                break;
            case Position.BottomRight1:
                var BottomRight1 = Camera.main.ViewportToWorldPoint(new Vector3(0.6f, -0.1f, 0));
                transform.position = new Vector3(BottomRight1.x, BottomRight1.y, 0);
                break;
            case Position.BottomRight2:
                var BottomRight2 = Camera.main.ViewportToWorldPoint(new Vector3(0.86f, -0.1f, 0));
                transform.position = new Vector3(BottomRight2.x, BottomRight2.y, 0);
                break;
            case Position.BottomRight3:
                var BottomRight3 = Camera.main.ViewportToWorldPoint(new Vector3(1f, -0.1f, 0));
                transform.position = new Vector3(BottomRight3.x + 0.7f, BottomRight3.y, 0);
                break;
        }
    }
}