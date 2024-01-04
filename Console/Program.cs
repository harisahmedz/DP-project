using Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    //1) FOF
    //2) Bridge
    //3) State
    //4) Strategy
    //5) Composite
    //6) Singleton
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("HOSPITAL MANAGEMENT SYSTEM");
            System.Console.WriteLine("===========================");
            Start();
            System.Console.WriteLine();
        }
        public static void Start()
        {
            System.Console.WriteLine("Select");
            System.Console.WriteLine("==============");
            System.Console.WriteLine("1 for Log in as Doctor");
            System.Console.WriteLine("2 for Log in as Patient");
            System.Console.WriteLine("3 to visit Pharmacy");

            char a = char.Parse(System.Console.ReadLine());
            if (a == '3')
            {
                GoToPharmacy gtp = new GoToPharmacy();
            }
            else
            {
                Login l = Login.getInstance();
                l.auth(a);
            }
        }
    }

    //Doctor Class
    public class Doctor
    {
        public string name = "";
        public List<Doctor> preReq = new List<Doctor>();
        public Doctor()
        {
        }
        public Doctor(string inputPass)
        {
            System.Console.WriteLine("Select");
            System.Console.WriteLine("==============");
            System.Console.WriteLine("1 to Add Doctor");
            System.Console.WriteLine("2 to Remove Doctor");
            System.Console.WriteLine("3 for Disease Info");
            System.Console.WriteLine("4 to Logout");

            char a = char.Parse(System.Console.ReadLine());
            switch (a)
            {
                case '1':
                    Context context2 = new Context(new OpAdd());
                    System.Console.WriteLine(context2.execStrategy());
                    break;

                case '2':
                    Context context3 = new Context(new OpRemove());
                    System.Console.WriteLine(context3.execStrategy());
                    break;

                case '3':
                    System.Console.WriteLine();
                    System.Console.WriteLine("Patients' Information");
                    System.Console.WriteLine("=====================");
                    Disease babar1 = new Cancer("Babar Saeed", new Stage_1());
                    Disease haris1 = new Cancer("Haris", new Stage_2());
                    babar1.info();
                    System.Console.ReadLine();
                    haris1.info();
                    System.Console.ReadLine();

                    Disease babar = new Pneumonia("Babar", new Stage_2());
                    babar.info();

                    System.Console.ReadLine();
                    Disease hassam = new Infection("Hassam", new Stage_1());
                    hassam.info();
                    System.Console.ReadLine();
                    break;

                case '4':
                    Context context = new Context();
                    LogoutState currentState = new LogoutState();
                    context.setState(currentState);
                    break;

                default:
                    System.Console.WriteLine();
                    System.Console.WriteLine("Invalid Input");
                    System.Console.WriteLine();
                    Doctor ss = new Doctor("abc");
                    break;
            }
        }
    }

    //Patient Class
    public class Patient
    {
        public Patient()
        {
            System.Console.WriteLine("Select");
            System.Console.WriteLine("==============");
            System.Console.WriteLine("1 for Doctor Details");
            System.Console.WriteLine("2 to Logout");

            char a = char.Parse(System.Console.ReadLine());
            switch (a)
            {
                case '1':
                    DoctorDetails doctor = new DoctorDetails();
                    doctor.Details();
                    break;

                case '2':
                    Context context = new Context();
                    LogoutState currentState = new LogoutState();
                    context.setState(currentState);
                    break;

                default:
                    System.Console.WriteLine();
                    System.Console.WriteLine("Invalid Input");
                    System.Console.WriteLine();
                    Patient ss = new Patient();
                    break;
            }
        }
    }

    //Go to Pharmacy Class
    public class GoToPharmacy
    {
        public GoToPharmacy()
        {
            pharmacy tab = FactoryProcedure.getFactory(true);
            pharmacy sy = FactoryProcedure.getFactory(false);
            System.Console.WriteLine();
            System.Console.WriteLine("Pharmacy");
            System.Console.WriteLine("=========");

            Medicine m1 = tab.takeMedicine("panadol");
            m1.take();
            Medicine m2 = tab.takeMedicine("ryzek");
            m2.take();
            Medicine m3 = sy.takeMedicine("brofeen");
            m3.take();
            Medicine m4 = sy.takeMedicine("coughsyrup");
            m4.take();

            System.Console.WriteLine();
            Program.Start();
        }
    }

    ///////////////////////////////////////////////
    ///////////////////////////////////////////////
    /// Patterns Starts Below
    ///////////////////////////////////////////////
    ///////////////////////////////////////////////

    //FOF Pattern Starts ---------------------------------------------
    //FactoryProcedure
    public class FactoryProcedure
    {
        public static pharmacy getFactory(Boolean factoryTypeMedicine)
        {
            if (factoryTypeMedicine == true)
            {
                return new Tablet();
            }
            else
            {
                return new Syrup();
            }
        }
    }

    //Abstract Factory
    public abstract class pharmacy
    {
        public abstract Medicine takeMedicine(string medicineType);
    }

    //Concrete Factory Tablet
    public class Tablet : pharmacy
    {
        public override Medicine takeMedicine(string medicineType)
        {
            if (medicineType == "panadol")
            {
                return new panadol();
            }

            else if (medicineType == "ryzek")
            {
                return new ryzek();
            }
            return null;
        }
    }

    //Concrete Factory Syrup
    public class Syrup : pharmacy
    {
        public override Medicine takeMedicine(string medicineType)
        {
            if (medicineType == "brofeen")
            {
                return new brofeen();
            }
            else if (medicineType == "coughsyrup")
            {
                return new coughsyrup();
            }
            return null;

        }
    }

    //Abstract Product Medicine
    public interface Medicine
    {
        void take();
    }

    //Concrete Product
    public class panadol : Medicine
    {
        public void take()
        {
            System.Console.WriteLine("Panadol Tablet");
        }
    }
    public class ryzek : Medicine
    {
        public void take()
        {
            System.Console.WriteLine("Ryzek Tablet");
        }
    }
    public class brofeen : Medicine
    {
        public void take()
        {
            System.Console.WriteLine("Brofeen Syrup");

        }
    }
    public class coughsyrup : Medicine
    {
        public void take()
        {
            System.Console.WriteLine("Cough Syrup");

        }
    }
    //FOF Pattern Ends ---------------------------------------------





    //Bridge Pattern Starts ---------------------------------------------
    public interface Ruler
    {
        void DiseaseInfo(string patient, string disease);
    }

    public class Stage_1 : Ruler
    {

        public void DiseaseInfo(string patient, string disease)
        {
            System.Console.WriteLine();
            System.Console.WriteLine(patient + " has " + disease + "[stage: 1]");
        }
    }

    public class Stage_2 : Ruler
    {
        public void DiseaseInfo(string patient, string disease)
        {
            System.Console.WriteLine();
            System.Console.WriteLine(patient + " has " + disease + "[stage: 2]");
        }
    }

    public abstract class Disease
    {
        protected Ruler diseaseInfo;

        protected Disease(Ruler diseaseInfo)
        {
            this.diseaseInfo = diseaseInfo;
        }
        public abstract void info();
    }

    public class Cancer : Disease
    {
        string name;
        public Cancer(string name, Ruler diseaseInfo) : base(diseaseInfo)
        {
            this.name = name;
        }

        public override void info()
        {
            diseaseInfo.DiseaseInfo(name, "Cancer");
        }
    }
    public class Infection : Disease
    {
        string name;
        public Infection(string name, Ruler diseaseInfo) : base(diseaseInfo)
        {
            this.name = name;
        }

        public override void info()
        {
            diseaseInfo.DiseaseInfo(name, "Infection");
        }
    }
    public class Pneumonia : Disease
    {
        string name;
        public Pneumonia(string name, Ruler diseaseInfo) : base(diseaseInfo)
        {
            this.name = name;
        }

        public override void info()
        {
            diseaseInfo.DiseaseInfo(name, "Pneumonia");
        }
    }
    //Bridge Pattern Ends ---------------------------------------------





    //State Pattern Starts ---------------------------------------------
    public interface State
    {
        void grantAccess();
    }
    public class LoginState : State
    {
        public void grantAccess()
        {
            System.Console.WriteLine("Logged In");
            System.Console.WriteLine();
        }
    }

    public class LogoutState : State
    {
        public void grantAccess()
        {
            System.Console.WriteLine("Logged Out");
            System.Console.WriteLine();
        }
    }

    public class Context
    {
        private State state;
        private Strategy strategy;
        public Context()
        {
            state = null;

        }
        public Context(Strategy str)
        {
            this.strategy = str;
        }

        public string execStrategy()
        {
            return strategy.doOp();
        }
        public void setState(State state)
        {
            this.state = state;
            this.state.grantAccess();
        }
        public State getState()
        {
            return state;
        }
    }
    //State Pattern Ends ---------------------------------------------





    //Strategy Pattern Starts ---------------------------------------------
    public interface Strategy
    {
        string doOp();
    }

    public class OpAdd : Strategy
    {
        public string doOp()
        {
            return "Person has been Added";
        }
    }

    public class OpRemove : Strategy
    {
        public string doOp()
        {
            return "Person has been Removed";
        }
    }
    //Strategy Pattern Ends ---------------------------------------------





    //Composite Pattern Starts ---------------------------------------------
    public class DoctorDetails
    {
        int id;
        public void Details()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Id    Name       Timings      Days                Specialization");
            System.Console.WriteLine("=================================================================");
            System.Console.WriteLine("1     Dr.Farhan  3:00-6:00pm  Tuesday-Wednestday  Dean");
            System.Console.WriteLine("2     Dr.Sumaira 5:00-8:00pm  Monday-Wednestday   Assistant Dean");
            System.Console.WriteLine("3     Dr.Asma    2:00-4:00pm  Tuesday-Thursday    Sr. Surgeon");
            System.Console.WriteLine("4     Dr.Ali     12:00-1:00pm Firday-Sunday       Neurosurgen");
            System.Console.WriteLine("5     Dr.Ahmed   9:00-8:00pm  Friday-Sunday       Skin specialist");
            System.Console.WriteLine("6     Dr.Aziz    1:00-10:00pm Monday-Sunday       Physiotheripist");

            Doctor dr = new Doctor();
            dr.name = "Dean Dr.Farhan";

            Doctor assistant = new Doctor();
            assistant.name = "Assistant Dr.Sumaira";

            Doctor Surgeon = new Doctor();
            Surgeon.name = "Sr. Surgeon Dr.Asma";

            Doctor Neurosurgen = new Doctor();
            Neurosurgen.name = "Dr.Ali";

            Doctor Skin = new Doctor();
            Skin.name = "Dr.Ahmed";

            Doctor Physiotheripist = new Doctor();
            Physiotheripist.name = "Dr.Aziz";

            dr.preReq.Add(assistant);
            dr.preReq.Add(Surgeon);

            assistant.preReq.Add(Physiotheripist);
            assistant.preReq.Add(Skin);

            Surgeon.preReq.Add(Neurosurgen);

            System.Console.WriteLine();
            System.Console.WriteLine(dr.name);
            foreach (Doctor item in dr.preReq)
            {
                System.Console.WriteLine(">>");
                System.Console.WriteLine(item.name);
                foreach (Doctor item2 in item.preReq)
                {
                    System.Console.WriteLine("--------------");
                    System.Console.WriteLine(item2.name);
                    foreach (Doctor item3 in item2.preReq)
                    {
                        System.Console.WriteLine("--------------");
                        System.Console.WriteLine(item3.name);
                        foreach (Doctor item4 in item3.preReq)
                        {
                            System.Console.WriteLine("--------------");
                            System.Console.WriteLine(item4.name);
                        }
                    }
                }
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Select Id to Request for Appointment");

            id = char.Parse(System.Console.ReadLine());
            switch (id)
            {
                case '1':
                    System.Console.WriteLine("Request send to Dr.Farhan");
                    break;
                case '2':
                    System.Console.WriteLine("Request send to Dr.Sumaira");
                    break;
                case '3':
                    System.Console.WriteLine("Request send to Dr.Asma");
                    break;
                case '4':
                    System.Console.WriteLine("Request send to Dr.Ali");
                    break;
                case '5':
                    System.Console.WriteLine("Request send to Dr.Ahmed");
                    break;
                case '6':
                    System.Console.WriteLine("Request send to Dr.Aziz");
                    break;
                default:
                    System.Console.WriteLine("Invalid ID");
                    break;
            }
        }
    }

}
//Composite Pattern Ends ---------------------------------------------




//Singleton Pattern Starts ---------------------------------------------
public class Login
{
    string docUsername = "Babar";
    string docPass = "123";
    string patUsername = "Haris";
    string patPass = "123";
    string inputUser;
    string inputPass;

    private static Login instance = new Login();

    private Login() { }

    public static Login getInstance()
    {
        return instance;
    }
    public void auth(char a)
    {
        switch (a)
        {
            case '1':
                System.Console.WriteLine("Enter Username:");
                inputUser = System.Console.ReadLine();

                System.Console.WriteLine("Enter Password:");
                inputPass = System.Console.ReadLine();

                if (inputUser == docUsername && inputPass == docPass)
                {
                    Context context = new Context();
                    LoginState currentState = new LoginState();
                    System.Console.WriteLine();
                    context.setState(currentState);

                    Doctor doc = new Doctor(inputPass);
                }
                else
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine("Login Failed");
                }
                break;

            case '2':
                System.Console.WriteLine("Enter Username:");
                inputUser = System.Console.ReadLine();

                System.Console.WriteLine("Enter Password:");
                inputPass = System.Console.ReadLine();

                if (inputUser == patUsername && inputPass == patPass)
                {
                    Context context = new Context();
                    LoginState currentState = new LoginState();
                    System.Console.WriteLine();
                    context.setState(currentState);

                    Patient pat = new Patient();
                }
                else
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine("Login Failed");
                }
                break;

            default:
                System.Console.WriteLine();
                System.Console.WriteLine("Invalid Input");
                System.Console.WriteLine();
                Program.Start();
                break;
        }
    }
}

//Singleton Pattern Ends ---------------------------------------------
///////////////////////////////////////////////
///////////////////////////////////////////////
///////////////////////////////////////////////
///////////////////////////////////////////////