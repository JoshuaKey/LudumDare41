using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

   [SerializeField] private PlayerController player;

    public Slider healthSlider;
    public Slider xpSlider;
    public TextMeshProUGUI levelText;

	// Use this for initialization
	void Start () {
        player.health.OnHeathChanged += OnHealthChange;
        player.level.OnLevel += OnLevel;
        player.level.OnExpChanged += OnXPChanged;

        healthSlider.value = 1f;
        levelText.text = "" + 1;
        xpSlider.value = 0f;
    }
	
	public void OnHealthChange() {
        float t = player.health.CurrHP / player.health.MaxHP;
        healthSlider.value = t;
    }

    public void OnLevel() {
        levelText.text = "" + player.level.Level;
    }

    public void OnXPChanged() {
        float t = player.level.CurrExp / player.level.MaxExp;
        xpSlider.value = t;
    }
}
