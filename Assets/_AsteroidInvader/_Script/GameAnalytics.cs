using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Analytics;
//By @JavierBullrich

public class GameAnalytics : MonoBehaviour {
	
	public static void BonusPerLife(int coins, int frozen, int boomer)
    {
        Analytics.CustomEvent("BonusPerLife", new Dictionary<string, object>
        {
            { "coins", coins },
            { "frozen", frozen },
            { "boomer", boomer }
        });
    }
}
