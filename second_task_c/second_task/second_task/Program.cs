using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;

namespace second_task
{
    class Program
    {
        static StreamWriter sw = new StreamWriter("result.txt");
        public const string connectionString = "server=127.0.0.1;port=3306;user id=root; password=; database=cs_beugro; SslMode=none";
        static Random rnd = new Random();
        public static Dictionary<string, string> from_olvass_txt = new Dictionary<string, string>(); // másik megoldás egy "olvass" class lenne
                                                                                                     // de a dictionary itt adja magát
        static List<int> randomNumbers(int b, byte db) //Visszaad "db" egyedi random számot [1 ; b[ intervallumon                                          
        {
            if (b <= db)
            {
                throw new Exception(string.Format("Nem tudok {0} db egyedi random számot generálni 1-{1} között!", db, b));
            }
            List<int> randomList = new List<int>();
            int num;
            for (int i = 0; i < db; i++)
            {
                num = rnd.Next(1, b);
                if (!randomList.Contains(num))
                {
                    randomList.Add(num);
                }
                else
                {
                    i--;
                }
            }

            return randomList;
        }
        //
        static void readOlvassTxt()
        {
            StreamReader sr = new StreamReader("olvass.txt");
            string sor;
            string key;
            string value;
            while ((sor = sr.ReadLine()) != null)
            {
                string[] data = sor.Split('|');
                key = data[0];
                value = data[1];
                from_olvass_txt.Add(key, value);
            }
            sr.Close();
        }
        //
        static List<string> convertRandomNumbersToString(List<int> numbers)
        {
            List<string> randomNumsString = new List<string>();
            for (int i = 0; i < numbers.Count; i++)
            {
                randomNumsString.Add(numbers[i].ToString());
            }
            return randomNumsString;
        }
        //
        static List<int> randomNums = randomNumbers(51, 10);
        static List<string> randomNumsString = convertRandomNumbersToString(randomNums); // a könnyebb kezelhetőségért stringre konvertálom
        //
        static List<int> selectedValues()
        {
            List<int> values = new List<int>();
            int z;
            foreach (KeyValuePair<string, string> a in from_olvass_txt)
            {
                if (randomNumsString.Contains(a.Key))
                {
                    if (int.TryParse(a.Value, out z) && z > 0)
                    {
                        values.Add(z);
                    }
                }
            }
            return values;
        }
        static void Main(string[] args)
        {
            readOlvassTxt();
            List<int> user_id_list = selectedValues();
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            sw.WriteLine("tulajdonos" + "\t" + "márka" + "\t" + "tipus");
            for (int i = 0; i < user_id_list.Count; i++)
            {
                MySqlCommand cmd_database = new MySqlCommand(String.Format("SELECT user.name,car.brand,car.model FROM user,car,user_car WHERE user.id = user_car.user AND user_car.car = car.id AND user.id = {0}", user_id_list[i]), connection);
                MySqlDataReader dr = cmd_database.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        sw.WriteLine(dr.GetString(0) + "\t" + dr.GetString(1) + "\t" + dr.GetString(2));                       
                        //Console.WriteLine(dr.GetString(0) + "\t" + dr.GetString(1) + "\t" + dr.GetString(2));
                    }
                }
                dr.Close();
            }
            sw.Close();
            Console.ReadKey();
        }
    }
}