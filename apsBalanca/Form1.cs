using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apsBalanca
{
    public partial class FormPrincipal : Form
    {
        //  variaveis
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private List<FoodValue> FoodValues = new List<FoodValue>
        {
            new FoodValue
            {
                Caloria = 0.53,
                Carboidrato = 0.1190,
                Gordura = 0.00425,
                Proteina = 0.0030
            },
            new FoodValue
            {
                Caloria = 0.47,
                Carboidrato = 0.12,
                Gordura = 0.001,
                Proteina = 0.009
            },
            new FoodValue
            {
                Caloria = 1.30,
                Carboidrato = 0.2817,
                Gordura = 0.0028,
                Proteina = 0.0269
            },
        };

        //  Construtor
        public FormPrincipal()
        {
            InitializeComponent();
            random = new Random();
            btnMenuPeso_Click(btnMenuPeso, new EventArgs());

        }
        //  Metodos
        private Color SelectThemeColor()
        {
            int index = random.Next(Tema.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(Tema.ColorList.Count);
            }
            tempIndex = index;
            string color = Tema.ColorList[tempIndex];
            return ColorTranslator.FromHtml(color);
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {

                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btnCalibrar.BackColor = color;
                    lblPeso.ForeColor = color;
                    lblKG.ForeColor = color;
                    panelLogo.BackColor = Tema.ChangeColorBrightness(color, -0.3);
                    pnlTitulo.BackColor = color;
                    //btnOk.BackColor = color;
                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(50, 52, 77);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void btnMenuPeso_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
                serialPort.Open();


            lblTitulo.Text = "Pesar";
            ActivateButton(sender);
            panelPeso.Show();
        }

        private void btnMenuSobre_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)  
                serialPort.Close(); 

            lblTitulo.Text = "Sobre";
            ActivateButton(sender);
            panelPeso.Hide();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    lblPeso.Text = serialPort.ReadLine();
                }
            }
            catch (Exception ex)
            {
                lblPeso.Text = ex.Message;
            }



            if (cbAlimentos.SelectedIndex != -1)
            {
                lblCarboValue.Text = (Convert.ToDouble(lblPeso.Text) * FoodValues[cbAlimentos.SelectedIndex].Carboidrato).ToString() + " g";
                lblProteinaValue.Text = (Convert.ToDouble(lblPeso.Text) * FoodValues[cbAlimentos.SelectedIndex].Proteina).ToString() + " g";
                lblGorduraValue.Text = (Convert.ToDouble(lblPeso.Text) * FoodValues[cbAlimentos.SelectedIndex].Gordura).ToString() + " g";
                lblCaloriaValue.Text = (Convert.ToDouble(lblPeso.Text) * FoodValues[cbAlimentos.SelectedIndex].Caloria).ToString() + " g";
            }
        }

        private void btnCalibrar_Click(object sender, EventArgs e)
        {
            serialPort.Write("t");
            timer1.Enabled = true;
            timer1.Stop();
            Thread.Sleep(500);
            timer1.Start();
            //timer1.Enabled = false;
        }

        private void cbAlimentos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private class FoodValue
        {
            public double Carboidrato { get; set; }
            public double Proteina { get; set; }
            public double Gordura { get; set; }
            public double Caloria { get; set; }
        }
    }
}
