namespace AutoServiceApp.Models;

public enum OrderStatus
{
    New,
    InProgress,
    Completed,
    Cancelled
}

public enum PaymentType
{
    Cash,
    Card,
    BankTransfer
}

public enum OrderType
{
    Standard,
    Urgent,
    Warranty
}