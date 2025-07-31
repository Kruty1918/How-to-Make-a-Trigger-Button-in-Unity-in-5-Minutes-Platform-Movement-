using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Клас для керування розкриванням воріт, що складаються з двох частин (лівих і правих).
/// Наслідує функціонал від базового класу <see cref="Openable"/>, що реалізує відкривання.
/// </summary>
public class GateOpenable : Openable
{
    /// <summary>
    /// Ліва частина воріт, що обертається навколо свого pivot.
    /// </summary>
    [Header("Gate Pivots")]
    public PivotPart leftGate;

    /// <summary>
    /// Права частина воріт, що обертається навколо свого pivot.
    /// </summary>
    public PivotPart rightGate;

    /// <summary>
    /// Список частин воріт, які будуть відкриватись (лівий та правий pivots).
    /// Ініціалізується у методі <see cref="Start"/>.
    /// </summary>
    private List<PivotPart> parts = new List<PivotPart>();

    /// <summary>
    /// Unity-метод, який викликається під час старту об'єкта.
    /// Ініціалізує список <see cref="parts"/> двома частинами воріт та викликає базовий метод старту.
    /// </summary>
    protected override void Start()
    {
        // Ініціалізація списку частин воріт, які будуть анімовані під час відкривання
        parts = new List<PivotPart> { leftGate, rightGate };
        
        // Виклик базового Start() для збереження поведінки з класу Openable
        base.Start();
    }

    /// <summary>
    /// Переоприділений метод, який повертає список частин воріт, які будуть задіяні при відкриванні.
    /// Викликається базовим класом для отримання частин, що мають рухатися.
    /// </summary>
    /// <returns>Список об'єктів типу <see cref="PivotPart"/>, які відкривають ворота.</returns>
    protected override List<PivotPart> GetPivotParts()
    {
        return parts;
    }
}