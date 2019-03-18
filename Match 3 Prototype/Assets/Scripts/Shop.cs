using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour {

	public Button AutoCorrect;
	public Image[] AutoCorectImages;
	public TextMeshProUGUI[] AutocorectTexts;
	public Button EmojiCrush;
	public Image[] EmojiCrushImages;
	public TextMeshProUGUI[] EmojiCrushTexts;

	public TextMeshProUGUI coinsDisplay;

	private float defaultColor = 1;
	private float disabledColor = 0.3f;

	private void Awake()
	{
		checkPrices();
	}

	public void buyAutoCorrect()
	{
		SavingManager.PersistantData.Coins -= 100;
		SavingManager.PersistantData.AutoCorrectsOwned += 1;
		checkPrices();
		SavingManager.Save();
	}

	public void buyEmojiCrush()
	{
		SavingManager.PersistantData.Coins -= 10;
		SavingManager.PersistantData.EmojiCrushOwned += 1;
		checkPrices();
		SavingManager.Save();
	}

	private void checkPrices()
	{
		coinsDisplay.text = "Gold: " + SavingManager.PersistantData.Coins;
		AutoCorrect.interactable = false;
		EmojiCrush.interactable = false;

		if(SavingManager.PersistantData.Coins >= 100)
		{
			AutoCorrect.interactable = true;
			EmojiCrush.interactable = true;		
		}
		else if(SavingManager.PersistantData.Coins >= 10)
		{
			AutoCorrect.interactable = false;
			EmojiCrush.interactable = true;
		}
		else
		{
			AutoCorrect.interactable = false;
			EmojiCrush.interactable = false;
		}

		AutoCorectImages = ChangeImage(AutoCorrect.interactable, AutoCorectImages);
		AutocorectTexts = ChangeText(AutoCorrect.interactable, AutocorectTexts);
		EmojiCrushImages = ChangeImage(EmojiCrush.interactable, EmojiCrushImages);
		EmojiCrushTexts = ChangeText(EmojiCrush.interactable, EmojiCrushTexts);
	}

	public TextMeshProUGUI[] ChangeText(bool disable, TextMeshProUGUI[] texts)
	{
		for(int i = 0; i < texts.Length; i++)
		{
			texts[i].color = assignColors(disable, texts[i].color);
		}
		return texts;
	}

	public Image[] ChangeImage(bool Enabled, Image[] images)
	{
		for(int i = 0; i < images.Length; i++)
		{
			images[i].color = assignColors(Enabled, images[i].color);
		}
		return images;
	}


	public Color assignColors(bool Enabled, Color c)
	{
		if(!Enabled)
		{
			c.a = disabledColor;
		}
		else{
			c.a = defaultColor;
		}
		return c;
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
