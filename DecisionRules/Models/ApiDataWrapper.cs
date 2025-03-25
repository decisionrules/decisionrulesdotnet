using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionRules.Models
{
    /// <summary>
    /// Simple data wrapper for correct DecisionRules input format
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ApiDataWrapper<T>
    {
        public T Data { get; set; }
    }
}
