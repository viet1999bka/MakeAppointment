using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Abstractions.IntegrationEvents
{
    public  record SendAppointmentToProcessRequest
    {
        public Guid Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public int SelectedDoctorId { get; set; }
        public string NamePatient { get; set; }
        public string DescribeSymptoms { get; set; }
        public string SelectedDate { get; set; }
        public string OptionDate { get; set; }
        public int IdApoint { get; set; }
    }

    public record UpdateStatusAppintResponse
    {
        public Guid Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string status { get; set; }  
        public string note { get; set; }
    }
}
