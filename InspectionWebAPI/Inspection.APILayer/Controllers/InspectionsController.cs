using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inspection.APILayer.Controllers
{
  [Route("api/v1")]
  [ApiController]
  public class InspectionsController : ControllerBase
  {
    public readonly IInspection _IInspection;

    public InspectionsController(IInspection IInspection)
    {
      _IInspection = IInspection;
    }

    [HttpGet("GetAllInspector")]
    public IActionResult GetAllInspector()
    {
      try
      {
        List<ClsInspectorBM> lstInspector = _IInspection.GetAllInspector();


        return Ok(new { IsSuccess = true, Inspector = lstInspector });
      }
      catch (Exception ex)
      {
        throw;
      }
    }


    [HttpGet("GetAllInspection")]
    public IActionResult GetAllInspection()
    {
      try
      {
        List<ClsInspectionsBM> lstInspection = _IInspection.GetAllInspection();

        if(lstInspection.Count == 0)
        {
          return Ok(new { IsSuccess = false , InspectionData = lstInspection });
        }

        return Ok(new { IsSuccess = true, InspectionData = lstInspection });
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    [HttpGet("GetAllInspectionByInspector/{InspectorName}")]
    public IActionResult GetAllInspectionByInspector(string InspectorName)
    {
      try
      {
        List<ClsInspectionsBM> lstInspection = _IInspection.GetAllInspectionsByInspectorName(InspectorName);

        if(lstInspection.Count == 0)
        {
          return Ok(new { StatusCode = (int)System.Net.HttpStatusCode.BadRequest, IsSuccess = false, InspectionData = lstInspection });
        }
        

        return Ok(new { IsSuccess = true, InspectionData = lstInspection });
      }
      catch(Exception ex)
      {
        throw;
      }
    }

    [HttpGet("GetAllInspectionByCondition/{Condition}")]
    public IActionResult GetAllInspectionByCondition(string Condition)
    {
      try
      {
        List<ClsInspectionsBM> lstInspection = _IInspection.GetAllInspectionsByCondition(Condition);

        if (lstInspection.Count == 0)
        {
          return NotFound(new { IsSuccess = false });
        }

        return Ok(new { IsSuccess = true, InspectionData = lstInspection });
      }
      catch(Exception ex)
      {
        throw;
      }
    }

    [HttpPost("PostAndUpdateInspectionData")]
    public IActionResult PostAndUpdateInspectionData(ClsInspectionsBM objInspection)
    {
      try
      {
        if (objInspection != null)
        {
          // objInspection.InspectionDate.ToString("dd/MM/yyyy");
          objInspection.InspectionDate = objInspection.InspectionDate == null ? (DateTime?)null : objInspection.InspectionDate.Value.AddDays(1);

          if (_IInspection.CheckInspectionExistForPerDay(objInspection.InspectorId, objInspection.InspectionDate.Value, objInspection.Status))
          {
            return Ok(new { IsSuccess = false, Message = "Only one inspection will assign to each inspector for a day "
            });
          }
          if (objInspection.InspectionID == 0)
          {
            _IInspection.InsertInspection(objInspection);

            return Ok(new { IsSuccess = true, Message = "Data insert successfully" });
          }

          if(objInspection.InspectionID > 0)
          {
            _IInspection.UpdateInspection(objInspection);

            return Ok(new { IsSuccess = true, Message = "Data update successfully" });
          }
          
        }

        return BadRequest(new { IsSuccess = false, Message = "Data does not avaiable for insert or update record" });
      }
      catch(Exception ex)
      {
        throw;
      }
    }

    [HttpDelete("DeleteInspection/{InspectionId}")]
    public IActionResult DeleteInspection(int InspectionId)
    {
      try
      {
        if(InspectionId > 0)
        {
          _IInspection.DeleteInspection(InspectionId);

          return Ok(new { IsSuccess = true, Message = "Data delete successfully" });
        }

        return BadRequest(new { IsSuccess = false, Message = "Data does not avaiable for delete record" });
      }
      catch (Exception ex)
      {
        throw;
      }
    }

  }
}
