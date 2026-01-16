using System.Collections;
using UnityEngine;
using System;

/// <summary>
/// Smoothly scales a GameObject while compensating its vertical position
/// to prevent terrain clipping or physics instability.
/// </summary>
// [DisallowMultipleComponent]
public sealed class ScaleWithVerticalCompensation : Ability
{
    #region Inspector Configuration

    [Header("Scaling")]
    [Tooltip("Target scale by this multiplier.")]
    [SerializeField] private float _multiplier = 1f;

    [Tooltip("Target scale to apply.")]
    [SerializeField] private Vector3 targetScale = Vector3.one * 2f;


    [Tooltip("Duration of the scaling animation in seconds.")]
    [SerializeField] private float scaleDuration = 0.5f;

    [Header("Vertical Compensation")]
    [Tooltip("Multiplier used to determine how much vertical lift is applied relative to scale change.")]
    [SerializeField] private float verticalCompensationMultiplier = 0.5f;

    [Tooltip("Optional additional vertical offset.")]
    [SerializeField] private float extraVerticalOffset = 0f;

    #endregion

    #region Private State

    private Coroutine activeRoutine;

    [Header("Mass Settings")]
    [SerializeField] bool changeMass = true;
    private Rigidbody cachedRigidbody;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {

        if (GetComponent<Rigidbody>())
        {
            // Cache Rigidbody once for performance (if present)
            cachedRigidbody = GetComponent<Rigidbody>();
            // Debug.LogError("No Rigidbody found on this GameObject!");
            // return;
        }
    }

    #endregion

    #region Public API

    /// <summary>
    /// Initiates a smooth scale transition with vertical compensation.
    /// </summary>
    // [ContextMenu("Apply Scale")]
    public void ApplyScale()
    {
        // Prevent multiple scaling operations from overlapping
        if (activeRoutine != null)
        {
            StopCoroutine(activeRoutine);
        }

        activeRoutine = StartCoroutine(ScaleRoutine());
    }

    public void ChangeMass(float newMass)
    {
        if (cachedRigidbody != null)
        {
            cachedRigidbody.mass = newMass;
            Debug.Log($"Changed mass to: {newMass} kg");
        }
    }

    public void CalculateNewMass(float scaleMultiplier)
    {
        //base * scaleMultiplier and POWER(3)
        float newMass = (float)Math.Pow(cachedRigidbody.mass * scaleMultiplier, 3); // 2 в степени 3

        ChangeMass(newMass);
    }

    public void ChangeScale(float multiplier)
    {
        _multiplier = multiplier;
        targetScale *= _multiplier;
        ApplyScale();
        if (changeMass && cachedRigidbody != null) CalculateNewMass(multiplier);
    }

    [ContextMenu("ChangeScale")]
    public void ChangeScale()
    {
        ChangeScale(_multiplier);
    }



    #endregion

    #region Scaling Logic

    /// <summary>
    /// Handles the full scaling sequence including vertical adjustment.
    /// </summary>
    private IEnumerator ScaleRoutine()
    {
        Vector3 initialScale = transform.localScale;
        Vector3 initialPosition = transform.position;

        // Calculate how much the scale changes vertically
        float scaleDeltaY = targetScale.y - initialScale.y;

        // Determine vertical offset direction
        float verticalOffset = scaleDeltaY * verticalCompensationMultiplier + extraVerticalOffset;

        // If shrinking, move downward instead of upward
        Vector3 targetPosition = initialPosition + Vector3.up * verticalOffset;

        // Temporarily disable physics interpolation forces if applicable
        if (cachedRigidbody != null)
        {
            cachedRigidbody.isKinematic = true;
        }

        float elapsed = 0f;

        while (elapsed < scaleDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / scaleDuration);

            // Smooth interpolation for professional feel
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            // Apply scale
            transform.localScale = Vector3.Lerp(initialScale, targetScale, smoothT);

            // Apply vertical movement
            transform.position = Vector3.Lerp(initialPosition, targetPosition, smoothT);

            yield return null;
        }

        // Ensure final values are exact
        transform.localScale = targetScale;
        transform.position = targetPosition;

        // Restore physics behavior
        if (cachedRigidbody != null)
        {
            cachedRigidbody.isKinematic = false;
        }

        activeRoutine = null;
    }

    #endregion
}
