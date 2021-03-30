using System;
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

    // GAME VARIABLES

    private int m_TotalPlayersNb; //The total of players in the game
    private int m_PlayerNb; //The player that is playing
    private Chest m_CurrentGameChest;
    private List<Player> m_PlayerList = new List<Player>();

    //CANVAS GAMEOBJECTS
    [SerializeField] GameObject GodFatherPanel;
    [SerializeField] GameObject PlayerTurnPanel;
    [SerializeField] GameObject ChestPanel;
    [SerializeField] GameObject DiamondsPanel;
    [SerializeField] GameObject TokenPanel;
    [SerializeField] GameObject LastPlayerPanel;
    [SerializeField] GameObject ChoicePannel;
    [SerializeField] GameObject[] chestItems;

    #region Events' subscription
    public override void SubscribeEvents()
    {
        base.SubscribeEvents();

        //MainMenuManager
        EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.AddListener<ResumeButtonClickedEvent>(ResumeButtonClicked);
        EventManager.Instance.AddListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
        EventManager.Instance.AddListener<QuitButtonClickedEvent>(QuitButtonClicked);
        //CanvasButtons
        EventManager.Instance.AddListener<PlusButtonClickedEvent>(PlusButtonClicked);
        EventManager.Instance.AddListener<PlusGFButtonClickedEvent>(PlusGFButtonClicked);
        EventManager.Instance.AddListener<MinusButtonClickedEvent>(MinusButtonClicked);
        EventManager.Instance.AddListener<MinusGFButtonClickedEvent>(MinusGFButtonClicked);
        EventManager.Instance.AddListener<TakeDiamondsButtonClickedEvent>(TakeDiamondsButtonClicked);
        //PopUpButtons
        EventManager.Instance.AddListener<TokenButtonClickedEvent>(TokenButton);
        EventManager.Instance.AddListener<TakeTokenButtonClickedEvent>(TakeTokenButton);
        EventManager.Instance.AddListener<DiamondsButtonClickedEvent>(DiamondsButton);
        EventManager.Instance.AddListener<BackButtonClickedEvent>(BackButton);
        EventManager.Instance.AddListener<MinusSDButtonClickedEvent>(MinusSDButtonClicked);
        EventManager.Instance.AddListener<PlusSDButtonClickedEvent>(PlusSDButtonClicked);
        EventManager.Instance.AddListener<StealDiamondsButtonClickedEvent>(StealDiamondsButtonClicked);
        EventManager.Instance.AddListener<PassButtonClickedEvent>(PassButtonClicked);
    }

    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();

        //MainMenuManager
        EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.RemoveListener<ResumeButtonClickedEvent>(ResumeButtonClicked);
        EventManager.Instance.RemoveListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
        EventManager.Instance.RemoveListener<QuitButtonClickedEvent>(QuitButtonClicked);
        //CanvasButtons
        EventManager.Instance.RemoveListener<PlusButtonClickedEvent>(PlusButtonClicked);
        EventManager.Instance.RemoveListener<PlusGFButtonClickedEvent>(PlusGFButtonClicked);
        EventManager.Instance.RemoveListener<MinusButtonClickedEvent>(MinusButtonClicked);
        EventManager.Instance.RemoveListener<MinusGFButtonClickedEvent>(MinusGFButtonClicked);
        EventManager.Instance.RemoveListener<TakeDiamondsButtonClickedEvent>(TakeDiamondsButtonClicked);
        //PopUpButtons
        EventManager.Instance.RemoveListener<TokenButtonClickedEvent>(TokenButton);
        EventManager.Instance.RemoveListener<TakeTokenButtonClickedEvent>(TakeTokenButton);
        EventManager.Instance.RemoveListener<DiamondsButtonClickedEvent>(DiamondsButton);
        EventManager.Instance.RemoveListener<BackButtonClickedEvent>(BackButton);
        EventManager.Instance.RemoveListener<MinusSDButtonClickedEvent>(MinusSDButtonClicked);
        EventManager.Instance.RemoveListener<PlusSDButtonClickedEvent>(PlusSDButtonClicked);
        EventManager.Instance.RemoveListener<StealDiamondsButtonClickedEvent>(StealDiamondsButtonClicked);
        EventManager.Instance.RemoveListener<PassButtonClickedEvent>(PassButtonClicked);


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
    private void PlusButtonClicked(PlusButtonClickedEvent e)
    {
        m_TotalPlayersNb = int.Parse(GameObject.Find("TotalPlayersNb").GetComponent<UnityEngine.UI.Text>().text);
        if (m_TotalPlayersNb < 12)
        {
            m_TotalPlayersNb += 1;
            GameObject.Find("TotalPlayersNb").GetComponent<UnityEngine.UI.Text>().text = m_TotalPlayersNb.ToString();
        }
    }
    private void MinusButtonClicked(MinusButtonClickedEvent e)
    {
        m_TotalPlayersNb = int.Parse(GameObject.Find("TotalPlayersNb").GetComponent<UnityEngine.UI.Text>().text);
        if (m_TotalPlayersNb > 6)
        {
            m_TotalPlayersNb -= 1;
            GameObject.Find("TotalPlayersNb").GetComponent<UnityEngine.UI.Text>().text = m_TotalPlayersNb.ToString();
        }
    }
    private void PlusGFButtonClicked(PlusGFButtonClickedEvent e)
    {
        int m_Diamonds = int.Parse(GameObject.Find("diamondsToTake").GetComponent<UnityEngine.UI.Text>().text);
        if (m_Diamonds < 5)
        {
            m_Diamonds += 1;
            GameObject.Find("diamondsToTake").GetComponent<UnityEngine.UI.Text>().text = m_Diamonds.ToString();
        }
    }
    private void MinusGFButtonClicked(MinusGFButtonClickedEvent e)
    {
        int m_Diamonds = int.Parse(GameObject.Find("diamondsToTake").GetComponent<UnityEngine.UI.Text>().text);
        if (m_Diamonds > 0)
        {
            m_Diamonds -= 1;
            GameObject.Find("diamondsToTake").GetComponent<UnityEngine.UI.Text>().text = m_Diamonds.ToString();
        }
    }
    private void TakeDiamondsButtonClicked(TakeDiamondsButtonClickedEvent e)
    {
        int m_Diamonds = int.Parse(GameObject.Find("diamondsToTake").GetComponent<UnityEngine.UI.Text>().text);
        m_CurrentGameChest.diamonds -= m_Diamonds;
        m_PlayerList[0].diamonds += m_Diamonds;
        endTurn();
    }
    private void PlusSDButtonClicked(PlusSDButtonClickedEvent e)
    {
        int m_Diamonds = int.Parse(GameObject.Find("diamondsToSteal").GetComponent<UnityEngine.UI.Text>().text);
        if (m_Diamonds < m_CurrentGameChest.diamonds)
        {
            m_Diamonds += 1;
            GameObject.Find("diamondsToSteal").GetComponent<UnityEngine.UI.Text>().text = m_Diamonds.ToString();
        }
    }
    private void MinusSDButtonClicked(MinusSDButtonClickedEvent e)
    {
        int m_Diamonds = int.Parse(GameObject.Find("diamondsToSteal").GetComponent<UnityEngine.UI.Text>().text);
        if (m_Diamonds > 1)
        {
            m_Diamonds -= 1;
            GameObject.Find("diamondsToSteal").GetComponent<UnityEngine.UI.Text>().text = m_Diamonds.ToString();
        }
    }
    private void StealDiamondsButtonClicked(StealDiamondsButtonClickedEvent e)
    {
        int m_Diamonds = int.Parse(GameObject.Find("diamondsToSteal").GetComponent<UnityEngine.UI.Text>().text);
        m_CurrentGameChest.diamonds -= m_Diamonds;
        m_PlayerList[m_PlayerNb - 1].diamonds += m_Diamonds;
        m_PlayerList[m_PlayerNb - 1].role = "Thief";
        GameObject.Find("diamondsToSteal").GetComponent<UnityEngine.UI.Text>().text = 1.ToString();
        endTurn();
    }
    private void DiamondsButton(DiamondsButtonClickedEvent e)
    {
        DiamondsPanel.SetActive(true);
    }
    private void TokenButton(TokenButtonClickedEvent e)
    {
        TokenPanel.SetActive(true);
        UnityEngine.UI.Text tokenChosen = GameObject.Find("tokenChosen").GetComponent<UnityEngine.UI.Text>();
        switch (e.eToken)
        {
            case "white":
                tokenChosen.text = "Loyal Henchman";
                tokenChosen.color = Color.white;
                break;
            case "green":
                tokenChosen.text = "Driver";
                tokenChosen.color = Color.green;
                break;
            case "blue":
                tokenChosen.text = "Agent";
                tokenChosen.color = Color.blue;
                break;
            default:
                break;
        }
    }
    private void TakeTokenButton(TakeTokenButtonClickedEvent e)
    {
        switch (GameObject.Find("tokenChosen").GetComponent<UnityEngine.UI.Text>().text)
        {
            case "Loyal Henchman":
                m_CurrentGameChest.loyalHenchmens -= 1;
                m_PlayerList[m_PlayerNb - 1].role = "Loyal Henchman";
                break;
            case "Driver":
                m_CurrentGameChest.drivers -= 1;
                m_PlayerList[m_PlayerNb - 1].role = "Driver";
                break;
            case "Agent":
                m_CurrentGameChest.agents -= 1;
                m_PlayerList[m_PlayerNb - 1].role = "Agent";
                break;
            default:
                break;
        }
        
        endTurn();
    }
    private void BackButton(BackButtonClickedEvent e)
    {
        DiamondsPanel.SetActive(false);
        TokenPanel.SetActive(false);
    }

    private void PassButtonClicked(PassButtonClickedEvent e){
        m_PlayerList[m_PlayerNb-1].role ="Street child";
        endTurn();
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
        initGame();
        EventManager.Instance.Raise(new GamePlayEvent());
        StartGame(m_CurrentGameChest, m_PlayerList);
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

    #region Game rounds
    private void StartGame(Chest currentChest, List<Player> playerList)
    {
        GodFatherTurn();
    }

    private void initGame()
    {
        m_TotalPlayersNb = int.Parse(GameObject.Find("TotalPlayersNb").GetComponent<UnityEngine.UI.Text>().text);
        switch (m_TotalPlayersNb)
        {
            case 6:
                m_CurrentGameChest = new Chest(1, 1, 1, 0);
                break;

            case 7:
                m_CurrentGameChest = new Chest(2, 1, 1, 0);
                break;

            case 8:
                m_CurrentGameChest = new Chest(3, 1, 1, 1);
                break;

            case 9:
                m_CurrentGameChest = new Chest(4, 1, 1, 1);
                break;

            case 10:
                m_CurrentGameChest = new Chest(4, 2, 1, 1);
                break;

            case 11:
                m_CurrentGameChest = new Chest(4, 2, 2, 2);
                break;

            case 12:
                m_CurrentGameChest = new Chest(5, 2, 2, 2);
                break;

            default:
                break;
        }
        m_PlayerList.Add(new Player("Player1", "GodFather"));
        for (int i = 2; i <= m_TotalPlayersNb; i++)
        {
            m_PlayerList.Add(new Player("Player" + i, "N/A"));
        }
        m_PlayerNb = 1;
    }

    private void GodFatherTurn()
    {
        //ChestPanel.SetActive(true);
        PlayerTurnPanel.SetActive(true);
        PlayerTurnPanel.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "GodFather";
        GodFatherPanel.SetActive(true);
    }

    private void PlayersTurn()
    {
        ChestPanel.SetActive(true);
        RefreshChestDisplay();
    }

    private void LastPlayerTurn()
    {
        ChestPanel.SetActive(false);
        LastPlayerPanel.SetActive(true);
        RefreshChestDisplay();
    }

    private void GodFathersReTurn(){
        LastPlayerPanel.SetActive(false);
    }

    private void endTurn()
    {   
        GodFatherPanel.SetActive(false);
        TokenPanel.SetActive(false);
        DiamondsPanel.SetActive(false);
        ChestPanel.SetActive(false);
        LastPlayerPanel.SetActive(false);
        Debug.Log("Name : "+m_PlayerList[m_PlayerNb-1].name+"\n Diamonds : "+m_PlayerList[m_PlayerNb-1].diamonds + "\n Role : "+ m_PlayerList[m_PlayerNb-1].role);
        if (m_PlayerNb < m_TotalPlayersNb - 1)
        {
            m_PlayerNb += 1;
            PlayerTurnPanel.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "Player" + m_PlayerNb;
            PlayersTurn();
        }
        else if (m_PlayerNb == m_TotalPlayersNb - 1)
        {
            m_PlayerNb += 1;
            PlayerTurnPanel.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "Player" + m_PlayerNb;
            LastPlayerTurn();
        }
        else if (m_PlayerNb == m_TotalPlayersNb)
        {
            PlayerTurnPanel.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "GodFather";
            GodFathersReTurn();
        }
    }
    #endregion

    private void RefreshChestDisplay()
    {
        if (m_CurrentGameChest.diamonds != 0)
        {
            GameObject.Find("diamondsNbTxt").GetComponent<UnityEngine.UI.Text>().text = "x" + m_CurrentGameChest.diamonds.ToString();
        }
        else
        {
            chestItems[0].SetActive(false);
        }
        if (m_CurrentGameChest.jokers != 0)
        {
            GameObject.Find("jokerNbTxt").GetComponent<UnityEngine.UI.Text>().text = "x" + m_CurrentGameChest.jokers.ToString();
        }
        else
        {
            chestItems[1].SetActive(false);
        }
        if (m_CurrentGameChest.loyalHenchmens != 0)
        {
            GameObject.Find("whiteTokenNbTxt").GetComponent<UnityEngine.UI.Text>().text = "x" + m_CurrentGameChest.loyalHenchmens.ToString();
        }
        else
        {
            chestItems[2].SetActive(false);
        }
        if (m_CurrentGameChest.agents != 0)
        {
            GameObject.Find("blueTokenNbTxt").GetComponent<UnityEngine.UI.Text>().text = "x" + m_CurrentGameChest.agents.ToString();
        }
        else
        {
            chestItems[4].SetActive(false);
        }
        if (m_CurrentGameChest.drivers != 0)
        {
            GameObject.Find("greenTokenNbTxt").GetComponent<UnityEngine.UI.Text>().text = "x" + m_CurrentGameChest.drivers.ToString();
        }
        else
        {
            chestItems[3].SetActive(false);
        }
    }

}
