using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class AppointmentModel
    {
        public AppointmentModel()
        {
            Consultants = new List<DoctorModel>();
            Patients = new List<PatientModel>();
        }
        public string NamePatients { get; set; }
        public string DescribeSymptoms { get; set; }

        public int SelectedConsultantId { get; set; }

        public int SelectedPatientId { get; set; }

        public DateTime SelectedDate { get; set; }
        public DateTime OptionDate1 { get; set; }
        public DateTime OptionDate2 { get; set; }

        public string SelectedTime { get; set; }

        public List<DoctorModel> Consultants { get; private set; }
        public List<PatientModel> Patients { get; private set; }
    }
}
