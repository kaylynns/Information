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
    
    public partial class info_User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public string UserRealName { get; set; }
        public string UserPhone { get; set; }
        public string UseroffcePhone { get; set; }
        public string UserKeShi { get; set; }
        public int UserJueSe { get; set; }
    
        public virtual info_Role info_Role { get; set; }
    }
}
