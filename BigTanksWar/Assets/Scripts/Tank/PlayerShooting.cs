using UnityEngine;
using System.Collections;
using Complete;

public class PlayerShooting : TankShooting {
	private bool isDown = false;
	private bool isUp = false;

	// Update is called once per frame
	void Update() {
		if (isDown) {
			touchFire();
		}
		if (isUp) {
			touchFire();
			isUp = false;
		}
	}

	public void touchDown() {
		isDown = true;
		isUp = false;
	}

	public void touchUp() {
		isDown = false;
		isUp = true;
	}

	private void touchFire() {
		// The slider should have a default value of the minimum launch force.
		//m_AimSlider.value = m_MinLaunchForce;
		// If the max force has been exceeded and the shell hasn't yet been launched...
		if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired) {
			// ... use the max force and launch the shell.
			m_CurrentLaunchForce = m_MaxLaunchForce;
			Fire();
		}

		// Otherwise, if the fire button has just started being pressed...
		else if (Input.GetMouseButtonDown(0)) {
			// ... reset the fired flag and reset the launch force.
			m_Fired = false;
			m_CurrentLaunchForce = m_MinLaunchForce;

			// Change the clip to the charging clip and start it playing.
			m_ShootingAudio.clip = m_ChargingClip;
			m_ShootingAudio.Play();

		}
		// Otherwise, if the fire button is being held and the shell hasn't been launched yet...
		else if (Input.GetMouseButton(0) && !m_Fired) {
			// Increment the launch force and update the slider.
			m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

			//m_AimSlider.value = m_CurrentLaunchForce;

		}
		// Otherwise, if the fire button is released and the shell hasn't been launched yet...
		else if (Input.GetMouseButtonUp(0) && !m_Fired) {
			// ... launch the shell.
			Fire();
		}
	}
}
