using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Абстрактний базовий клас для об'єктів, які можуть відкриватися та закриватися за допомогою анімації повороту частин.
/// Реалізує логіку керування анімацією відкриття/закриття декількох <see cref="PivotPart"/>.
/// </summary>
public abstract class Openable : MonoBehaviour
{
    /// <summary>
    /// Визначає, чи буде об'єкт відкритим при старті (true — відкритий, false — закритий).
    /// </summary>
    [Header("Openable Base")]
    public bool startOpened = false;

    /// <summary>
    /// Поточний стан: відкрито (true) або закрито (false).
    /// </summary>
    protected bool isOpen = false;

    /// <summary>
    /// Має повертати список усіх частин <see cref="PivotPart"/>, які беруть участь у відкриванні/закриванні.
    /// Повинен бути реалізований у похідних класах.
    /// </summary>
    /// <returns>Список частин воріт чи дверей, що анімуватимуться.</returns>
    protected abstract List<PivotPart> GetPivotParts();

    /// <summary>
    /// Ініціює процес відкривання об'єкта.
    /// Якщо об'єкт вже відкритий, виклик ігнорується.
    /// </summary>
    public void Open()
    {
        if (isOpen) return;
        isOpen = true;
        StartAnimation(true);
    }

    /// <summary>
    /// Ініціює процес закривання об'єкта.
    /// Якщо об'єкт вже закритий, виклик ігнорується.
    /// </summary>
    public void Close()
    {
        if (!isOpen) return;
        isOpen = false;
        StartAnimation(false);
    }

    /// <summary>
    /// Запускає анімацію відкривання або закривання для всіх частин.
    /// Якщо частина вже анімувалась у протилежному напрямку, змінює напрямок.
    /// Інакше починає анімацію з початкового прогресу.
    /// </summary>
    /// <param name="opening">True для відкривання, false для закривання.</param>
    private void StartAnimation(bool opening)
    {
        foreach (var part in GetPivotParts())
        {
            if (part.isAnimating && part.opening != opening)
            {
                // Зміна напрямку анімації під час виконання
                part.opening = opening;
            }
            else if (!part.isAnimating)
            {
                // Початок нової анімації
                part.isAnimating = true;
                part.opening = opening;
                part.animationProgress = opening ? 0f : 1f;
            }
        }
        OnAnimationStart(opening);
    }

    /// <summary>
    /// Unity-метод, що викликається при старті об'єкта.
    /// Ініціалізує стани поворотів кожної частини та встановлює початковий стан (відкрито/закрито).
    /// </summary>
    protected virtual void Start()
    {
        foreach (var part in GetPivotParts())
        {
            if (part.pivot == null) continue;

            // Нормалізація осі обертання
            var axis = part.rotationAxis.normalized;

            // Розрахунок кватерніонів для закритого та відкритого станів
            part.closedRotation = Quaternion.AngleAxis(part.closedAngle, axis);
            part.openedRotation = Quaternion.AngleAxis(part.openedAngle, axis);

            // Встановлення початкового прогресу та локального повороту
            part.animationProgress = startOpened ? 1f : 0f;
            part.pivot.localRotation = Quaternion.Slerp(part.closedRotation, part.openedRotation, part.animationProgress);

            part.isAnimating = false;
            part.opening = startOpened;
        }
        isOpen = startOpened;
    }

    /// <summary>
    /// Unity-метод, що викликається кожен кадр.
    /// Оновлює анімацію повороту для кожної частини, якщо вона анімована.
    /// Коли анімація завершується, викликає <see cref="OnAnimationEnd"/>.
    /// </summary>
    protected virtual void Update()
    {
        bool anyAnimating = false;

        foreach (var part in GetPivotParts())
        {
            if (!part.isAnimating || part.pivot == null) continue;

            // Обчислення кроку прогресу анімації відповідно до тривалості
            float delta = Time.deltaTime / part.animationDuration;
            part.animationProgress += part.opening ? delta : -delta;

            // Обмеження прогресу в діапазоні [0, 1]
            part.animationProgress = Mathf.Clamp01(part.animationProgress);

            // Плавне інтерполювання прогресу для природнішої анімації
            float smoothT = Mathf.SmoothStep(0f, 1f, part.animationProgress);

            // Обчислення поточного локального повороту частини
            part.pivot.localRotation = Quaternion.Slerp(part.closedRotation, part.openedRotation, smoothT);

            // Визначення, чи анімація закінчилась
            if (part.animationProgress == 0f || part.animationProgress == 1f)
                part.isAnimating = false;
            else
                anyAnimating = true;
        }

        // Якщо жодна частина не анімується — викликаємо подію закінчення анімації
        if (!anyAnimating)
            OnAnimationEnd(isOpen);
    }

    /// <summary>
    /// Віртуальний метод, що викликається на початку анімації відкривання/закривання.
    /// Можна перевизначити у похідних класах для додаткової поведінки.
    /// </summary>
    /// <param name="opening">True — відкривання, false — закривання.</param>
    protected virtual void OnAnimationStart(bool opening) { }

    /// <summary>
    /// Віртуальний метод, що викликається після завершення анімації відкриття або закриття.
    /// Можна перевизначити у похідних класах для додаткової поведінки.
    /// </summary>
    /// <param name="opened">True — об'єкт відкритий, false — закритий.</param>
    protected virtual void OnAnimationEnd(bool opened) { }
}
