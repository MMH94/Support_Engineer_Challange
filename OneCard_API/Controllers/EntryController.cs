using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace OneCard_API.Controllers
{
    public class EntryController : ApiController
    {


        // ----- GET: ~/api/Entry/id
        //[Route("api/Entry/{id:int}")]
        [Route("api/GetEntry")]
        [HttpGet]
        public HttpResponseMessage GetEntry([FromUri] string bodyDetail)
        {
           
          
            HttpResponseMessage response;
            string targetPath;
            int newID;

            // ----- Get the new entry ID.
            newID = (new Random()).Next(1, 10000);
            

            // ----- Build the response with a location.
            response = Request.CreateResponse(HttpStatusCode.Created);
            targetPath = $"~/api/GetEntry/{newID}";
            response.Headers.Location = RelativeToAbsolutePath(Request, targetPath);

            // -------- get values to print
            List<string> valuesToPrint = new List<string>();
            valuesToPrint.Add("Body:" + bodyDetail);
            valuesToPrint.Add("Method:" + Request.Method.ToString());
            valuesToPrint.Add("Content-Type:" + Request.Content.Headers.ContentType.ToString());
            PrintToFile(valuesToPrint);


            return response;
        }

                     


        // ----- POST: ~/api/Entry
        [Route("api/Entry")]
        [HttpPost]
        public HttpResponseMessage PostEntry([FromBody] string bodyDetail)
        {
            // ----- Pretend to add an entry to internal storage.
            HttpResponseMessage response;
            string targetPath;
            int newID;

            // ----- Get the new entry ID.
            newID = (new Random()).Next(1, 10000);
                                         


            // ----- Build the response with a location.
            response = Request.CreateResponse(HttpStatusCode.Created);
            targetPath = $"~/api/Entry/{newID}";
            response.Headers.Location = RelativeToAbsolutePath(Request, targetPath);

            // -------- Get values to print.
            List<string> valuesToPrint = new List<string>();
            valuesToPrint.Add("Body:" + bodyDetail);
            valuesToPrint.Add("Method:" + Request.Method.ToString());
            valuesToPrint.Add("Content-Type:" + Request.Content.Headers.ContentType.ToString());
            PrintToFile(valuesToPrint);

            return response ;



        }

        private static Uri RelativeToAbsolutePath(
         HttpRequestMessage requestSource, string relativePath)
        {
            Uri baseUri = new Uri(requestSource.RequestUri.AbsoluteUri.Replace(
              requestSource.RequestUri.PathAndQuery, String.Empty));
            return new Uri(baseUri, VirtualPathUtility.ToAbsolute(relativePath));
        }

                     




        // write the output in file 
        public void PrintToFile(List<string> lines)
        {

            // Set a variable to the Documents path.  Note You can change the path
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "WriteLines.txt".
            using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(System.IO.Path.Combine(docPath, "Response.txt")))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }

      


    }
}