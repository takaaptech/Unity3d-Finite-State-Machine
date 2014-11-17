using UnityEngine;
using System.Collections;
using MonsterLove.StateMachine;

public class GodClass : StateMachineBehaviour
{
	public enum States
	{
		Init,
		Play,
		Win,
		Lose
	}

	public float health = 100;
	public float damage = 20;

	private float startHealth;

	private void Awake()
	{
		startHealth = health;

		stateMachine.Initialize<GodClass, States>(this);

		stateMachine.ChangeState(States.Init);
	}

	void OnGUI()
	{
		//Example of polling states
		var state = stateMachine.GetState();

		if (state == null) return;

		if(state.Equals(States.Play))
		{
			if(GUILayout.Button("Win"))
			{
				stateMachine.ChangeState(States.Win);
			}
		}
		if(state.Equals(States.Win) || state.Equals(States.Lose))
		{
			if(GUILayout.Button("Play Again"))
			{
				stateMachine.ChangeState(States.Init);
			}
		}
	}

	private IEnumerator Init_Enter()
	{
		Debug.Log("Inited");

		health = startHealth;

		yield return new WaitForSeconds(3);

		stateMachine.ChangeState(States.Play);
	}

	private void Init_Update()
	{
		Debug.Log("init loop");
	}


	private void Play_Enter()
	{
		Debug.Log("Playing");
	}

	private void Play_Update()
	{
		Debug.Log("Health Remaining " + health);

		health -= damage * Time.deltaTime;
	
		if(health < 0)
		{
			stateMachine.ChangeState(States.Lose);
		}

		
	}

	void Play_Exit()
	{

	}

	void Lose_Enter()
	{
		Debug.Log("Lost");
	}

	void Win_Enter()
	{
		Debug.Log("Won");
	}
}