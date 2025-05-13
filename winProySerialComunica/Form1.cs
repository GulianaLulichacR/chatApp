using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace winProySerialComunica
{
    public partial class FormChatApp : Form
    {
        classComunicacion ComunicaTXRX;
        private string nombreUsuarioLocal = "Guliana";
        private string nombreUsuarioRemoto = "Fernando"; // Puedes cambiarlo según el nombre del otro usuario


        public FormChatApp()
        {
            InitializeComponent();
            rtxBox.LinkClicked += rtxBox_LinkClicked;

            ComunicaTXRX = new winProySerialComunica.classComunicacion("COM1", 115200);
            ComunicaTXRX.LlegoMensaje += new classComunicacion.manejadorEventos(ComunicaTXRX_LLegomensaje);

            // Asociar evento KeyDown para capturar ENTER
            rtxMensajeEnvia.KeyDown += rtxMensajeEnvia_KeyDown;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnEnviaMensaje.Enabled = false;

            rtxBox.BorderStyle = BorderStyle.None;
            rtxBox.BackColor = Color.WhiteSmoke;
            rtxBox.Font = new Font("Segoe UI Emoji", 12);
            rtxBox.ForeColor = Color.Black;

            btnEnviaMensaje.FlatStyle = FlatStyle.Flat;
            btnEnviaMensaje.FlatAppearance.BorderSize = 0;
            btnEnviaMensaje.FlatAppearance.MouseOverBackColor = Color.DarkCyan;
            btnEnviaMensaje.FlatAppearance.MouseDownBackColor = Color.Black;

            rtxMensajeEnvia.Focus();      // Para que el cursor esté listo para escribir

        }

        private void AgregarMensaje(string remitente, string mensaje, bool alineadoDerecha)
        {
            string hora = DateTime.Now.ToString("HH:mm");

            rtxBox.SelectionStart = rtxBox.TextLength;
            rtxBox.SelectionLength = 0;
            rtxBox.SelectionAlignment = alineadoDerecha ? HorizontalAlignment.Right : HorizontalAlignment.Left;

            // Nombre del remitente en negrita
            rtxBox.SelectionFont = new Font("Segoe UI Emoji", 10, FontStyle.Bold);
            rtxBox.SelectionColor = Color.DarkCyan;
            rtxBox.AppendText(remitente + ": ");

            // Mensaje principal
            rtxBox.SelectionFont = new Font("Segoe UI Emoji", 12, FontStyle.Regular);
            rtxBox.SelectionColor = Color.Black;
            rtxBox.AppendText(mensaje + Environment.NewLine);

            // Hora del mensaje
            rtxBox.SelectionFont = new Font("Segoe UI Emoji", 8, FontStyle.Regular);
            rtxBox.SelectionColor = Color.Gray;
            rtxBox.AppendText("   " + hora + Environment.NewLine + Environment.NewLine);

            rtxBox.ScrollToCaret();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string mensaje = rtxMensajeEnvia.Text.Trim();
            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                ComunicaTXRX.enviaMensaje(mensaje);
                AgregarMensaje(nombreUsuarioLocal, mensaje, true);
                rtxMensajeEnvia.Clear();
                rtxMensajeEnvia.Focus();
            }
        }


        private void ComunicaTXRX_LLegomensaje(string m)
        {
            if (rtxBox.InvokeRequired)
            {
                rtxBox.Invoke(new Action(() =>
                {
                    AgregarMensaje(nombreUsuarioRemoto, m, false);
                }));
            }
            else
            {
                AgregarMensaje(nombreUsuarioRemoto, m, false);
            }
        }


        private void rtxMensajeEnvia_TextChanged(object sender, EventArgs e)
        {
            btnEnviaMensaje.Enabled = !string.IsNullOrWhiteSpace(rtxMensajeEnvia.Text);
        }

        private void rtxBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
         
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = e.LinkText,   // uso directo del texto detectado
                    UseShellExecute = true   // le pide al SO que lo abra con la app apropiada
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir el enlace: " + ex.Message);
            }
     
    }


    private void rtxMensajeEnvia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true; // Evita el salto de línea
                btnEnviaMensaje.PerformClick();
            }
        }

        private void rtxBox_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
