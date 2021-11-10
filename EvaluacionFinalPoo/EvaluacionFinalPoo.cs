using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica_3
{
    public partial class EvaluacionFinalPoo : Form
    {
        public EvaluacionFinalPoo()
        {
            InitializeComponent();
        }
        private int count;
        private List<Customer> Customers = new List<Customer>(); //instace of my class Customers
        private int edit_index = -1;

        //function to validate empty txt's
        bool validateEntrances(GroupBox ejercicio1) //instace, type GroupBox
        {            
            bool empty=false;
            foreach (Control c in ejercicio1.Controls) // I check the elements of the groupbox
            {
                // if control c is a textbox and it is empty, vacio is gonna be true
                if (c.GetType() == typeof(TextBox) && c.Text == String.Empty)
                {
                    empty = true;
                }                                       
            }

            //print a message
            if (empty == true)
            {
                MessageBox.Show("Llena los campos que estén vacios!");
            }

            return empty;
        }

        //fuction to validate entrances

        //---validating salary
        private bool ValidateSalary(double amount)
        {
            bool var = false;
            if(amount ==0)
            {
                MessageBox.Show("Ingresa un valor diferente de cero!");              
                var = true;
            }
            return var;
        }

        //validating dui
        private bool ValidateDui(string duiTxt)
        {
            bool var = false;
            //regular expression to validate DUI entrance
            //format: ########-#
            Regex dui = new Regex("^[0-9]{8}-[0-9]{1}$");
            if (!dui.IsMatch(duiTxt))
            {
                MessageBox.Show("Formato de DUI incorrecto");
                txtDui.Focus();
                var = true;
            }
            return var;
        }          
        
        private bool validateEmail(string mailTxt)
        {
            bool var = false;
            //regular expression
            Regex mail = new Regex(@"^[a-zA-Z0-9.!#$%&'+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)$");
            if (!mail.IsMatch(mailTxt))
            {
                MessageBox.Show("Formato de correo incorrecto");                
                var = true;
            }
            return var;
        }

        private bool validatePhone(string phoneTxt)
        {
            bool var = false;
            //regular expression
            Regex phone = new Regex(@"^([0-9]+)$");
            if (!phone.IsMatch(phoneTxt))
            {
                MessageBox.Show("Formato de numero de telefono incorrecto");
                var = true;
            }
            return var;
        }

        private void updateGrid()
        {
            dgvDatos.DataSource = null;
            dgvDatos.DataSource = Customers; 
        }

        private void delete(GroupBox groupBox)
        {            
            foreach (Control c in groupBox.Controls) // I check the elements of the groupbox
            {
                // if control c is a textbox and it is empty, vacio is gonna be true
                if (c.GetType() == typeof(TextBox))
                {
                    c.Text = "";
                }
            }
        }

        //---- Functions to calculate salary, afp, etc.
        
        public double calculateIsss(double sueldo)
        {
            double sueldoFinal = 0;
            sueldoFinal = (sueldo * 0.03);
            return sueldoFinal;
        }
                       
        public double calculateRent(double sueldo)
        {
            double sueldoFinal = 0;
            sueldoFinal = (sueldo * 0.10);
            return sueldoFinal;
        }

        public double calculateSalary(double sueldo, double iss, double renta)
        {
            double sueldoLiquido = 0;            
            sueldoLiquido = sueldo - (iss  + renta);
            return sueldoLiquido;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Ejercicio1_Load(object sender, EventArgs e)
        {
            //welcome message :)            
            MessageBox.Show("¡BIENVENIDOS!");            
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            groupBox3.Enabled = true;
            btnAceptar.Enabled = true;            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void txtDui_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }

        //keypress event, validate txt 'only numbers' acepted
        

        private void btnMostrar_Click(object sender, EventArgs e)
        {           
        }

        private void txtSueldoIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            //user can write only one . 
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!validateEntrances(groupBox1)) //if the function return false, we're gonna make all the proccess
            {
                Customer customer = new Customer(); //instance of Customer's class
                DateTime fechaNacimiento = dtpFechaNac.Value;
                //verifying 
                int year = System.DateTime.Now.Year - fechaNacimiento.Year;
                int month = System.DateTime.Now.Month - fechaNacimiento.Month;
                int day = System.DateTime.Now.Day - fechaNacimiento.Day;

                //validating
                if (year == 0 && month <= 0 && day <= 0)
                {
                    MessageBox.Show("Fecha no válida");
                    return;
                }
                else
                {
                    try
                    {
                        double isss = calculateIsss(Convert.ToDouble(txtSueldoIni.Text));
                        double rent = calculateRent(Convert.ToDouble(txtSueldoIni.Text));
                        double finalSalary = calculateSalary(Convert.ToDouble(txtSueldoIni.Text), isss, rent);
                        //set value to attributes                        
                        customer.Names = txtNombres.Text.ToString();
                        customer.Lastname = txtApellidos.Text.ToString();
                        customer.Dui = (txtDui.Text.ToString());
                        customer.BornDate = dtpFechaNac.Value;
                        customer.ContractDate = dtpFechaContrato.Value;
                        customer.Email = txtCorreo.Text.ToString();
                        customer.Phone = (txtTelefono.Text);
                        customer.Amount = Convert.ToDouble(txtSueldoIni.Text.ToString());
                        //IMPORTANT: 
                        /*
                            Following the instructions (ISSS = 3%, RENT = 10%)
                            Is contradictory to use the value of the numeric updowns, because is like
                            you're gonna set the percentajes that are presetted by the document PDF
                         */
                        customer.Rent = rent;
                        customer.Isss = isss;
                        customer.FinalSalary = Convert.ToDouble(finalSalary);
                    }
                    catch (Exception x)
                    {
                        //print message
                        MessageBox.Show(x.Message);
                        return;
                    }
                }


                //same logic, if the functions returns false, we're gonna make all the proccess
                if (!ValidateSalary(customer.Amount) && !ValidateDui(customer.Dui)
                    && !validateEmail(customer.Email) && !validatePhone(customer.Phone))
                {
                    count++; 
                    if(count <= 10)
                    {
                        MessageBox.Show("Cliente registrado satisfactoriamente");
                        delete(groupBox1);
                        delete(groupBox3);
                        txtNombres.Focus();
                        if (edit_index > -1)
                        {
                            Customers[edit_index] = customer;
                            edit_index = -1;
                        }
                        else
                        {
                            Customers.Add(customer);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Limite de empleados registrados alcanzado!");
                        groupBox1.Enabled = false;
                        groupBox3.Enabled = false;
                        btnNuevo.Enabled = false;
                        btnAceptar.Enabled = false;                        
                        btnAbrirPlanilla.Enabled = true;                        
                    }                                                          
                }
            }
        }

        private void btnAbrirPlanilla_Click(object sender, EventArgs e)
        {
            dgvDatos.Enabled = true;
            btnAbrirPlanilla.Enabled = false;
            btnGenerarPlanilla.Enabled = true;            
        }

        private void btnGenerarPlanilla_Click(object sender, EventArgs e)
        {
            updateGrid();
        }
    }
}
