﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;
using UnityEngine.SceneManagement;

public enum GameState { gameMenu, gamePlay, gamePause }

public class GameManager : Manager<GameManager>
{

	//Game State
	private GameState m_GameState;
	public bool IsPlaying { get { return m_GameState == GameState.gamePlay; } }

	// TIME SCALE
	private float m_TimeScale;
	public float TimeScale { get { return m_TimeScale; } }
	void SetTimeScale(float newTimeScale)
	{
		m_TimeScale = newTimeScale;
		Time.timeScale = m_TimeScale;
	}

	#region Events' subscription
	public override void SubscribeEvents()
	{
		base.SubscribeEvents();

		//MainMenuManager
		EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
		EventManager.Instance.AddListener<PlayerHasBeenHitEvent>(PlayerHasBeenHit);
		EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
		EventManager.Instance.AddListener<ResumeButtonClickedEvent>(ResumeButtonClicked);
		EventManager.Instance.AddListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
        EventManager.Instance.AddListener<QuitButtonClickedEvent>(QuitButtonClicked);
	}

	public override void UnsubscribeEvents()
	{
		base.UnsubscribeEvents();

		//MainMenuManager
		EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
		EventManager.Instance.RemoveListener<PlayerHasBeenHitEvent>(PlayerHasBeenHit);
		EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
		EventManager.Instance.RemoveListener<ResumeButtonClickedEvent>(ResumeButtonClicked);
		EventManager.Instance.RemoveListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
        EventManager.Instance.RemoveListener<QuitButtonClickedEvent>(QuitButtonClicked);
	}
	#endregion

	#region Manager implementation
	protected override IEnumerator InitCoroutine()
	{
		Menu();
		//EventManager.Instance.Raise(new GameStatisticsChangedEvent() { eBestScore = BestScore, eScore = 0, eNLives = 0, eNEnemiesLeftBeforeVictory = 0 });
		yield break;
	}
	#endregion

	#region Callbacks to Events issued by MenuManager
	private void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
	{
		Menu();
	}

	private void PlayButtonClicked(PlayButtonClickedEvent e)
	{
		Play();
	}

	private void ResumeButtonClicked(ResumeButtonClickedEvent e)
	{
		Resume();
	}

	private void EscapeButtonClicked(EscapeButtonClickedEvent e)
	{
		if (IsPlaying)
			Pause();
	}
    private void QuitButtonClicked(QuitButtonClickedEvent e)
    {
        Quit();
    }

	#endregion

	//EVENTS
	private void Menu()
	{
		SetTimeScale(0);
		m_GameState = GameState.gameMenu;
		EventManager.Instance.Raise(new GameMenuEvent());
	}
    private void Quit()
    {
        EventManager.Instance.Raise(new GameQuitEvent());
    }

	private void Play()
	{
        //InitNewGame();
		SetTimeScale(1);
		m_GameState = GameState.gamePlay;
        EventManager.Instance.Raise(new GamePlayEvent());
	}

	private void Pause()
	{
		SetTimeScale(0);
		m_GameState = GameState.gamePause;
		EventManager.Instance.Raise(new GamePauseEvent());
	}

	private void Resume()
	{
		SetTimeScale(1);
		m_GameState = GameState.gamePlay;
		EventManager.Instance.Raise(new GameResumeEvent());
	}

}
