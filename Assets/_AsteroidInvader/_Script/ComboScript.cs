using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//By @JavierBullrich

public class ComboScript : MonoBehaviour {
    Text comboLabel;
    Animator animator;
    bool gameObjectOut;
    float animationTime = 1f;

	void Start () {
        comboLabel = GetComponent<Text>();
        animator = GetComponent<Animator>();
	}
	
	public void showScore(int combo)
    {
        comboLabel.text = "Combo: " + combo;
        if (gameObjectOut)
        {
            CancelInvoke();
            Invoke("callQuitAnim", animationTime);
        }
        else
            animator.Play("comboAnimEnter");
    }

    public void animationEnded()
    {
        Invoke("callQuitAnim", animationTime);
        gameObjectOut = true;
    }
    public void animationExit()
    {
        restartCombo();
        gameObjectOut = false;
    }

    public void callQuitAnim()
    {
        if (gameObjectOut)
        {
            animator.Play("comboAnimExit");
            gameObjectOut = false;
        }
    }

    private void restartCombo()
    {
        PlanetManager.instance.combo = 0;
        CancelInvoke();
    }

    public void setBoolOn()
    {
        gameObjectOut = true;
    }
}
