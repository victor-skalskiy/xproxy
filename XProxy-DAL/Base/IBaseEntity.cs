using System;
namespace XProxy.DAL
{
	public interface IBaseEntity
	{
		long Id { get; set; }
		bool IsActive { get; set; }
	}
}

