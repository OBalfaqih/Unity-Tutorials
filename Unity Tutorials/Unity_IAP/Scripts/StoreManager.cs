using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour {

	// To update the text that has your current points
	public Text diamonds_text;

	// This function gets called once a purchase is complete
	public void OnPurchaseCompleted(Product product){
		// Check if the product exists
		if(product != null){
			// Checking the product's id (Ex: com.example.diamonds.500)
			switch(product.definition.id){
			case "diamonds.500":
				print("You have successfully purchased 500 diamonds !");
				// Convert string to integer
				int current_diamonds = int.Parse(diamonds_text.text);
				// Converting back the integer to a string
				diamonds_text.text = (current_diamonds + 500).ToString();
				break;
			default:
				// If the id is not covered, then just print it is not there
				print("Sorry, this product is not defined :(");
				break;
			}
		}
	}
}
