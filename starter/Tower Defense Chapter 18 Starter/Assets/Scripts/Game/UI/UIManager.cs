﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UIElements;

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
    public GameObject centerWindow;
    public GameObject damageCanvas;
    public static float vrUiScaleDivider = 12;

    void Awake()
    {
        Instance = this;
    }

    private void UpdateTopBar()
    {
        txtGold.text = GameManager.Instance.gold.ToString();
        txtWave.text = "Wave " + GameManager.Instance.waveNumber + " / " + WaveManager.Instance.enemyWaves.Count;
        txtEscapedEnemies.text = "Escaped Enemies " + GameManager.Instance.escapedEnemies + " / " + GameManager.Instance.maxAllowedEscapedEnemies;
    }

    public void ShowAddTowerWindow(GameObject towerSlot)
    {
        if (GameManager.Instance.gameOver)
        {
            return;
        }
        addTowerWindow.SetActive(true); addTowerWindow.GetComponent<AddTowerWindow>().towerSlotToAddTowerTo = towerSlot;
        UtilityMethods.MoveUiElementToWorldPosition(addTowerWindow.GetComponent<RectTransform>(), towerSlot.transform.position);
        UtilityMethods.MoveUiElementToWorldPosition(addTowerWindow.GetComponent<RectTransform>(), towerSlot.transform.position);
    }

    public void ShowWinScreen()
    {
        winGameWindow.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        loseGameWindow.SetActive(true);
    }

    public void CreateHealthBarForEnemy(Enemy enemy)
    {
        GameObject healthBar = Instantiate(enemyHealthBarPrefab);
        healthBar.transform.SetParent(enemyHealthBars, false);
        healthBar.GetComponent<EnemyHealthBar>().enemy = enemy;
    }

    private IEnumerator EnableAndDisableCenterWindow()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.4f);
            centerWindow.SetActive(true);
            yield return new WaitForSeconds(.4f);
            centerWindow.SetActive(false);
        }
    }

    public void ShowCenterWindow(string text)
    {
        centerWindow.transform.FindChild("TxtWave").GetComponent<Text>().
        text = text;
        StartCoroutine(EnableAndDisableCenterWindow());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTopBar();
    }

    public void ShowTowerInfoWindow(Tower tower)
    {
        if (GameManager.Instance.gameOver)
        {
            return;
        }
        towerInfoWindow.GetComponent<TowerInfoWindow>().tower = tower; towerInfoWindow.SetActive(true);
        UtilityMethods.MoveUiElementToWorldPosition(towerInfoWindow.GetComponent<RectTransform>(), tower.transform.position);
        UtilityMethods.MoveUiElementToWorldPosition(towerInfoWindow.GetComponent<RectTransform>(), tower.transform.position);
    }

    public void ShowDamage()
    {
        StartCoroutine(DoDamageAnimation());
    }
    private IEnumerator DoDamageAnimation()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.1f);
            damageCanvas.SetActive(true);
            yield return new WaitForSeconds(.1f);
            damageCanvas.SetActive(false);
        }
    }
}

