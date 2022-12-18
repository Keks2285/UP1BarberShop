namespace BarberShopTests
{
    using Newtonsoft.Json;
    using RestSharp;
    public class StoredProceduresTest
    {
        public static RestClient client = new RestClient("http://192.168.1.49:8080/BarberApi/");
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("Новиков", "Новиков", "Новиков", "1123222222" , "ilion230221@gmail.com", 1,1,237,true)]
        public void Employe_Update_Procedure_Test(string firstname, string lastname, 
                                                  string middlename, string inn,string email,
                                                  int status, int post,int emplyer_id , bool expected)
        {


            var req = new RestRequest("/updateEmploye", Method.Post);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            req.AddParameter("firstname", firstname);
            req.AddParameter("lasttname", lastname);
            req.AddParameter("middlename", middlename);
            req.AddParameter("email", email);
            req.AddParameter("inn", inn);
            req.AddParameter("post_id", post);
            req.AddParameter("status_id", status);
            req.AddParameter("id_employer", emplyer_id);
            var res = client.Post(req);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);

            Assert.AreEqual(data.status.value, expected, "greygrtjiyhji");
        }



        [TestCase( 13, true)]
        public void Post_Delete_Procedure_Test( int id_post, bool expected)
        {


            var req = new RestRequest("/removePost", Method.Post);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            req.AddParameter("id", id_post);
            var res = client.Post(req);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);

            Assert.AreEqual(data.status.value, expected, "greygrtjiyhji");
        }


        [TestCase("ДолжностьД",15658, true)]
        public void Post_Insert_Procedure_Test(string postname, int post_price, bool expected)
        {


            var req = new RestRequest("/createPost", Method.Post);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            req.AddParameter("name_post", postname);
            req.AddParameter("price", post_price);
            var res = client.Post(req);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);

            Assert.AreEqual(data.status.value, expected, "greygrtjiyhji");
        }

        [TestCase("ДолжностьА", 15658, 14,true)]
        public void Post_UpDate_Procedure_Test(string postname, int post_price,int post_id ,bool expected)
        {


            var req = new RestRequest("/updatePost", Method.Post);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            req.AddParameter("namepost", postname);
            req.AddParameter("price", post_price);
            req.AddParameter("id_post", post_id);
            var res = client.Post(req);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);

            Assert.AreEqual(data.status.value, expected, "greygrtjiyhji");
        }
    }
}