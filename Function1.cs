using Crabit_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Crabit_API
{
    public static class Function1
    {
        public static readonly List<Person> personItems = new List<Person>();

        [FunctionName("CreatePerson")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "create/person")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Creacion de nuevos datos de Persona.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<CreatePerson>(requestBody);
            var person = new Person()
            {
                DNI = input.DNI,
                CUI = input.CUI,
                paternalSurname = input.paternalSurname,
                maternalSurname = input.maternalSurname,
                ftName = input.ftName,
                sdName = input.sdName,
                birthday = input.birthday,
                gender = input.gender,
                location = input.location,
                maritalStatus = input.maritalStatus
            };
            try
            {
                personItems.Add(person);
                return new OkObjectResult(person);

            }
            catch (Exception e)
            {
                log.LogError($"Excepcion lanzada: {e.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [FunctionName("GetPeople")]
        public static IActionResult GetPeople(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "people")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Obtener todas las personas.");
            return new OkObjectResult(personItems);
        }

        [FunctionName("GetPersonByDNI")]
        public static IActionResult GetPersonByDNI(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "person/{dni}")] HttpRequest req,
            ILogger log, string dni)
        {
            var person = personItems.FirstOrDefault(p => p.DNI == dni);
            if (person == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(person);
        }

        [FunctionName("UpdatePerson")]
        public static async Task<IActionResult> UpdatePerson(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "update/person/{dni}")] HttpRequest req,
            ILogger log, string dni)
        {
            var person = personItems.FirstOrDefault(p => p.DNI == dni);
            if (person == null)
            {
                return new NotFoundResult();
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updatedPerson = JsonConvert.DeserializeObject<UpdatePerson>(requestBody);

            person.maritalStatus = updatedPerson.maritalStatus;
            if (!string.IsNullOrEmpty(updatedPerson.location))
            {
                person.location = updatedPerson.location;
            }

            return new OkObjectResult(person);
        }

        [FunctionName("DeletePerson")]
        public static IActionResult DeletePerson(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "delete/person/{dni}")] HttpRequest req,
            ILogger log, string dni)
        {
            var person = personItems.FirstOrDefault(p => p.DNI == dni);
            if (person == null)
            {
                return new NotFoundResult();
            }
            personItems.Remove(person);
            return new OkResult();
        }

    }
}
