//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(IsReference =true)]
    public partial class ZiChanBeiZhu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ZiChanBeiZhu()
        {
            this.info_Asset = new HashSet<info_Asset>();
        }
    
        [DataMember]
        public int ATypeId { get; set; }
        [DataMember]
        public string AName { get; set; }
        [DataMember]
        public string ABeiZhu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<info_Asset> info_Asset { get; set; }
    }
}
