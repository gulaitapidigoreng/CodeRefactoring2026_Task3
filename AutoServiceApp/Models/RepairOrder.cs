namespace AutoServiceApp.Models;

public class RepairOrder : BaseEntity
{
    public const decimal DefaultUrgentFee = 500m;
    public string OrderNumber { get; init; } = "";
    public string CustomerId { get; init; } = "";
    public string CarId { get; init; } = "";

    [System.Text.Json.Serialization.JsonIgnore]
    public Customer? Customer { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public Car? Car { get; set; }

    public string ProblemDescription { get; set; } = "";

    public OrderStatus Status { get; private set; } = OrderStatus.New;
    public OrderType Type { get; init; } = OrderType.Standard;

    public bool NeedTaxi { get; set; }
    public decimal UrgentFee { get; set; } = DefaultUrgentFee;
    public string WarrantyNumber { get; set; } = "";
    public bool ApprovedByDealer { get; set; }

    public string AssignedMechanicId { get; private set; } = "";
    [System.Text.Json.Serialization.JsonIgnore]
    public Mechanic? AssignedMechanic { get; private set; }

    public DateTime AcceptedAt { get; init; } = DateTime.Now;
    public DateTime? CompletedAt { get; private set; }

    public PaymentType PaymentMethod { get; set; } = PaymentType.Cash;

    private readonly List<RepairWork> _works = new();
    public IReadOnlyCollection<RepairWork> Works => _works.AsReadOnly();

    private readonly List<string> _statusHistory = new();
    public IReadOnlyCollection<string> StatusHistory => _statusHistory.AsReadOnly();

    public List<string> UsedPartIds { get; set; } = new();

    public decimal CalculateTotalCost()
    {
        decimal total = _works.Sum(w => w.Cost);
        if (Type == OrderType.Urgent) total += UrgentFee;
        if (Type == OrderType.Warranty && ApprovedByDealer) total = 0;
        return total;
    }

    public void AddWork(RepairWork work)
    {
        _works.Add(work);
    }

    public void AssignMechanic(Mechanic mechanic)
    {
        AssignedMechanic = mechanic;
        AssignedMechanicId = mechanic.Id;
        UpdateStatus(OrderStatus.InProgress);
    }

    public void CompleteOrder()
    {
        CompletedAt = DateTime.Now;
        UpdateStatus(OrderStatus.Completed);
    }

    private void UpdateStatus(OrderStatus newStatus)
    {
        Status = newStatus;
        _statusHistory.Add($"{DateTime.Now}: Status changed to {newStatus}");
    }

    public override string ToString()
    {
        var client = Customer?.Name ?? CustomerId;
        var car = Car == null ? CarId : $"{Car.Make} {Car.Model}";
        return $"{OrderNumber} ({Type}): {client}, {car}, {Status}, Total: {CalculateTotalCost():C}";
    }
}