using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls: MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based on input")][SerializeField] float controlSpeed = 30f;
    [Tooltip("How fast player moves in x position")][SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 7f;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float positionRollFactor = -2f;
    [Header("Player input controls")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -30;

    [Header("Lasers")]
    [SerializeField] GameObject[] lasers;

    float xThrow;
    float yThrow;

    void Start()
    {
 
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFire();
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;

        float newXPos = transform.localPosition.x + xOffset;
        float newYPos = transform.localPosition.y + yOffset;

        float xPosClamp = Mathf.Clamp(newXPos, -xRange, xRange);
        float yPosClamp = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(xPosClamp, yPosClamp, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float positonPitch = transform.localPosition.y * positionPitchFactor;
        float pitch = positonPitch + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = transform.localPosition.z * positionRollFactor + xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFire()
    {
        if (Input.GetButton("Fire1"))
        {
            ActivateLasers(true);
        }
        else
        {
            ActivateLasers(false);
        }
        
    }

    void ActivateLasers(bool isAcive)
    {
        // Turn on lasers
        foreach(GameObject laser in lasers)
        {
            laser.SetActive(true);
            ParticleSystem.EmissionModule emmissionModule = laser.GetComponent<ParticleSystem>().emission;
            emmissionModule.enabled = isAcive;
        }
    }

}
