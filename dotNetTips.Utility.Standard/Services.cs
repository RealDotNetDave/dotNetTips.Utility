using dotNetTips.Utility.Standard.OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Collections;

namespace dotNetTips.Utility.Standard
{
    public static class Services
    {
        //TODO: BLOG POST
        public static ServiceControllerStatus ServiceStatus(string serviceName)
        {
            var service = LoadService(serviceName);

            if (service != null)
            {
                return service.Status;
            }
            else
            {
                throw new InvalidOperationException("Service not found.");
            }
        }

        public static bool ServiceExists(string serviceName)
        {
            var service = LoadService(serviceName);

            if (service != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ServiceActionResult StopService(string serviceName)
        {
            var statusResult = ServiceActionResult.NotFound;

            if (ServiceExists(serviceName) == false)
            {
                return statusResult;
            }

            var service = LoadService(serviceName);

            if ((service != null && service.Status == ServiceControllerStatus.Running))
            {
                service.Stop();
                statusResult = ServiceActionResult.Stopped;
            }

            return statusResult;
        }

        public static ServiceActionResult StartService(string serviceName)
        {
            var statusResult = ServiceActionResult.NotFound;

            if (ServiceExists(serviceName) == false)
            {
                return statusResult;
            }

            var service = LoadService(serviceName);

            if ((service != null && service.Status == ServiceControllerStatus.Stopped))
            {
                service.Start();
                statusResult = ServiceActionResult.Running;
            }

            return statusResult;
        }

        public static IEnumerable<string> AllServices()
        {
            return ServiceController.GetServices().Select(p => p.ServiceName).AsEnumerable();
        }

        private static ServiceController LoadService(string serviceName)
        {
            return ServiceController.GetServices().Where(p => p.ServiceName == serviceName).FirstOrDefault();
        }

        public static void StartStopServices(IEnumerable<ServiceAction> requests)
        {
            Encapsulation.TryValidateParam(requests, nameof(requests));

            foreach (var request in requests)
            {
                if (request.ServiceActionRequest == ServiceActionRequest.Start)
                {
                    request.ServiceActionResult = StartService(request.ServiceName);
                }
                else if (request.ServiceActionRequest == ServiceActionRequest.Stop)
                {
                    request.ServiceActionResult = StopService(request.ServiceName);
                }
            }
        }
    }

    public enum ServiceActionResult
    {
        NotFound,
        Running,
        Stopped
    }

    public enum ServiceActionRequest
    {
        Unknown,
        Start,
        Stop
    }

    public class ServiceAction
    {
        public string ServiceName { get; set; }
        public ServiceActionRequest ServiceActionRequest { get; set; }
        public ServiceActionResult ServiceActionResult { get; set; }
    }
}
