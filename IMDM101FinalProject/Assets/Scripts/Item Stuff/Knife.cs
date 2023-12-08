using System.Threading.Tasks;
using UnityEngine;

public class Knife : Holdable {
	private float swingSpeed, distance;
	private bool canSwing;


	public Knife(float speed, float damage, float swingSpeed, Renderer[] mesh, Animator anim) {
		this.speed = speed;
		this.damage = damage;
		this.swingSpeed = swingSpeed;
		canSwing = true;
		distance = 3f;
		this.anim = anim;
		this.mesh = mesh;
	}
	public override async void UseAsync(Transform hit, float distance) {
		if (canSwing) {
			anim.SetTrigger("Swing");
			canSwing = false;
			await Wait(swingSpeed);
			canSwing = true;

			if(this.distance >= distance) {
				if(hit.gameObject.GetComponent<SlimeBehaviour>()) {
					hit.gameObject.GetComponent<SlimeBehaviour>().Damage(50);
				}
			}

		}
	}

	private async Task Wait(float seconds) {
		await Task.Delay(Mathf.FloorToInt(seconds * 1000f));
	}


}