﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class InformationEntities : DbContext
    {
        public InformationEntities()
            : base("name=InformationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CaiGouXingShi> CaiGouXingShi { get; set; }
        public virtual DbSet<CaiGouYiJu> CaiGouYiJu { get; set; }
        public virtual DbSet<info_Asset> info_Asset { get; set; }
        public virtual DbSet<info_ComputerRoomVisit> info_ComputerRoomVisit { get; set; }
        public virtual DbSet<info_Detailed> info_Detailed { get; set; }
        public virtual DbSet<info_Equipment> info_Equipment { get; set; }
        public virtual DbSet<info_Equipments> info_Equipments { get; set; }
        public virtual DbSet<info_Maintenance> info_Maintenance { get; set; }
        public virtual DbSet<info_Meeting> info_Meeting { get; set; }
        public virtual DbSet<info_Offices> info_Offices { get; set; }
        public virtual DbSet<info_Permission> info_Permission { get; set; }
        public virtual DbSet<info_Question> info_Question { get; set; }
        public virtual DbSet<info_Registration> info_Registration { get; set; }
        public virtual DbSet<info_Role> info_Role { get; set; }
        public virtual DbSet<info_Software> info_Software { get; set; }
        public virtual DbSet<info_Technology> info_Technology { get; set; }
        public virtual DbSet<info_User> info_User { get; set; }
        public virtual DbSet<PiShiJieGuo> PiShiJieGuo { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<ZiChanBeiZhu> ZiChanBeiZhu { get; set; }
        public virtual DbSet<ZiChanZhuangTai> ZiChanZhuangTai { get; set; }
        public virtual DbSet<v_Asset> v_Asset { get; set; }
        public virtual DbSet<V_ComputerRoomVisit> V_ComputerRoomVisit { get; set; }
        public virtual DbSet<v_Detailed> v_Detailed { get; set; }
        public virtual DbSet<v_DetailedSelect> v_DetailedSelect { get; set; }
        public virtual DbSet<V_info_Equipment> V_info_Equipment { get; set; }
        public virtual DbSet<v_info_Registration> v_info_Registration { get; set; }
        public virtual DbSet<v_info_Technology> v_info_Technology { get; set; }
        public virtual DbSet<v_Maintenance> v_Maintenance { get; set; }
        public virtual DbSet<v_User> v_User { get; set; }
    }
}
