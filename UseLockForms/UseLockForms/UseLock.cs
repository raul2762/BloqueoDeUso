using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace UseLockForms
{
    public class UseLock
    {
        public static bool Access_ { get; set; }
        public static bool Estado_Net;
        public static string SerialNumber;
        public static bool compruebaConexion()
        {

            WebRequest request = null;
            WebResponse response = null;
            Uri Url = new Uri("http://www.google.com.do");

            try
            {
                //Creamos la request
                request = System.Net.WebRequest.Create(Url);
                //POnemos un tiempo limite
                request.Timeout = 5000;
                //ejecutamos la request
                response = request.GetResponse();
                response.Close();

                request = null;

                return true;


            }
            catch
            {
                //si ocurre un error, devolvemos error
                request = null;

                return false;

            }
        }
        public static void GetSerial()
        {
            Estado_Net = compruebaConexion();
            if (Estado_Net == true)
            {
                try
                {
                    string url = "http://domipass.000webhostapp.com/BloqueoDeUso/General.txt";
                    WebRequest req = WebRequest.Create(url);
                    WebResponse resp = req.GetResponse();
                    Stream str = resp.GetResponseStream();
                    StreamReader leer = new StreamReader(str);
                    SerialNumber = leer.ReadLine();
                }
                catch
                {
                    SerialNumber = "3556215";
                }
                
            }
            else
            {
                SerialNumber = "3556215";
            }
        }
        public static void ManageAttempt()
        {
            GetSerial();

            RegistryKey rk1 = Registry.CurrentUser;
            RegistryKey rk2 = Registry.CurrentUser;
            string valor1;
            rk1 = rk1.OpenSubKey("Microsoft_LA_RD", true);

            if (rk1 == null)
            {
                RegistryKey rk3 = rk2.CreateSubKey("Microsoft_LA_RD");
                rk3.SetValue("Attempt", "1", RegistryValueKind.String);
                MessageBox.Show($"Este programa tiene un limite de 5 uso. cuando supere el limite debera digitar un numero de serie para continuar su uso", "Notificación de uso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Access_ = true;
            }
            else
            {
                valor1 = rk1.GetValue("Attempt").ToString();
                switch (valor1)
                {
                    case "1":
                        rk1.SetValue("Attempt", "2", RegistryValueKind.String);
                        Access_ = true;
                        break;
                    case "2":
                        rk1.SetValue("Attempt", "3", RegistryValueKind.String);
                        Access_ = true;
                        break;
                    case "3":
                        rk1.SetValue("Attempt", "4", RegistryValueKind.String);
                        Access_ = true;
                        break;
                    case "4":
                        rk1.SetValue("Attempt", "5", RegistryValueKind.String);
                        Access_ = true;
                        break;
                    case "5":
                        InputBox.SetLanguage(InputBox.Language.Spanish);
                        DialogResult res = InputBox.ShowDialog($"Limite de uso superado, introdusca un numero de serie para continuar usando este programa: ",
                            "Aplicacion Bloqueada",
                            InputBox.Icon.Error,
                            InputBox.Buttons.OkCancel,
                            InputBox.Type.TextBox);
                        if (res == DialogResult.OK)
                        {
                            while (InputBox.ResultValue.Length > 0 || String.IsNullOrEmpty(InputBox.ResultValue))
                            {

                                if (InputBox.ResultValue == SerialNumber)
                                {

                                    Access_ = true;
                                    break;

                                }
                                else
                                {
                                    DialogResult res2 = InputBox.ShowDialog($"Numero de serie incorrecto!", "Aplicacion Bloqueada", InputBox.Icon.Error, InputBox.Buttons.OkCancel, InputBox.Type.TextBox, null, false, null);
                                    if (res2 == DialogResult.Cancel)
                                    {
                                        Access_ = false;
                                        break;
                                    }
                                }
                            }
                        }
                        else if (res == DialogResult.Cancel)
                        {
                            Access_ = false;
                            break;
                        }

                        //MessageBox.Show($"Ha superado la cantidad de uso que es de: {valor1} uso por usuario" , "Aplicacion Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        break;
                    default:
                        break;
                }

            }
        }

    }
}

