﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject addTowerWindow;
    public GameObject towerInfoWindow;
    public GameObject winGameWindow;
    public GameObject loseGameWindow;
    public GameObject blackBackground;
    public Text txtGold;
    public Text txtWave;
    public Text txtEscapedEnemies;
    public Transform enemyHealthBars;
    public GameObject enemyHealthBarPrefab;
}

void Awake()
{
    Instance = this;
}

private void UpdateTopBar()
{
    txtGold.text = GameManager.Instance.gold.ToString();
    txtWave.text = "Wave " + GameManager.Instance.waveNumber + " / " +     WaveManager.Instance.enemyWaves.Count;
    txtEscapedEnemies.text = "Escaped Enemies " +     GameManager.Instance.escapedEnemies + " / " +     GameManager.Instance.maxAllowedEscapedEnemies;
}

public void ShowAddTowerWindow(GameObject towerSlot)
{
    addTowerWindow.SetActive(true);   addTowerWindow.GetComponent<AddTowerWindow>().     towerSlotToAddTowerTo = towerSlot;
    UtilityMethods.MoveUiElementToWorldPosition(addTowerWindow.GetComponent<RectTransform>(), towerSlot.transform.position);
}

public void ShowWinScreen()
{
    blackBackground.SetActive(true);
    winGameWindow.SetActive(true);
}

public void ShowLoseScreen()
{
    blackBackground.SetActive(true);
    loseGameWindow.SetActive(true);
}

public void CreateHealthBarForEnemy(Enemy enemy)
{
    GameObject healthBar = Instantiate(enemyHealthBarPrefab);
    healthBar.transform.SetParent(enemyHealthBars, false);
    healthBar.GetComponent<EnemyHealthBar>().enemy = enemy;
}

// Update is called once per frame
void Update()
{
    UpdateTopBar();
}

public void ShowTowerInfoWindow(Tower tower)
{
    towerInfoWindow.GetComponent<TowerInfoWindow>().tower = tower; towerInfoWindow.SetActive(true);
    UtilityMethods.MoveUiElementToWorldPosition(towerInfoWindow.GetComponent<RectTransform>(), tower.transform.position);
}
