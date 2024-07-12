using Common.Abstractions.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Abstractions.IntegrationEvents
{
    public static class Command
    {
        public record SendBookAppointment : IRequest /*: INotificationEvent*/
        {
            public Guid Id { get; set; }
            public DateTimeOffset Timestamp { get; set; }
            public int SelectedDoctorId { get; set; }
            public string NameDoctor { get; set; }
            public string NamePatient { get; set; }
            public string DescribeSymptoms { get; set; }
            public string SelectedDate { get; set; }
            public string OptionDate { get; set; }
        }
    }
}
