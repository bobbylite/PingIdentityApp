namespace PingIdentityApp.Models;

public class ReservationForm
{
    /// <summary>
    /// User ID making the reservation.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Address for the reservation.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Date of the reservation.
    /// </summary>
    public DateTime ReservationDate { get; set; }

    /// <summary>
    /// User's date of birth in YYYY-MM-DD format.
    /// </summary>
    public string? DateOfBirth { get; set; }
}
