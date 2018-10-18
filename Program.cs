using System;

namespace Delegates
{
    class Program
    {        
        static void Main(string[] args)
        {
            var s=new Myclass();
            s.GetInterger(Mymethod2);
            Console.ReadKey();

        }
        public static void  Mymethd( int b)
        {
            System.Console.WriteLine(b);
        }
public static void Mymethod2(int c)
{
    Console.WriteLine("Hello");
}
        
    }
    public class Myclass
    {
        //Define Delegate Signature
        public delegate void Mydelegate (int a);

        //Pass the delegate as a parmetr to the method below
        //this delegate serves to pass back the value from the GetInteger method to the calling method in the 
        //other class.
        //Recall the passed method has the same signatue and hence we can argue that the passed metthod is equivalent
        // to the delegate
        public void GetInterger(Mydelegate f)
        {
            for(int x = 1;x<=100;x++)
            {
                f.Invoke(x);
            }
        }
        
    }
}
