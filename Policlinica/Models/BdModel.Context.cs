﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Policlinica.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BdEntities : DbContext
    {
        public BdEntities()
            : base("name=BdEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Diagnoz> Diagnoz { get; set; }
        public DbSet<Lechenie> Lechenie { get; set; }
        public DbSet<Otdelenie> Otdelenie { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Uvedomlenie> Uvedomlenie { get; set; }
        public DbSet<Vrem> Vrem { get; set; }
    }
}
