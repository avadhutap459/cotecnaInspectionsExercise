using Inspection.APILayer.Controllers;
using Inspection.ServiceLayer.Interface;
using Inspection.ServiceLayer.Model;
using Inspection.TestFreamwork.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspection.TestFreamwork
{
  public class InspectionControllerTest
  {

    [Theory]
    [InlineData("Inspector 2")]
    public void GetAllInspectionByValidInspectorName_ShouldBeReturn200StatusCode(string InspectorName)
    {
      var InspecSvc = new Mock<IInspection>();

      InspecSvc.Setup(x => x.GetAllInspectionsByInspectorName(InspectorName)).Returns(DataBaseStuff.ReturnAllInspectionsBaseOnInspectorName(InspectorName));

      var sut = new InspectionsController(InspecSvc.Object);

      IActionResult response = sut.GetAllInspectionByInspector(InspectorName);

      var result = response as ObjectResult;

      if (result.Value == null) return;
      var json = Newtonsoft.Json.JsonConvert.SerializeObject(result.Value);
      var values = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

      string message = values.IsSuccess;
      
      Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }

    [Theory]
    [InlineData("Inspector 15")]
    public void GetAllInspectionByInValidInspectorName_ShouldBeReturn200StatusCode(string InspectorName)
    {
      var InspecSvc = new Mock<IInspection>();

      InspecSvc.Setup(x => x.GetAllInspectionsByInspectorName(InspectorName)).Returns(DataBaseStuff.ReturnAllInspectionsBaseOnInspectorName(InspectorName));

      var sut = new InspectionsController(InspecSvc.Object);

      IActionResult response = sut.GetAllInspectionByInspector(InspectorName);

      var result = response as ObjectResult;

      if (result.Value == null) return;
      var json = Newtonsoft.Json.JsonConvert.SerializeObject(result.Value);
      var values = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

      string message = values.IsSuccess;

      Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
    }

    [Fact]
    public void PostAndUpdateInspectionData_ShouldBeRetrun400BadRequest()
    {
      var InspecSvc = new Mock<IInspection>();

      ClsInspectionsBM objInsepction = new ClsInspectionsBM
      {
        InspectionID = 1,
        InspectionDate = DateTime.Now,
        Customer = "Test",
        Address = "India",
        Observations = "Charp issue",
        Status = "New",
        InspectorId = 1,
        InspectorName = "Inspector 1"
      };
      InspecSvc.Setup(x => x.CheckInspectionExistForPerDay(objInsepction.InspectorId, objInsepction.InspectionDate.Value,objInsepction.Status)).Returns(true);

      var sut = new InspectionsController(InspecSvc.Object);

      IActionResult response = sut.PostAndUpdateInspectionData(objInsepction);

      InspecSvc.Verify(x => x.UpdateInspection(objInsepction), times: Times.Never);

    }
  }
}
