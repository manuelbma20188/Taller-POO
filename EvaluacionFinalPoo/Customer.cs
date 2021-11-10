using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_3
{
    class Customer
    {
        //atributes of our class
        
        private string names;
        private string lastname;
        private DateTime bornDate;
        private DateTime contractDate;
        private double amount; //salary
        private double rent;
        private double isss;        
        private string dui;
        private string email;
        private string phone;
        private double finalSalary;

        //only numbers
        public string Dui { get => dui; set => dui = value; }
        public string Names { get => names; set => names = value; }
        public string Lastname { get => lastname; set => lastname = value; }

        //only numbers
        public double Amount { get => amount; set => amount = value; }
        public DateTime BornDate { get => bornDate; set => bornDate = value; }
        public DateTime ContractDate { get => contractDate; set => contractDate = value; }

        //only numbers
        public double Rent { get => rent; set => rent = value; }

        //only numbers
        public double Isss { get => isss; set => isss = value; }
        public string Email { get => email; set => email = value; }

        //only numbers
        public string Phone { get => phone; set => phone = value; }
        public double FinalSalary { get => finalSalary; set => finalSalary = value; }
    }
}
