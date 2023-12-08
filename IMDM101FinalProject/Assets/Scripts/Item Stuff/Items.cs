using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items {


	public enum Item {
		KNIFE,
		AK,
		DEAGLE
	}

	private Item item;
	private Holdable current;
	private Gun deag;
	private Gun aK;
	private Knife knife;
	private List<Renderer[]> models;
	private List<Animator> animators;

	public Items(List<Renderer[]> models, List<Animator> animators) {
		this.models = models;
		this.animators = animators;


		knife = new Knife(1f, 100f, 1f, models[0], animators[0]);
		deag = new Gun(Item.DEAGLE, .5f, 100f, 1000f, .5f, 6f, models[1], animators[1]);
		aK = new Gun(Item.AK, .2f, 30f, 1000f, .2f, 25f, models[2], animators[2]);
		Held = Item.KNIFE;
		foreach(Renderer[] item in models) {
			foreach(Renderer part in item) {
				part.enabled = false;
			}
		}
	}



	private Item Held {
		get {
			return item;
		}
		set {
			item = value;

			if (item == Item.KNIFE) {
				
				if (current != null) {
					foreach(Renderer rend in current.mesh) {
						rend.enabled = false;
					}
				}
				current = knife;
				foreach (Renderer rend in current.mesh) {
					rend.enabled = true;
				}
				current.mesh[3].enabled = false;
			} else if (item == Item.AK) {
				if (current != null) {
					foreach (Renderer rend in current.mesh) {
						rend.enabled = false;
					}
				}
				current = aK;
				foreach (Renderer rend in current.mesh) {
					rend.enabled = true;
				}
				foreach (Renderer rend in current.mesh) {
					rend.enabled = true;
				}

			} else if (item == Item.DEAGLE) {
				if (current != null) {
					foreach (Renderer rend in current.mesh) {
						rend.enabled = false;
					}
				}
				current = deag;
				foreach (Renderer rend in current.mesh) {
					rend.enabled = true;
				}
			}
		}
	}

	public void Use(Transform hit, float distance) {
		current.UseAsync(hit, distance);
	}


	public void setItem(Items.Item item) {
		Held = item;
	}

}

