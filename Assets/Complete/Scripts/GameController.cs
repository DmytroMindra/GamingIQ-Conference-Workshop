using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Complete
{
	public class GameController : MonoBehaviour
	{

		public int Coins;
		public Text ScoreTextField;

		public void AddCoins (int coins)
		{
			// Update coins
			Coins += coins;
			// Update text value
			ScoreTextField.text = Coins.ToString ();
		}
	

	}
}