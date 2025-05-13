using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
// por ahora para mostrar mensajes de prueba
using System.Windows.Forms;


namespace winProySerialComunica
{

    class classComunicacion
    {
        public delegate void manejadorEventos(string mensaje);   // la plantilla de delegado - siempre es el manejador

        public event manejadorEventos LlegoMensaje;  // el delegado que es el evento

        private List<byte> bufferReensamblado = new List<byte>();
        private const int umbralFragmento = 1018;


        SerialPort sPuerto;
        Thread procesoEnviaMensaje;
        private string mensajeEnviar;

        byte[] tramaInfo;
        byte[] tramaCabezera;
        byte[] tramaRelleno;
        byte[] tramaRecibida;

        public classComunicacion(string nomPuerto, int velocidad)
        {
            try
            {
                sPuerto = new SerialPort();
                sPuerto.DataReceived += new SerialDataReceivedEventHandler(sPuerto_Datareceived);
                sPuerto.DataBits = 8;
                sPuerto.StopBits = StopBits.Two;
                sPuerto.Parity = Parity.Odd;
                sPuerto.ReadBufferSize = 2048;
                sPuerto.WriteBufferSize = 1024;
                sPuerto.ReceivedBytesThreshold = 1024; //Asumiendo que la trama 


                sPuerto.PortName = nomPuerto;
                sPuerto.BaudRate = velocidad;
                sPuerto.Open();
                tramaInfo = new byte[1024];
                tramaCabezera = new byte[6];
                tramaRelleno = new byte[1024];
                tramaRecibida = new byte[1024];

                for (int i = 0; i <= 1023; i++)
                    tramaRelleno[i] = 64; //Caracter arroba

                MessageBox.Show("Se abrio el puerto sin problemas");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error descripcion: " + ex.Message);
            }
        }

        public void enviaMensaje(string mensaje)
        {
            if (string.IsNullOrWhiteSpace(mensaje))
            {
                MessageBox.Show("El mensaje no puede estar vacío.");
                return;
            }

            mensajeEnviar = mensaje;
            procesoEnviaMensaje = new Thread(enviandoMensajeLargo);
            procesoEnviaMensaje.Start();
        }



      /*  private void enviandoMensaje()
        {
            try
            {
                // Convierte mensaje a bytes correctamente en UTF8
                tramaInfo = Encoding.UTF8.GetBytes(mensajeEnviar);

                // Arma cabecera con la longitud real del mensaje en 5 dígitos
                string cabecera = "M" + tramaInfo.Length.ToString("D5"); // Ej: M00023
                tramaCabezera = Encoding.ASCII.GetBytes(cabecera);

                // Enviar partes
                sPuerto.Write(tramaCabezera, 0, 6);
                sPuerto.Write(tramaInfo, 0, tramaInfo.Length);
                sPuerto.Write(tramaRelleno, 0, 1018 - tramaInfo.Length);

                // MessageBox.Show("se envió el mensaje");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la transmicion: " + ex.Message);
            }
        }*/

        private void enviandoMensajeLargo()
        {
            try
            {
                byte[] fullMensaje = Encoding.UTF8.GetBytes(mensajeEnviar);
                int maxChunkSize = 1018;
                int totalLength = fullMensaje.Length;
                int offset = 0;

                while (offset < totalLength)
                {
                    int chunkSize = Math.Min(maxChunkSize, totalLength - offset);
                    byte[] chunk = new byte[chunkSize];
                    Array.Copy(fullMensaje, offset, chunk, 0, chunkSize);

                    string cabecera = "M" + chunkSize.ToString("D5"); // Ej: M00118
                    byte[] headerBytes = Encoding.ASCII.GetBytes(cabecera);

                    sPuerto.Write(headerBytes, 0, 6);
                    sPuerto.Write(chunk, 0, chunkSize);

                    // Enviar relleno solo si es menor al máximo
                    if (chunkSize < maxChunkSize)
                        sPuerto.Write(tramaRelleno, 0, maxChunkSize - chunkSize);

                    offset += chunkSize;

                    Thread.Sleep(50); // pequeña pausa entre fragmentos
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar mensaje largo: " + ex.Message);
            }
        }


        public void sPuerto_Datareceived(object o, SerialDataReceivedEventArgs e)
        {
            if (sPuerto.BytesToRead >= 1024)
            {
                sPuerto.Read(tramaRecibida, 0, 1024);

                // Leer cabecera y longitud del fragmento
                string cabecera = Encoding.ASCII.GetString(tramaRecibida, 0, 6); // M0023
                int longitud = int.Parse(cabecera.Substring(1, 5));

                // Extraer mensaje real del fragmento
                byte[] fragmento = new byte[longitud];
                Array.Copy(tramaRecibida, 6, fragmento, 0, longitud);

                bufferReensamblado.AddRange(fragmento);

                // Si el fragmento fue menor al máximo, significa que fue el último
                if (longitud < umbralFragmento)
                {
                    string mensajeCompleto = Encoding.UTF8.GetString(bufferReensamblado.ToArray());
                    bufferReensamblado.Clear();

                    onLLegoMensaje(mensajeCompleto); // Disparar evento con mensaje completo
                }

                // Si longitud == umbralFragmento, esperamos el siguiente fragmento
            }
        }



        // disparar un evento
        // es necesario que e evento este instanciasdo con el metodo a desencadenar
        protected virtual void onLLegoMensaje(string mens)
        {
            if (LlegoMensaje != null)
                LlegoMensaje(mens);
        }
    }
}
