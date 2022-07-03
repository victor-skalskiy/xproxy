using System;
namespace XProxy.DAL
{
    public class BaseEntity : IBaseEntity
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public BaseEntity() { }
    }
}

