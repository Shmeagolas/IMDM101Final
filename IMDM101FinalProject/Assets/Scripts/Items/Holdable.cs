using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable {

	internal float speed, damage;
	internal Items.Item name;
	internal MeshFilter mesh;
	public virtual void Use() { }
	public MeshFilter GetMesh() {
		return mesh;
	}

}
