using Inspection.DbLayer;
using Inspection.ServiceLayer.EF.Interface;

namespace Inspection.ServiceLayer.EF.Service
{
  public class UnitofWorkRepo : IUnitOfWork
  {
    private readonly InspectionDbContext _dbContext;

    public UnitofWorkRepo(InspectionDbContext dbContext)
    {
      _dbContext = dbContext;
    }


    public InspectionDbContext dbContext
    {
      get { return _dbContext; }
    }


    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        _dbContext.Dispose();
      }
    }
  }
}
