using UnityEngine;
using TMPro;

public class TrajectoryController : MonoBehaviour
{
	[SerializeField] Transform dotsParent = null;
	[SerializeField] Transform dotPrefab = null;
	[SerializeField] int dotsNumber = 0;
	[SerializeField] float dotSpacing = 0;		
	[SerializeField] [Range(0.01f, 0.3f)] float dotMinScale = 0;
	[SerializeField] [Range(0.01f, 0.3f)] float dotMaxScale = 0;

    public GameObject ball = null;

	private Transform[] dotsList;

	private void Start() {
		Hide();
		PrepareDots();
	}

	private void PrepareDots() {
		dotsList = new Transform[dotsNumber];
		dotPrefab.localScale = Vector3.one * dotMaxScale;

		float scale = dotMaxScale;
		float scaleFactor = scale / dotsNumber;

		for (int i = 0; i < dotsNumber; i++) {
			dotsList[i] = Instantiate(dotPrefab, null);
			dotsList[i].parent = dotsParent.transform;
			dotsList[i].gameObject.SetActive(true);

			dotsList[i].localScale = Vector3.one * scale;
			if (scale > dotMinScale) {
				scale -= scaleFactor;
			}
		}
	}
	public void UpdateDots(Vector3 ballPos, Vector3 forceApplied) {
		float timeStamp = dotSpacing;
		bool hideDot = false;
		foreach (Transform dot in dotsList) {
			Vector3 dotPos;
			dotPos.x = (ballPos.x + forceApplied.x * timeStamp);
			dotPos.y = transform.position.y;
			dotPos.z = (ballPos.z + forceApplied.z * timeStamp);
			
			dot.position = dotPos;
			dot.LookAt(Camera.main.transform);
			dot.gameObject.SetActive(!hideDot);
			
			timeStamp += dotSpacing;
		}
	}

	public void Show() {
		dotsParent.gameObject.SetActive(true);
	}

	public void Hide() {
		dotsParent.gameObject.SetActive(false);
	}
}