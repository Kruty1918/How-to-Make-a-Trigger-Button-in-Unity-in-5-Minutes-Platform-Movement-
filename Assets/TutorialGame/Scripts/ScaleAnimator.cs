using System.Collections;
using UnityEngine;

/// <summary>
/// Компонент для анімації масштабування об'єкта з плавним переходом між двома масштабами з використанням кривої анімації.
/// </summary>
public class ScaleAnimator : MonoBehaviour
{
    [Header("Scale Animation Settings")]

    /// <summary>
    /// Початковий масштаб для анімації (з якого масштабу починається перехід).
    /// </summary>
    [SerializeField]
    private Vector3 fromScale = new Vector3(1.2f, 1.2f, 1.2f);

    /// <summary>
    /// Кінцевий масштаб для анімації (до якого масштабу відбувається перехід).
    /// </summary>
    [SerializeField]
    private Vector3 toScale = Vector3.one;

    /// <summary>
    /// Тривалість анімації в секундах.
    /// </summary>
    [SerializeField]
    private float duration = 0.5f;

    /// <summary>
    /// Крива анімації, що визначає плавність переходу масштабу (вхід/вихід).
    /// </summary>
    [SerializeField]
    private AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    /// <summary>
    /// Посилання на поточну корутину анімації, щоб можна було зупинити її перед запуском нової.
    /// </summary>
    private Coroutine _currentRoutine;

    /// <summary>
    /// Запускає анімацію масштабування об'єкта від значення <see cref="fromScale"/> до <see cref="toScale"/>.
    /// Якщо анімація вже виконується, поточна корутина буде зупинена перед стартом нової.
    /// </summary>
    public void PlayScaleDown()
    {
        if (_currentRoutine != null)
            StopCoroutine(_currentRoutine);

        _currentRoutine = StartCoroutine(AnimateScale(fromScale, toScale));
    }

    /// <summary>
    /// Запускає зворотну анімацію масштабування об'єкта від значення <see cref="toScale"/> до <see cref="fromScale"/>.
    /// Якщо анімація вже виконується, поточна корутина буде зупинена перед стартом нової.
    /// </summary>
    public void PlayScaleUp()
    {
        if (_currentRoutine != null)
            StopCoroutine(_currentRoutine);

        _currentRoutine = StartCoroutine(AnimateScale(toScale, fromScale));
    }

    /// <summary>
    /// Корутина, що поступово змінює масштаб об'єкта від стартового до кінцевого значення за задану тривалість із застосуванням кривої анімації.
    /// </summary>
    /// <param name="start">Початковий масштаб.</param>
    /// <param name="end">Кінцевий масштаб.</param>
    /// <returns>IEnumerator для виконання корутини.</returns>
    private IEnumerator AnimateScale(Vector3 start, Vector3 end)
    {
        // Встановити початковий масштаб об'єкта
        transform.localScale = start;

        float time = 0f;

        // Поступово збільшувати час анімації до тривалості
        while (time < duration)
        {
            // Обчислити відношення часу від 0 до 1
            float t = time / duration;

            // Оцінити криву анімації для плавності переходу
            float evaluated = curve.Evaluate(t);

            // Плавно інтерполювати масштаб між start та end відповідно до кривої
            transform.localScale = Vector3.LerpUnclamped(start, end, evaluated);

            // Збільшити час анімації на deltaTime
            time += Time.deltaTime;

            yield return null;
        }

        // Встановити остаточний масштаб після завершення анімації
        transform.localScale = end;

        // Обнулити посилання на корутину, оскільки анімація завершена
        _currentRoutine = null;
    }
}
