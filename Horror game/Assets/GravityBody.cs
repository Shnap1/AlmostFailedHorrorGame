using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GravityBody : MonoBehaviour, IGravityBody
{
    [Header("Параметры для Rigidbody-версии")]
    [Tooltip("Если true и на объекте есть Rigidbody, мы будем применять силы через AddForce.")]
    public bool useRigidbody = true;

    [Header("Параметры для CharacterController-версии")]
    [Tooltip("Максимальная скорость падения (когда без гравитационных сфер).")]
    public float maxFallSpeed = 20f;

    [Tooltip("Приоритет обработки: если есть CharacterController, то он будет использоваться вместо Rigidbody.")]
    public bool preferCharacterController = true;

    // Список активных сфер, которые сейчас действуют на этот объект
    private readonly List<GravitySphere> activeSpheres = new List<GravitySphere>();

    // Кешируем компоненты
    private Rigidbody rb;
    private CharacterController controller;

    // Скорость объекта (только для CharacterController-движения)
    private Vector3 _velocity = Vector3.zero;

    // Горизонтальная скорость, если нужна
    private Vector3 _horizontalVelocity = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();

        // Если у нас нет ни того, ни другого, этот компонент теряет смысл.
        if (rb == null && controller == null)
        {
            Debug.LogWarning(
                $"[GravityBody] у {gameObject.name} нет ни Rigidbody, ни CharacterController. Снятие компонента.");
            Destroy(this);
        }
    }

    // Реализация IGravityBody
    public IReadOnlyList<GravitySphere> ActiveSpheres => activeSpheres;

    public void EnterGravitySphere(GravitySphere sphere)
    {
        if (!activeSpheres.Contains(sphere))
            activeSpheres.Add(sphere);

        // Если у нас Rigidbody и мы хотим, чтобы при вхождении в сферу отключалась глобальная гравитация:
        if (rb != null && useRigidbody)
        {
            rb.useGravity = false;
        }
    }

    public void ExitGravitySphere(GravitySphere sphere)
    {
        if (activeSpheres.Contains(sphere))
            activeSpheres.Remove(sphere);

        // Если вы вышли из последней сферы и используете Rigidbody,
        // возвращаем глобальную гравитацию Unity:
        if (activeSpheres.Count == 0 && rb != null && useRigidbody)
        {
            rb.useGravity = true;
        }
    }

    private void Update()
    {
        // Если у нас есть CharacterController, обрабатываем движение здесь:
        if (controller != null && (!useRigidbody || preferCharacterController))
        {
            ApplyGravityForces();
        }
    }

    private void FixedUpdate()
    {
        // Если есть Rigidbody и мы выбрали вариант 'useRigidbody', применяем силы там:
        if (rb != null && useRigidbody && !(preferCharacterController && controller != null))
        {
            // Здесь мы позволяем Unity физике вызывать FixedUpdate
            ApplyGravityForces();
        }
    }

    public void ApplyGravityForces()
    {
        // Если у нас есть хотя бы одна активная сфера → вычисляем суммарное ускорение
        if (activeSpheres.Count > 0)
        {
            Vector3 totalAccel = Vector3.zero;
            Vector3 currentPos = transform.position;

            foreach (var sphere in activeSpheres)
            {
                Vector3 dir = sphere.transform.position - currentPos;
                float dist = dir.magnitude;
                if (dist <= Mathf.Epsilon) continue;

                dir.Normalize();
                float r = Mathf.Max(dist, sphere.minDistance);

                float accelMag = sphere.useInverseSquare
                    ? sphere.gravityStrength / (r * r)
                    : Mathf.Lerp(sphere.gravityStrength, 0f, Mathf.Clamp01(dist / ((SphereCollider)sphere.GetComponent<Collider>()).radius));

                totalAccel += dir * accelMag;
            }// Теперь «раскладываем» применение этой силы в зависимости от контроллера:
            if (controller != null && (!useRigidbody || preferCharacterController))
            {
                // --- Версия для CharacterController ---
                // Горизонтальной составляющей нет (или её можно добавить через Input).
                // Оставляем _horizontalVelocity неизменным (или задаём снаружи).
                // Берём текущую vertical-скорость _velocity.y, добавляем g*dt 
                // (в данном случае g = totalAccel по направлению относительно мира).
                // Но поскольку в данном примере у нас нет управления «вверх/вниз», 
                // и мы хотим полагаться только на локальные силы:

                // Интегрируем скорость:
                _velocity += totalAccel * Time.deltaTime;

                // Ограничиваем «падение» при выходе из сфер (если они исчезнут)— 
                // тут не очень важно, так как _velocity.y постоянно заменяется, 
                // когда sphere-действие есть.
                controller.Move((_horizontalVelocity + new Vector3(0, _velocity.y, 0)) * Time.deltaTime);
            }
            else if (rb != null && useRigidbody)
            {
                // --- Версия для Rigidbody ---
                // Применяем ускорение: F = m * a; но используем ForceMode.Acceleration, 
                // чтобы Unity сама домножила на массу.
                rb.AddForce(totalAccel, ForceMode.Acceleration);
            }
        }
        else
        {
            // Никаких активных сфер → возвращаем глобальную гравитацию (если Rigidbody)
            if (rb != null && useRigidbody)
            {
                // в Enter/Exit глобальная гравитация уже возвращается, 
                // но на случай, если список очищен не через Exit (редкий кейс):
                if (!rb.useGravity)
                    rb.useGravity = true;
            }

            if (controller != null && (!useRigidbody || preferCharacterController))
            {
                // Если мы на земле, чуть «прижимаемся», 
                // чтобы CharacterController не «парил» над поверхностью.
                if (controller.isGrounded)
                {
                    _velocity.y = -0.1f;
                }
                else
                {
                    // Если мы просто «падаем» (нет сфер), падаем вниз с глобальным g=9.81:
                    _velocity += Physics.gravity * Time.deltaTime;
                    _velocity.y = Mathf.Max(_velocity.y, -maxFallSpeed);
                }

                controller.Move((_horizontalVelocity + new Vector3(0, _velocity.y, 0)) * Time.deltaTime);
            }
        }
    }
}

public interface IGravityBody
{
    /// <summary>
    /// Вызывается, когда этот объект вошёл в область действия конкретной GravitySphere.
    /// </summary>
    /// <param name="sphere">Источник гравитации.</param>
    void EnterGravitySphere(GravitySphere sphere);

    /// <summary>
    /// Вызывается, когда этот объект вышел из области действия конкретной GravitySphere.
    /// </summary>
    /// <param name="sphere">Источник гравитации.</param>
    void ExitGravitySphere(GravitySphere sphere);

    /// <summary>
    /// Должен возвращать список активных сфер, которые сейчас «тянут» этот объект.
    /// </summary>
    IReadOnlyList<GravitySphere> ActiveSpheres { get; }

    /// <summary>
    /// На каждом кадре (или на каждом FixedUpdate) нужно вычислить и применить силу гравитации 
    /// со всех активных сфер к этому объекту.
    /// </summary>
    void ApplyGravityForces();
}