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
    
    public partial class Cottager : IEquatable<Cottager>, IComparable<Cottager>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cottager()
        {
            this.Billing = new HashSet<Billing>();
        }
    
        public int CottagerID { get; set; }
        public string Author { get; set; }
        public decimal Square { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Billing> Billing { get; set; }

        public bool Equals(Cottager other)
        {
            if (this.Author == other.Author && this.Square == other.Square)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CompareTo(Cottager other)
        {
            if (string.Compare(this.Author, other.Author) > 0)
            {
                return 1;
            }
            else if (string.Compare(this.Author, other.Author) < 0)
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
