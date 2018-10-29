using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AfricasTalkingCS;
using static SimpleLogInSystem.AppSettings;
using static System.Console;

namespace SimpleLogInSystem
{
    class Program
    {
        private static string _username;
        private static string _phoneNumber;
        private static int code;

        private delegate void OnSendSms(string phonenumber,int code);
        private delegate void OnVerifyCode(int code);
        static void Main()
        {
            Write("Username : ");
            _username = ReadLine();
            Write("Phone Number : ");
            _phoneNumber = ReadLine();

            if (_phoneNumber != null && (_phoneNumber.StartsWith("+254") && _phoneNumber.Length.Equals(13)))
            {
                code=new Random().Next(1000);
                var onSendSms=new OnSendSms(SendSms);
                onSendSms.Invoke(_phoneNumber, code);
                Write("Verification Code: ");
                var inputCode = Convert.ToInt32(ReadLine());
                var onVerify=new OnVerifyCode(VerifyCode);
                onVerify.Invoke(inputCode);
            }
            else
            {
                WriteLine(_phoneNumber);
                Clear();
                Main();
            }

            ReadKey();
        }

        private static void VerifyCode(int inputCode)
        {
            if(inputCode!=code)
                WriteLine("Invalid Code");
            else
            {
                Clear();
                WriteLine($"Welcome {_username}");
            }
        }

        static void SendSms(string phoneNo, int code)
        {
            InitialiseSettings();
            var gateway = new AfricasTalkingGateway(UserName, ApiKey);
            try
            {
                dynamic results = gateway.SendMessage(phoneNo, $"Your verification code is {code}");
                WriteLine("Sms Sent");
            }
            catch (Exception e)
            {
                WriteLine(e);
                throw;
            }
        }
    }
}
