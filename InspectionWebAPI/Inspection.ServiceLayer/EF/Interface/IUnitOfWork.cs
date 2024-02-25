using Inspection.DbLayer;

namespace Inspection.ServiceLayer.EF.Interface
{
  public interface IUnitOfWork : IDisposable
  {
    InspectionDbContext dbContext { get; }

  }
}
