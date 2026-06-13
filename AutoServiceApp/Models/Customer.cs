namespace AutoServiceApp.Models;

public class Customer : BaseEntity, IExportable
{
    public string Name { get; set; } = "";
    public ContactInfo ContactDetails { get; set; } = new ContactInfo("", "", "");

    [System.Text.Json.Serialization.JsonIgnore]
    public List<Car> Cars { get; set; } = new();

    public PaymentType LastPaymentMethod { get; set; } = PaymentType.Cash;

    public string Export() => $"{Name};{ContactDetails.Phone};{ContactDetails.Email};{ContactDetails.Address}";

    public override string ToString() => string.IsNullOrWhiteSpace(ContactDetails.Phone) ? Name : $"{Name} ({ContactDetails.Phone})";
}