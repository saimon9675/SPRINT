//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SPRINT
{
    using System;
    using System.Collections.Generic;
    
    public partial class Billing
    {
        public int BillingID { get; set; }
        public string Author { get; set; }
        public int Date { get; set; }
        public decimal Bill { get; set; }
    
        public virtual MonthConsumption MonthConsumption { get; set; }
        public virtual Cottager Cottager { get; set; }
    }
}