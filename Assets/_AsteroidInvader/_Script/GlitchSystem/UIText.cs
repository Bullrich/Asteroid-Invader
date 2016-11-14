using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// by @JavierBullrich

public class UIText : MonoBehaviour {
    public float letterPause = 0.1f;

    [SerializeField]
    string[] answers;
    [HideInInspector]
    public string message;
    Text textComp;

    // Use this for initialization
    void Start()
    {
        textComp = GetComponent<Text>();
        message = answers[Random.Range(0, answers.Length)];
        textComp.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            textComp.text += letter;
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }
}
