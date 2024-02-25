using Inspection.DbLayer.DBModel;
using Inspection.ServiceLayer.EF.Interface;
using Inspection.ServiceLayer.Interface;
using Inspection.ServiceLayer.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspection.ServiceLayer.Service
{
  internal class ClsInspectionSVc : IInspection, IDisposable
  {
    private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

    public IUnitOfWork _unitOfWork;
    public ClsInspectionSVc(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    ~ClsInspectionSVc()
    {
      Dispose(false);
    }

    public List<ClsInspectorBM> GetAllInspector()
    {
      try
      {
        List<ClsInspectorBM> lstInspector = new List<ClsInspectorBM>();

        lstInspector = _unitOfWork.dbContext.mstInspectors.Select(x => new ClsInspectorBM
        {
          InspectorId = x.InspectorId,
          InspectorName = x.InspectorName,
        }).ToList();

        return lstInspector;
      }
      catch (Exception ex)
      {
        throw;
      }
    }
       
    public List<ClsInspectionsBM> GetAllInspection()
    {
      try
      {
        List<ClsInspectionsBM> lstInspection = new List<ClsInspectionsBM>();

        lstInspection = _unitOfWork.dbContext.TxnInspections.Join(_unitOfWork.dbContext.mstInspectors,
                    i => i.InspectorId, j => j.InspectorId, (i, j) => new { i, j })
                  .Select(x => new ClsInspectionsBM
                  {
                    InspectionID = x.i.InspectionID,
                    InspectionDate = x.i.InspectionDate,
                    Customer = x.i.Customer,
                    Address = x.i.Address,
                    Observations = x.i.Observations,
                    Status = x.i.Status,
                    InspectorId = x.j.InspectorId,
                    InspectorName = x.j.InspectorName
                  }).ToList();

        return lstInspection;
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public List<ClsInspectionsBM> GetAllInspectionsByInspectorName(string InspectoName)
    {
      try
      {
        List<ClsInspectionsBM> lstInspection = new List<ClsInspectionsBM>();

        lstInspection = _unitOfWork.dbContext.TxnInspections.Join(_unitOfWork.dbContext.mstInspectors,
                    i => i.InspectorId, j => j.InspectorId, (i, j) => new { i, j })
                  .Where(x => x.j.InspectorName == InspectoName)
                  .Select(x => new ClsInspectionsBM
                  {
                    InspectionID = x.i.InspectionID,
                    InspectionDate = x.i.InspectionDate,
                    Customer = x.i.Customer,
                    Address = x.i.Address,
                    Observations = x.i.Observations,
                    Status = x.i.Status,
                    InspectorId = x.j.InspectorId,
                    InspectorName = x.j.InspectorName
                  }).ToList();

        return lstInspection;
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public List<ClsInspectionsBM> GetAllInspectionsByCondition(string ConditionValue)
    {
      try
      {
        List<ClsInspectionsBM> lstInspection = new List<ClsInspectionsBM>();

        lstInspection = _unitOfWork.dbContext.TxnInspections.Join(_unitOfWork.dbContext.mstInspectors,
                    i => i.InspectorId, j => j.InspectorId, (i, j) => new { i, j })
                  .Where(x => x.i.Customer == ConditionValue || x.i.Status == ConditionValue)
                  .Select(x => new ClsInspectionsBM
                  {
                    InspectionID = x.i.InspectionID,
                    InspectionDate = x.i.InspectionDate,
                    Customer = x.i.Customer,
                    Address = x.i.Address,
                    Observations = x.i.Observations,
                    Status = x.i.Status,
                    InspectorId = x.j.InspectorId,
                    InspectorName = x.j.InspectorName
                  }).ToList();

        return lstInspection;
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public bool CheckInspectionExistForPerDay(int CurrentInspectorId,DateTime CurrentInspectionDt,string CurrentStatus)
    {
      try
      {

        List<string> lststring = new List<string>
        {
          "new" , "in progress"
        };
        
        var GetAllInsepctionByInspectorAndStatus = _unitOfWork.dbContext.TxnInspections.Where(x => x.InspectorId == CurrentInspectorId
         && lststring.Contains(x.Status.ToLower())).ToList();
        bool Isinspectionexistforday = GetAllInsepctionByInspectorAndStatus.Where(x => x.InspectionDate.ToString("dd-MM-yyyy") == CurrentInspectionDt.ToString("dd-MM-yyyy") && x.Status == CurrentStatus).Any();

       
        return Isinspectionexistforday;


       // return true;
      }
      catch(Exception ex)
      {
        throw;
      }
    }

    public int InsertInspection(ClsInspectionsBM objInspection)
    {
      try
      {
        if (objInspection != null)
        {
          //var myInspectionDate = DateTime.ParseExact(objInspection.InspectionDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
          //DateTime myInspectionDate = DateTime.ParseExact(objInspection.InspectionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

          ClsInspection txnInsepction = new ClsInspection()
          {
            InspectionDate = objInspection.InspectionDate.Value,
            Customer = objInspection.Customer,
            Address = objInspection.Address,
            Observations = objInspection.Observations,
            Status = objInspection.Status,
            InspectorId = objInspection.InspectorId
          };

          _unitOfWork.dbContext.TxnInspections.Add(txnInsepction);

          return _unitOfWork.dbContext.SaveChanges();


        }
        return 0;
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public void UpdateInspection(ClsInspectionsBM objInspection)
    {
      try
      {
        var CurrentInspectionData = _unitOfWork.dbContext.TxnInspections.Where(x => x.InspectionID == objInspection.InspectionID).FirstOrDefault();
        //DateTime myInspectionDate = DateTime.ParseExact(objInspection.InspectionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        if (CurrentInspectionData != null)
        {
          CurrentInspectionData.InspectionDate = objInspection.InspectionDate.Value;
          CurrentInspectionData.Customer = objInspection.Customer;
          CurrentInspectionData.Address = objInspection.Address;
          CurrentInspectionData.Observations = objInspection.Observations;
          CurrentInspectionData.Status = objInspection.Status;
          CurrentInspectionData.InspectorId = objInspection.InspectorId;

          _unitOfWork.dbContext.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public void DeleteInspection(int InspectionId)
    {
      try
      {
        var CurrentInspectionData = _unitOfWork.dbContext.TxnInspections.Where(x => x.InspectionID == InspectionId).FirstOrDefault();

        if (CurrentInspectionData != null)
        {
          _unitOfWork.dbContext.TxnInspections.Remove(CurrentInspectionData);
          _unitOfWork.dbContext.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }





    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        // Console.WriteLine("This is the first call to Dispose. Necessary clean-up will be done!");

        if (disposing)
        {
          // TODO: dispose managed state (managed objects).
          // Console.WriteLine("Explicit call: Dispose is called by the user.");
        }
        else
        {
          // Console.WriteLine("Implicit call: Dispose is called through finalization.");
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // Console.WriteLine("Unmanaged resources are cleaned up here.");

        // TODO: set large fields to null.

        disposedValue = true;
      }
      else
      {
        // Console.WriteLine("Dispose is called more than one time. No need to clean up!");
      }
    }



    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      GC.SuppressFinalize(this);
    }


    #endregion
  }
}
