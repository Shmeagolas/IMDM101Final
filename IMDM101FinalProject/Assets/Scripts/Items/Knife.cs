using System.Threading.Tasks;
using UnityEngine;

public class Knife : Holdable
{
	private float swingSpeed, moveSpeed, damage;
	private bool canSwing;
	private int timeSwung;

	public Knife(float speed, float damage, float swingSpeed) {
		this.moveSpeed = speed;
		this.damage = damage;
		this.swingSpeed = swingSpeed;
		canSwing = true;
		timeSwung = 0;
	}
	public async void use() {
		if (canSwing) {
			timeSwung++;
			Debug.Log("Swing " + timeSwung);
			canSwing = false;
			await Wait(swingSpeed);
			canSwing = true;
		}
	}

	private async Task Wait(float seconds) {
		await Task.Delay(Mathf.FloorToInt(seconds * 1000));
	}
	

}
