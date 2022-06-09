using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared.ResponeModels.Dashboard
{
    public class StatisticalRespone
    {
        public StatisticalRespone(decimal amountdonated, int projectcreated, int donated, 
            int projectcompleted, int projectimplementation, int projectwaitting,
            int lockusers, IEnumerable<object> donatedinmonth)
        {
            AmountDonated = amountdonated + " TRX";
            ProjectCreated = projectcreated;
            Donated = donated;
            ProjectCompleted = projectcompleted;
            ProjectImlementation = projectimplementation;
            ProjectWaitting = projectwaitting;
            LockUsers = lockusers;
            DonatedInMonth = donatedinmonth;
        }


        public string AmountDonated { get; set; }
        public int ProjectCreated { get; set; }
        public int Donated { get; set; }
        public int ProjectCompleted { get; set; }
        public int ProjectImlementation { get; set; }
        public int ProjectWaitting { get; set; }
        public int LockUsers { get; set; }
        public IEnumerable<object> DonatedInMonth { get; set; }
    }
}
