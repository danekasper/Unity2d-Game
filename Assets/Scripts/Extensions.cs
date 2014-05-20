﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Extensions {

	//gameobject extensions
	public static bool IsSubClassOf<T>(this GameObject o){
		MonoBehaviour[] list = o.GetComponents<MonoBehaviour>();
		foreach(MonoBehaviour mb in list)
			if (mb is T) return true;
		return false;
	}

	public static GameObject CloestToObject(this GameObject obj, object[] list){
		List<GameObject> l = new List<GameObject>();
		foreach(var o in list){
			if (o.GetType().IsSubclassOf(typeof(BaseObj))) 
				l.Add(((BaseObj)o).gameObject);
		}
		return obj.CloestToObject(l);
	}

	public static GameObject CloestToObject(this GameObject obj, List<GameObject> list){
		if (list.Count == 0) return null;
		float minDistance = 100000f;
		GameObject closest = new GameObject();
		foreach(var entity in list){
			var distance = Vector3.Distance(entity.transform.position, obj.transform.position);
			if (distance < minDistance){ minDistance = distance; closest = entity;}
		}
		return closest;
	}

	public static void LookAt2d(this Transform thisTransform, Transform lookAt){
		var dir = Camera.main.ScreenToWorldPoint(lookAt.position) - Camera.main.ScreenToWorldPoint(thisTransform.position);
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg -90;
		thisTransform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, angle));
	}
}