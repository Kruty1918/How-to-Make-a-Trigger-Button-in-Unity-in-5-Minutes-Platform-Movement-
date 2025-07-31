using UnityEngine;

/// <summary>
/// Представляє окрему частину, що відкривається, з визначеним pivot-ом і параметрами анімації повороту.
/// </summary>
[System.Serializable]
public class PivotPart
{
    /// <summary>
    /// Трансформ, який слугує точкою обертання (pivot) для цієї частини.
    /// </summary>
    public Transform pivot;

    /// <summary>
    /// Вісь обертання (нормалізований вектор), навколо якої відбувається відкривання.
    /// За замовчуванням - вісь Y (Vector3.up).
    /// </summary>
    public Vector3 rotationAxis = Vector3.up;

    /// <summary>
    /// Кут обертання в закритому стані (у градусах).
    /// </summary>
    public float closedAngle = 0f;

    /// <summary>
    /// Кут обертання в відкритому стані (у градусах).
    /// </summary>
    public float openedAngle = 90f;

    /// <summary>
    /// Тривалість анімації відкриття або закриття (у секундах).
    /// </summary>
    public float animationDuration = 1f;

    /// <summary>
    /// Кватерніон, що представляє ротацію у закритому стані.
    /// Ініціалізується під час запуску.
    /// </summary>
    [HideInInspector] public Quaternion closedRotation;

    /// <summary>
    /// Кватерніон, що представляє ротацію у відкритому стані.
    /// Ініціалізується під час запуску.
    /// </summary>
    [HideInInspector] public Quaternion openedRotation;

    /// <summary>
    /// Прогрес анімації від 0 (закрито) до 1 (відкрито).
    /// </summary>
    [HideInInspector] public float animationProgress = 0f;

    /// <summary>
    /// Чи зараз відбувається анімація (відкриття або закриття).
    /// </summary>
    [HideInInspector] public bool isAnimating = false;

    /// <summary>
    /// Якщо true — анімація відкриває, якщо false — закриває.
    /// </summary>
    [HideInInspector] public bool opening = false;
}