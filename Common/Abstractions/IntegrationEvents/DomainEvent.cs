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
        public record EmailNotificationEvent : INotificationEvent
        {
            public string Name { get ; set ; }
            public string Description { get ; set ; }
            public string Type { get ; set ; }
            public Guid TransactionId { get ; set ; }
            public Guid Id { get ; set ; }
            public DateTimeOffset Timestamp { get ; set ; }
        }

        public record SmsNotificationEvent : INotificationEvent
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public Guid TransactionId { get; set; }
            public Guid Id { get; set; }
            public DateTimeOffset Timestamp { get; set; }
        }

        public record BookAppointmentEvent : INotificationEvent
        {
            public Guid Id { get ; set ; }
            public DateTimeOffset Timestamp { get ; set ; }
            public int SelectedDoctorId { get; set ; }
            public string NamePatient { get; set; }
            public string DescribeSymptoms { get; set; }
            public string SelectedDate { get; set; }
            public string OptionDate { get; set; }
            public int IdApoint { get; set; }
        }

        public record ChangeStatusEvent : INotificationEvent
        {
            public Guid Id { get; set; }
            public DateTimeOffset Timestamp { get; set; }
            public int IdApoint { get; set; }
            public string Status { get; set; }
            public string Note { get; set; }
        }
    }
}
