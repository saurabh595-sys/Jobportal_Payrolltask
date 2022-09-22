using Jobportel.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Jobportel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected int UserId => int.Parse(this.User.Claims.First(x => x.Type == "UserId").Value);
        protected int RoleId => int.Parse(this.User.Claims.First(x => x.Type == "RoleId").Value);

        protected OkObjectResult OkResponse( string message, dynamic data)
        {
            return Ok( new Response { StatusCode = StatusCodes.Status200OK, Message = message, Data = data });
        }
        protected BadRequestObjectResult BadResponse(string message,Object data)
        {
            return BadRequest(new Response { StatusCode = StatusCodes.Status400BadRequest, Message = message, Data = data });
        }
        protected NotFoundObjectResult NotFoundResponse(string message,Object data)
        {
            return NotFound(new Response { StatusCode = StatusCodes.Status404NotFound, Message = message, Data = data });
        }



        protected FileStreamResult Export(List<IEnumerable<dynamic>> data/* IEnumerable<T> recruitersList,*/ /*IEnumerable<T> JobsAppliedByCandidatesList*/)
        {

            var stream = new MemoryStream();
            List<dynamic> datalist= new List<dynamic>();
            using (var excle = new ExcelPackage(stream))
            {
                foreach (var item in data)
                {
                    datalist.Add(item);
                }
                var workSheet = excle.Workbook.Worksheets.Add("candidates");
                workSheet.Cells.LoadFromCollection(datalist[0], true);
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                var workSheet2 = excle.Workbook.Worksheets.Add("recruiters");
                workSheet2.Cells.LoadFromCollection(datalist[1], true);
                workSheet2.Cells[workSheet.Dimension.Address].AutoFitColumns();

                var workSheet3 = excle.Workbook.Worksheets.Add("Jobs Applied By Candidates");
                workSheet3.Cells.LoadFromCollection(datalist[2], true);
                workSheet3.Cells[workSheet.Dimension.Address].AutoFitColumns();
                excle.SaveAs(stream);
            }
            stream.Position = 0;

            return File(
                   stream,
                   "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                   "Users.xlsx"
                   );
        }
    }
}
