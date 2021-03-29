﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SDD.Events;


public class HUDManager : Manager<HUDManager>
{
	[Header("HudManager")]
	[Header("Texts")]
	[SerializeField] private Text m_TxtNLives;
	//[SerializeField] private Text m_TxtScore;

	[SerializeField] private GameObject m_Life1;
	[SerializeField] private GameObject m_Life2;
	[SerializeField] private GameObject m_Life3;
	#region Manager implementation
	protected override IEnumerator InitCoroutine()
	{
		yield break;
	}
	#endregion


}
