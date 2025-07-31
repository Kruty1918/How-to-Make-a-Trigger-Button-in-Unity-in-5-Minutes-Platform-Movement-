using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Компонент кнопки, яка реагує на входження та вихід об'єктів з певним тегом у тригерну зону.
/// Викликає події при натисканні (вході) та відпусканні (виході) кнопки.
/// </summary>
[RequireComponent(typeof(Collider))]
public class PhysixButton : MonoBehaviour
{
    /// <summary>
    /// Тег об'єкта, який може активувати кнопку при вході в тригер.
    /// За замовчуванням - "Player".
    /// </summary>
    [Tooltip("Тег об'єкта, що активує кнопку")]
    [SerializeField] private string targetTag = "Player";

    /// <summary>
    /// Подія, що викликається, коли об'єкт з тегом <see cref="targetTag"/> входить у тригер.
    /// </summary>
    [Tooltip("Подія, яка буде викликана при вході в тригер")]
    public UnityEvent onEnter;

    /// <summary>
    /// Подія, що викликається, коли об'єкт з тегом <see cref="targetTag"/> виходить із тригера.
    /// </summary>
    [Tooltip("Подія, яка буде викликана при виході з тригера")]
    public UnityEvent onExit;

    /// <summary>
    /// Локальний стан кнопки: true — кнопка натиснута (об'єкт знаходиться в зоні), false — відпущена.
    /// </summary>
    private bool _isPressed = false;

    /// <summary>
    /// Метод викликається Unity, коли інший колайдер заходить у тригерну зону цього об'єкта.
    /// Якщо тег колайдера відповідає <see cref="targetTag"/>, виконує подію натискання.
    /// </summary>
    /// <param name="other">Колайдер об'єкта, що увійшов у тригер.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Ігноруємо колайдери з тегами, які не відповідають targetTag
        if (!other.CompareTag(targetTag)) return;

        // Викликаємо подію натискання (входу)
        onEnter?.Invoke();

        // Якщо кнопка вже натиснута, не змінюємо стан
        if (_isPressed) return;

        // Встановлюємо стан натиснутої кнопки
        _isPressed = true;
    }

    /// <summary>
    /// Метод викликається Unity, коли інший колайдер виходить із тригерної зони цього об'єкта.
    /// Якщо тег колайдера відповідає <see cref="targetTag"/>, виконує подію відпускання.
    /// </summary>
    /// <param name="other">Колайдер об'єкта, що вийшов із тригера.</param>
    private void OnTriggerExit(Collider other)
    {
        // Ігноруємо колайдери з тегами, які не відповідають targetTag
        if (!other.CompareTag(targetTag)) return;

        // Встановлюємо стан відпущеної кнопки
        _isPressed = false;

        // Викликаємо подію відпускання (виходу)
        onExit?.Invoke();
    }
}
