using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Shared;
using MongoDB.Libmongocrypt;
namespace test_project;
public class Program  {


    public static async Task Main(string[] args)
    {
        //setup mongodb connections
        
        string conString = File.ReadAllText("database.config");
        var settings = MongoClientSettings.FromConnectionString(conString);
        var client = new MongoClient(settings);
        IMongoDatabase db = client.GetDatabase("Test");
        var collection = db.GetCollection<User>("Users");

        //Do stuff with the db
        Console.WriteLine("Connected to Mongodb, enter a first name then a last...");
        string first = Console.ReadLine();
        string last = Console.ReadLine();
        User user = await findUserAsync(first,last,collection);
        if(user != null)
        {
            Console.WriteLine("Found the user! \n"+user.ToJson());
        }


    }

    static async Task<User> findUserAsync(string firstName,string lastName,IMongoCollection<User> collection)
    {
        using (IAsyncCursor<User> cursor = await collection.FindAsync(x=>x.FirstName == firstName && x.LastName == lastName))
        {
            User p = cursor.FirstOrDefault();
            if(p != null)
            {
                return p;
            }else
                Console.WriteLine("No result found...");
        }

        return null;
    }

    public class User
    {

        public ObjectId Id { get; set; }
        public string Gender { get; set; }
        public string FirstName {get; set; }
        public string LastName {get; set; }
        public string UserName {get; set; }
        public string Avatar {get; set; }
        public string Email {get; set; }
        public DateTime DateOfBirth {get; set; }
        public string Address {get; set; }
        public string Phone {get; set; }

        public string Website {get; set; }
        public string Company {get; set; }
        public decimal Salary { get; set; }
        public int MonthlyExpenses { get; set; }
        public List<string> FavoriteSports { get; set; }
        public string Profession { get; set; }
    }}