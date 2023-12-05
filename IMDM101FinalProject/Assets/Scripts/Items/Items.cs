using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items 
{
	[SerializeField] private float
		knifeDamage,
		knifeSpeed,
		akDamage,
		akSpeed,
		deagDamage,
		deagSpeed;

	 public enum Item {
		KNIFE,
		AK,
		DEAGLE
	}

	private Item item;
	private Holdable current;
	private Gun backup;
	
	public Items(){
		item = Item.KNIFE;
		current = new Knife(knifeSpeed, knifeDamage, 1f);
	}

	public Item Held {
		get {
			return item;
		}
		set {
			item = value;

			if(item == Item.KNIFE) {
				if(current is Gun) {
					backup = (Gun)current;
				}
				current = new Knife(knifeSpeed, knifeDamage, 1f);
			}else if(item == Item.AK) {
				if(backup.getItemType() == Item.AK) {
					current = backup;
				} else {
					current = new Gun(Item.AK, akSpeed, akDamage, 1000f, .2f, 25f);
				}
				backup = null;

			} else if(item == Item.DEAGLE) {
				if (backup.getItemType() == Item.DEAGLE) {
					current = backup;
				} else {
					current = new Gun(Item.DEAGLE, deagSpeed, deagDamage, 1000f, .5f, 6f);
				}
				backup = null;
			}
		}
	}

	public void use() {
		current.use();
	}





}
