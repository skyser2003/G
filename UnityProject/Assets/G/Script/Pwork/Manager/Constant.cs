using UnityEngine;
using System.Collections;

public class Constant{

	public static readonly string Scene_Town = "MainTownScene";
	public static readonly string Scene_Boss_Lich = "Boss_Lich_Map";
	public static readonly string Scene_Dungeon_Scene = "DungeonScene";

	public static readonly string Layer_Ground = "GroundPlatform";
	public static readonly string Layer_Side = "SideCollider";
	public static readonly string Layer_JumpCheck = "JumpCheckCollider";
	public static readonly string Layer_Player = "Player";
}

public enum GObjectGroup
{
	MY_PLAYER = 1,
	OPP_PLAYER = 2,
	ENEMY = 4,
	ENEMY_BOSS = 5,
	OBJECT = 6,
	TRAP = 7,
}
