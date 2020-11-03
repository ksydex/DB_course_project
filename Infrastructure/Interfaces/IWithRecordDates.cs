﻿using System;

namespace ContractAndProjectManager.Infrastructure.Interfaces
{
    public interface IWithRecordDates
    {
        public DateTime DateStart { get; set; }
        public DateTime DateDeadLine { get; }
        public DateTime DateEnd { get; set; }
    }
}