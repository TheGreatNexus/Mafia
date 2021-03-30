﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class MenuManager : Manager<MenuManager>
{

    [Header("MenuManager")]

    #region Panels
    [Header("Panels")]
    [SerializeField] GameObject m_PanelMainMenu;
    [SerializeField] GameObject m_PanelInGameMenu;
    [SerializeField] GameObject m_PanelGameOver;

    List<GameObject> m_AllPanels;
    #endregion

    #region Manager implementation
    protected override IEnumerator InitCoroutine()
    {
        yield break;
    }
    #endregion

    #region Monobehaviour lifecycle
    protected override void Awake()
    {
        base.Awake();
        RegisterPanels();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            EscapeButtonHasBeenClicked();
        }
    }
    #endregion

    #region Panel Methods
    void RegisterPanels()
    {
        m_AllPanels = new List<GameObject>();
        if (m_PanelMainMenu) m_AllPanels.Add(m_PanelMainMenu);
        if (m_PanelInGameMenu) m_AllPanels.Add(m_PanelInGameMenu);
        if (m_PanelGameOver) m_AllPanels.Add(m_PanelGameOver);
    }

    void OpenPanel(GameObject panel)
    {
        foreach (var item in m_AllPanels)
            if (item) item.SetActive(item == panel);
    }
    #endregion

    #region UI OnClick Events
    public void EscapeButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new EscapeButtonClickedEvent());
    }

    public void PlayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PlayButtonClickedEvent());
    }

    public void ResumeButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new ResumeButtonClickedEvent());
    }

    public void MainMenuButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MainMenuButtonClickedEvent());
    }
    public void QuitButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new QuitButtonClickedEvent());
    }
    public void PlusButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PlusButtonClickedEvent());
    }
    public void MinusButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MinusButtonClickedEvent());
    }
    public void PlusGFButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PlusGFButtonClickedEvent());
    }
    public void MinusGFButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MinusGFButtonClickedEvent());
    }
    public void TakeDiamondsButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new TakeDiamondsButtonClickedEvent());
    }
    public void TokenButtonHasBeenClickedEvent(string token)
    {
        EventManager.Instance.Raise(new TokenButtonClickedEvent() { eToken = token });
    }
    public void DiamondsButtonHasBeenClickedEvent()
    {
        EventManager.Instance.Raise(new DiamondsButtonClickedEvent());
    }
    public void BackButtonHasBeenClickedEvent()
    {
        EventManager.Instance.Raise(new BackButtonClickedEvent());
    }
    public void TakeTokenButtonClickedEvent()
    {
        EventManager.Instance.Raise(new TakeTokenButtonClickedEvent());
    }
    public void PlusSDButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PlusSDButtonClickedEvent());
    }
    public void MinusSDButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MinusSDButtonClickedEvent());
    }
    public void StealDiamondsButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new StealDiamondsButtonClickedEvent());
    }
    public void PassButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PassButtonClickedEvent());
    }
    #endregion

    #region Callbacks to GameManager events
    protected override void GameMenu(GameMenuEvent e)
    {
        OpenPanel(m_PanelMainMenu);
    }

    protected override void GamePlay(GamePlayEvent e)
    {
        OpenPanel(null);
    }

    protected override void GameOver(GameOverEvent e)
    {
        OpenPanel(m_PanelGameOver);
    }

    protected override void GamePause(GamePauseEvent e)
    {
        OpenPanel(m_PanelInGameMenu);
    }

    protected override void GameResume(GameResumeEvent e)
    {
        OpenPanel(null);
    }

    protected override void GameQuit(GameQuitEvent e)
    {
        Application.Quit();
    }
    #endregion
}
