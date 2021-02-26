using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace src
{
    public static class UdrChangeAudit
    {
        [FunctionName("UdrChangeAudit")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            // delay time
            log.LogInformation(eventGridEvent.Data.ToString());


            dynamic eventData = JsonConvert.DeserializeObject(eventGridEvent.Data.ToString());

            if (eventGridEvent.EventType == "Microsoft.Resources.ResourceWriteSuccess" 
                && eventData["operationName"] == "Microsoft.Network/routeTables/routes/write"
                //&& eventGridEvent.Subject.StartsWith("/subscriptions/930c11b0-5e6d-458f-b9e3-f3dda0734110/resourceGroups/crgar-dns-es-spoke/providers/Microsoft.Network/routeTables/crgar-dns-es-spoke-udr/routes")
            )
            {
                log.LogInformation("Route table has been changed");
                
            }
        }
    }
}
