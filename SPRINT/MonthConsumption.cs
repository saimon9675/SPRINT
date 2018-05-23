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
    
    public partial class MonthConsumption : IEquatable<MonthConsumption>, IComparable<MonthConsumption>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MonthConsumption()
        {
            this.Billing = new HashSet<Billing>();
        }
    
        public int MonthBillingID { get; set; }
        public int Date { get; set; }
        public decimal Consumption { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Billing> Billing { get; set; }

        public bool Equals(MonthConsumption other)
        {
            if (this.Date == other.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CompareTo(MonthConsumption other)
        {
            if (this.Date > other.Date)
            {
                return 1;
            }
            else if (this.Date < other.Date)
            {
                return -1;
            }
            else
            {
                return 0;
            }

        }
    }
}