using gyakorlat_9.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gyakorlat_9
{
    public partial class Form1 : Form
        
    {
        
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        List<int> Males = new List<int>();
        List<int> Females = new List<int>();
        Random rng = new Random(1234);
        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");
        }
            void Simulation()
            {
                for (int year = 2005; year <= 2024; year++)
                {
                    // Végigmegyünk az összes személyen
                    for (int i = 0; i < Population.Count; i++)
                    {
                        SimStep(year, Population[i]);

                        if (Population[i].Gender == Gender.Male)
                        {
                            Males.Add(year);
                        }
                        else
                        {
                            Females.Add(year);
                        }
                    }

                    int nbrOfMales = (from x in Population
                                      where x.Gender == Gender.Male && x.IsAlive
                                      select x).Count();
                    int nbrOfFemales = (from x in Population
                                        where x.Gender == Gender.Female && x.IsAlive
                                        select x).Count();
                    Console.WriteLine(
                        string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
                }
            }
        

        private List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> deathProbabilities = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deathProbabilities.Add(new DeathProbability()
                    {
                        D_gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        D_age = int.Parse(line[1]),
                        D_P = double.Parse(line[2])
                    });
                }
            }

            return deathProbabilities;
        }

        private List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> birthProbabilities = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    birthProbabilities.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        Children = int.Parse(line[1]),
                        P = double.Parse(line[2])
                    });
                }
            }

            return birthProbabilities;
        }

        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }

            return population;
        }
        void SimStep(int year, Person person)
        {
            if (!person.IsAlive) return;

            int age = year - person.BirthYear;

            double pD = (from x in DeathProbabilities
                         where x.D_age == age && x.D_gender == person.Gender
                         select x.D_P).FirstOrDefault();

            if (rng.NextDouble() <= pD) person.IsAlive = false;

            if (person.IsAlive == false || person.Gender == Gender.Male) return;

            double pB = (from x in BirthProbabilities
                         where x.Age == age && x.Children == person.NbrOfChildren
                         select x.P).FirstOrDefault();

            if (rng.NextDouble() <= pB)
            {
                Person newBorn = new Person();
                newBorn.Gender = (Gender)rng.Next(1, 3);
                newBorn.BirthYear = year;
                newBorn.NbrOfChildren = 0;
                Population.Add(newBorn);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Population = GetPopulation(textBox1.Text);
            BirthProbabilities = GetBirthProbabilities(@"C:\Windows\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Windows\Temp\halál.csv");

            Males.DefaultIfEmpty();
            Females.DefaultIfEmpty();
            richTextBox1.Text = "";

            Simulation();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "Comma Seperated Values (*.csv)|*.csv";
            ofd.DefaultExt = "csv";
            ofd.AddExtension = true;
            if (ofd.ShowDialog() != DialogResult.OK) return;
            textBox1.Text = ofd.FileName;
        }




    }

    
    


}
