﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetHealth(int health) {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void SetMaxHealth(int maxHealth) {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradient.Evaluate(1);
    }
}