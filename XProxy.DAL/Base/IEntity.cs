using System;
namespace XProxy.DAL
{
	public interface IEntity
	{
		long Id { get; set; }
		bool IsActive { get; set; }
		DateTime CreateDate { get; set; }
		DateTime? ModifyDate { get; set; }
	}
}

