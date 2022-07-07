using System;
namespace XProxy.DAL
{
    public class BaseEntityEntity : IEntity
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? ModifyDate { get; set; }

        public BaseEntityEntity() { }
    }
}

