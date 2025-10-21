namespace PingIdentityApp.Models;

public class ReservationPayment
{
    /// <summary>
    /// The pickup location chosen by the user.
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// The device reserved for pickup.
    /// </summary>
    public string Device { get; set; } = string.Empty;

    /// <summary>
    /// The selected pickup time slot.
    /// </summary>
    public string TimeSlot { get; set; } = string.Empty;

    /// <summary>
    /// The reservation or transaction ID (optional, generated when reservation is created).
    /// </summary>
    public string? ReservationId { get; set; }

    /// <summary>
    /// Indicates whether the payment was completed successfully.
    /// </summary>
    public bool PaymentSuccessful { get; set; }

    /// <summary>
    /// The amount paid (mock value for demonstration or future integration).
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Timestamp when the reservation or payment was processed.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}