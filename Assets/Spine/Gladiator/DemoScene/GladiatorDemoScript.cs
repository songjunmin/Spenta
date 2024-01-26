using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

public class GladiatorDemoScript : MonoBehaviour {

	public SkeletonAnimation skeletonAnimation;
	float buttonSize = 160f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		TrackEntry entry = skeletonAnimation.state.GetCurrent(0);
		string animName = (entry == null ? null : entry.Animation.Name);

		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal();
		int i = 1;
		foreach (Spine.Animation a in skeletonAnimation.SkeletonDataAsset.GetSkeletonData(true).Animations) {
			if (GUILayout.Button(a.Name, GUILayout.Width(buttonSize), GUILayout.Height(buttonSize * 0.25f))) {
				if (string.Compare(a.Name, animName) != 0) {
					// true : 반복 / false : 반복x 
					skeletonAnimation.state.SetAnimation(0, a.Name, true);
				}
			};
			if (i++ % 4 == 0) {
				// next string
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
			}
		}
		// next string
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();

		GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
		labelStyle.fontSize = 30;
		labelStyle.normal.textColor = Color.red;
		GUILayout.Label("CURRENT ANIMATION IS : " + animName, labelStyle, GUILayout.Width(buttonSize * 5), GUILayout.Height(buttonSize));

		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
   }


}
