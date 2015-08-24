using UnityEngine;
using System.Collections;

public class UserDataManager 
{
	private static UserDataManager instance;
	public static UserDataManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new UserDataManager();
				instance.Initiate();
			}

			return instance;
		}
	}


	public int Coin;

	protected void Initiate()
	{
		Coin = 0;
	}
}