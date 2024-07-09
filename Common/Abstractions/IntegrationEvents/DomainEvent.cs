using Common.Abstractions.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Abstractions.IntegrationEvents
{
    public static class DomainEvent
    {
        public record class EmailNotificationEvent : INotificationEvent
        {
            public string Name { get ; set ; }
            public string Description { get ; set ; }
            public string Type { get ; set ; }
            public Guid TransactionId { get ; set ; }
            public Guid Id { get ; set ; }
            public DateTimeOffset Timestamp { get ; set ; }
        }

        public record class SmsNotificationEvent : INotificationEvent
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public Guid TransactionId { get; set; }
            public Guid Id { get; set; }
            public DateTimeOffset Timestamp { get; set; }
        }

        public record class BookAppointmentEvent : INotificationEvent
        {
            public Guid Id { get ; set ; }
            public DateTimeOffset Timestamp { get ; set ; }
            public int SelectedDoctorId { get; set ; }
            public string NamePatient { get; set; }
            public string DescribeSymptoms { get; set; }
            public string SelectedDate { get; set; }
            public string OptionDate { get; set; }
        }
    }
}
