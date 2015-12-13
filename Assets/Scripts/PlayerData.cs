using UnityEngine;
using System.Collections;

public class PlayerData
{
	/// <summary>
	/// トリガーを引いた数
	/// </summary>
	public int TriggerCnt { get; set; }

	/// <summary>
	/// 振り返り時間
	/// </summary>
	public float LookBackTime { get; set; }

	/// <summary>
	/// 振り返ったかどうか
	/// </summary>
	public bool IsLookBacked { get; set; }
}
