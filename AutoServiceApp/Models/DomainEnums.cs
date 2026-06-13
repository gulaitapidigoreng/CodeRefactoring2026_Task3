using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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