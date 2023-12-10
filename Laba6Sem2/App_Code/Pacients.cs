using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Laba6Sem2.App_Code
{
    public enum Vacitation
    {
        Zero = 0,
        One = 1,
        Two = 2
    }
    public class Pacient
    {
        public string FullName { get; set; }
        public Vacitation Vacitation { get; set; }
        public Pacient(string fullName, Vacitation vacitation)
        {
            FullName = fullName;
            Vacitation = vacitation;
        }
        public int VacitationNum
        {
            get
            {
                return (int)Vacitation;
            }
        }
        public static bool IsSertificate(Pacient pacient)
        {
            if (pacient.Vacitation == Vacitation.Two)
            {
                return true;
            }
            return false;
        }

    }
}
