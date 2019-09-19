using supertalentoftheworld.Models;
using System.Data.Entity;
using System.Linq;
namespace SmartFactory.Util
{
    public class msg
    {
        public static string msg_insert = "Saved.";
        public static string msg_edit = "Update complete";
        public static string msg_del = "Deleted.";
        public static string msg_no = "You do not have permission.";
        public static string msg_disable = "Disable.";



    }

    public class get_word
    {



        // string language = Request.Cookies["language"].Value ?? "korea";
        public static string language_code(string language_code, string code)
        {
            db_e db = new db_e();

            code = code.Trim();

            string _language_code = "korea";

            if (language_code == "korea")
            {

                _language_code = (from a in db.language
                                  where a.code_name.Equals(code)
                                  select a.korea).FirstOrDefault();
            }

            else if (language_code == "english")
            {

                _language_code = (from a in db.language
                                  where a.code_name.Equals(code)
                                  select a.english).FirstOrDefault();

            }

            return _language_code;

        }
        public static string style_color(string code)
        {

            string color = "red";

            if (code == "blue") { color = "tbl_blue"; }
            else if (code == "azure") { color = "tbl_azure"; }
            else if (code == "green") { color = "tbl_green"; }
            else if (code == "orange") { color = "tbl_orange"; }
            else if (code == "red") { color = "tbl_red"; }
            else if (code == "purple") { color = "tbl_purple"; }





            return color;
        }

    }
 
}