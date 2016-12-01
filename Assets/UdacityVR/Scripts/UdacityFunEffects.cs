using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UdacityFunEffects : MonoBehaviour {

    public const float SUN_ROTATION_SPEED = 40f;
    private Vector3 SUN_UP = new Vector3(70f, 0f, 0f); //vectors cannot be consts :(
    private Vector3 SUN_DOWN = new Vector3(220f, 0f, 0f);

    public Light sunLight;
    public Light flashLight;

    public GameObject halloween;
    public GameObject christmas;

    // Buttons
    public Button flashLightButton;
    public Button switchHolidayButton;
 
    private bool isSunUp = true;

    // Use this for initialization
    void Start () {
    	SetChristmasButtons();
	}

    public void ToggleSun()
    {
        StopAllCoroutines();
        isSunUp = !isSunUp;
        SwitchHoliday();
        StartCoroutine(AnimateSun(isSunUp));
    }

    public void SwitchHoliday ()
	{
		if (isSunUp) {
			halloween.SetActive (false);
			// Turns off flashlight if it's on.
			if (flashLight.gameObject.activeInHierarchy) {
				flashLight.gameObject.SetActive(false);
			}

			christmas.SetActive(true);
			SetChristmasButtons();
		} else {
			christmas.SetActive(false);

			halloween.SetActive(true);
			SetHalloweenButtons();
		}
    }

    /// <summary>
    ///  Displays button text that will be viewable when the sun is up and it's Christmas. 
    /// </summary>
	public void SetChristmasButtons() {
		flashLightButton.gameObject.SetActive(false);
		switchHolidayButton.GetComponentInChildren<Text>().text = "Done here? Let's see something else.";
	}


	/// <summary>
	/// Displays button text that will be viewable when the sun is down and it's Halloween.
	/// </summary>
	public void SetHalloweenButtons() {
		flashLightButton.gameObject.SetActive(true);
		flashLightButton.GetComponentInChildren<Text>().text = "Too scary? Maybe this will help.";

		switchHolidayButton.GetComponentInChildren<Text>().text = "Still too scary? Fine. Let's go back.";
	}

    public void ToggleFlashLight()
    {
        flashLight.gameObject.SetActive(!flashLight.gameObject.activeInHierarchy);
    }

    private IEnumerator AnimateSun(bool isSunUp)
    {
        Quaternion target = Quaternion.Euler(isSunUp ? SUN_UP : SUN_DOWN);
        float startAngle = Quaternion.Angle(sunLight.transform.rotation, target);
        while (true)
        {
            float currAngle = Quaternion.Angle(sunLight.transform.rotation, target);
            if (currAngle > 0.5f) {
                sunLight.transform.Rotate(new Vector3(SUN_ROTATION_SPEED * Time.deltaTime, 0f, 0f));
                float intensity = currAngle / startAngle;
                if (isSunUp)
                    intensity = 1f - intensity;

                sunLight.intensity = intensity;

                yield return new WaitForEndOfFrame();
            } else {
                break;
            }
        }
    }
}
