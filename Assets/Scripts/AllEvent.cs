using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

#region GameManager Events
public class GameMenuEvent : SDD.Events.Event
{
}
public class GamePlayEvent : SDD.Events.Event
{
}
public class GameOverEvent : SDD.Events.Event
{
}
public class GamePauseEvent : SDD.Events.Event
{
}
public class GameResumeEvent : SDD.Events.Event
{
}
public class GameQuitEvent : SDD.Events.Event
{
}
#endregion

#region MenuManager Events
public class EscapeButtonClickedEvent : SDD.Events.Event
{
}
public class PlayButtonClickedEvent : SDD.Events.Event
{
}
public class ResumeButtonClickedEvent : SDD.Events.Event
{
}
public class MainMenuButtonClickedEvent : SDD.Events.Event
{
}
public class QuitButtonClickedEvent : SDD.Events.Event
{
}
#endregion

#region HudManager Event
public class PlusButtonClickedEvent : SDD.Events.Event
{
}
public class MinusButtonClickedEvent : SDD.Events.Event
{
}
public class PlusGFButtonClickedEvent : SDD.Events.Event
{
}
public class MinusGFButtonClickedEvent : SDD.Events.Event
{
}
public class TakeDiamondsButtonClickedEvent : SDD.Events.Event
{
}
public class TokenButtonClickedEvent : SDD.Events.Event
{
    public string eToken { get; set; }
}
public class DiamondsButtonClickedEvent : SDD.Events.Event
{
}
public class BackButtonClickedEvent : SDD.Events.Event
{
}
public class TakeTokenButtonClickedEvent : SDD.Events.Event
{
}
public class PlusSDButtonClickedEvent : SDD.Events.Event
{
}
public class MinusSDButtonClickedEvent : SDD.Events.Event
{
}
public class StealDiamondsButtonClickedEvent : SDD.Events.Event
{
}
public class PassButtonClickedEvent : SDD.Events.Event
{
}
public class AccusateButtonClickedEvent : SDD.Events.Event
{
    public GameObject ePlayer { get; set; }
}
#endregion

