﻿namespace Barski_Lewandowski_Interfaces
{
    public interface IProducer
    {
        int Id { get; set; }
        string Name { get; set; }
        string Country { get; set; }
        string PhoneNumber { get; set; }
    }
}
